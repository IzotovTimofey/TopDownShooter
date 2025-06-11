using System.Collections;
using UnityEngine;

public class EnemyShooter : GameplayEntityShooter
{
    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _bulletFactory;

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
                _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right, DamageValueModifier);
                CurrentWeapon.Shoot();
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
        IsReloading = false;
    }
}