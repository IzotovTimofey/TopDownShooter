using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : GameplayEntityShooter
{
    // TODO: Разобраться с оружием, у каждого оружия должно быть правильное стартовое значение патронов/правильная перезарядка, для этого создан отдельный класс
    // В качестве референса - класс Team Assigner в DroneMiningOperation
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;

    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _factory;
    [SerializeField] private List<RangedWeapon> _weapons = new();
    private int _weaponIndex;
    public event UnityAction<int, int> AmmoValueChanged;

    private void Awake()
    {
        _weapons.Add(CurrentWeapon);
        CurrentAmmoCount = CurrentWeapon.MaxMagCapacity;
    }

    private void OnEnable()
    {
        _reader.OnPlayerShoot += Shoot;
        _reader.OnPlayerReload += OnReload;
        _reader.OnPlayerWeaponSwap += SwapCurrentWeapon;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= Shoot;
        _reader.OnPlayerReload -= OnReload;
        _reader.OnPlayerWeaponSwap -= SwapCurrentWeapon;
    }

    private void SwapCurrentWeapon(float value)
    {
        if (value > 0)
            _weaponIndex++;
        else
            _weaponIndex--;
        if (_weaponIndex >= _weapons.Count)
            _weaponIndex = 0;
        else if (_weaponIndex < 0)
            _weaponIndex = _weapons.Count - 1;
        CurrentWeapon = _weapons[_weaponIndex];
        AmmoValueChanged?.Invoke(CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
    }

    public void SetUp(BulletsFactory factory)
    {
        _factory = factory;
    }

    private void OnReload()
    {
        StartCoroutine(nameof(ReloadingCoroutine));
    }

    protected override IEnumerator ShootingCoroutine()
    {
        while (IsShooting)
        {
            if (CanShoot && !IsReloading)
            {
                _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position,
                    _directionProvider.IdleDashDirection);
                CurrentAmmoCount--;
                AmmoValueChanged?.Invoke(CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
                CanShoot = false;
                StartCoroutine(nameof(LimitFireRateCoroutine));
            }

            if (CurrentAmmoCount <= 0)
                yield return StartCoroutine(nameof(ReloadingCoroutine));
            else
                yield return new WaitForSeconds(CurrentWeapon.FireRate);
        }
    }

    protected override IEnumerator ReloadingCoroutine()
    {
        IsReloading = true;
        yield return new WaitForSeconds(CurrentWeapon.ReloadTimer);
        CurrentAmmoCount = CurrentWeapon.MaxMagCapacity;
        AmmoValueChanged?.Invoke(CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
        IsReloading = false;
    }
}