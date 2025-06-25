using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : GameplayEntityShooter
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;

    [SerializeField] private Transform _shootPoint;

    private List<PickedUpWeapon> _pickedUpWeapons = new();
    private int _weaponIndex;
    private TimerService _timerService;

    public List<PickedUpWeapon> PickedUpWeapons => _pickedUpWeapons;
    public event UnityAction<int, int> AmmoValueChanged;


    protected override void Awake()
    {
        base.Awake();
        _pickedUpWeapons.Add(CurrentWeapon);
    }

    private void Start()
    {
        CurrentWeapon.GetTimerService(_timerService);
    }

    public void GetTimerService(TimerService timerService)
    {
        _timerService = timerService;
    }

    private void OnEnable()
    {
        _reader.OnPlayerShoot += Shoot;
        _reader.OnPlayerReload += OnReloadInput;
        _reader.OnPlayerWeaponSwap += SwapCurrentWeapon;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= Shoot;
        _reader.OnPlayerReload -= OnReloadInput;
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

    private void OnReloadInput()
    {
        IsReloading = true;
        StartCoroutine(nameof(ReloadingCoroutine));
    }
    

    protected override void OnReload()
    {
        AmmoValueChanged?.Invoke(CurrentWeapon.CurrentAmmoCount, CurrentWeapon.MaxMagCapacity);
    }

    protected override void OnShoot()
    {
        ProjectilesFactory.SpawnProjectile(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection, CurrentWeapon.WeaponDamage, CurrentWeapon.Projectile);
    }

    public void GetWeapon(RangedWeapon weapon)
    {
        PickedUpWeapon newWeapon = new(weapon);
        _pickedUpWeapons.Add(newWeapon);
        newWeapon.GetTimerService(_timerService);
    }
}