using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : GameplayEntityShooter
{
    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _bulletFactory;

    private void Awake()
    {
        CurrentAmmoCount = CurrentWeapon.MaxMagCapacity;
    }

    public void GetBulletsFactoryReference(BulletsFactory factory)
    {
        _bulletFactory = factory;
    }

    protected override IEnumerator ShootingCoroutine()
    {
        while (IsShooting)
        {
            if (CanShoot && !IsReloading)
            {
                _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right);
                CurrentAmmoCount--;
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
        IsReloading = false;
    }
}