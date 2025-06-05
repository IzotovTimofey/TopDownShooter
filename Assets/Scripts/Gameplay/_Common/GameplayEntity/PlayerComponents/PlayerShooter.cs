using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : GameplayEntityShooter
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;
    
    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _factory;

    public event UnityAction<int, int> AmmoValueChanged;

    private void OnEnable()
    {
        _reader.OnPlayerShoot += Shoot;
        _reader.OnPlayerReload += OnReload;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= Shoot;
        _reader.OnPlayerReload -= OnReload;
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
                _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection);
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
}