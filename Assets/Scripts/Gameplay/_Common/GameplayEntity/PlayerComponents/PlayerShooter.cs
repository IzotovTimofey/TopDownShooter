using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : GameplayEntityShooter
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;

    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _factory;
    private List<PickedUpWeapon> _pickedUpWeapons = new();
    private int _weaponIndex;
    public event UnityAction<int, int> AmmoValueChanged;

    protected override void Awake()
    {
        base.Awake();
        _pickedUpWeapons.Add(CurrentWeapon);
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
        if (_pickedUpWeapons.Count <= 1)
            return;
        if (value > 0)
            _weaponIndex++;
        else
            _weaponIndex--;
        if (_weaponIndex >= _pickedUpWeapons.Count)
            _weaponIndex = 0;
        else if (_weaponIndex < 0)
            _weaponIndex = _pickedUpWeapons.Count - 1;
        StopCoroutine(nameof(ReloadingCoroutine));
        IsReloading = false;
        CurrentWeapon = _pickedUpWeapons[_weaponIndex];
        AmmoValueChanged?.Invoke(CurrentWeapon.CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
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
                _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection, DamageValueModifier);
                CurrentWeapon.Shoot();
                AmmoValueChanged?.Invoke(CurrentWeapon.CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
                CanShoot = false;
                StartCoroutine(nameof(LimitFireRateCoroutine));
            }

            if (CurrentWeapon.CurrentAmmoCount <= 0)
                yield return StartCoroutine(nameof(ReloadingCoroutine));
            else
                yield return new WaitForSeconds(CurrentWeapon.FireRate);
        }
    }

    protected override IEnumerator ReloadingCoroutine()
    {
        IsReloading = true;
        yield return new WaitForSeconds(CurrentWeapon.ReloadTimer);
        CurrentWeapon.Reload();
        AmmoValueChanged?.Invoke(CurrentWeapon.CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
        IsReloading = false;
    }

    public void GetWeapon(RangedWeapon weapon)
    {
        PickedUpWeapon newWeapon = new(weapon);
        _pickedUpWeapons.Add(newWeapon);
    }

    public void GetDamageBuff(float duration, int value)
    {
        StartCoroutine(BuffDamageCoroutine(duration, value));
    }
    
    private IEnumerator BuffDamageCoroutine(float duration, int value)
    {
        DamageValueModifier += value;
        yield return new WaitForSeconds(duration);
        DamageValueModifier -= value;
    }
}