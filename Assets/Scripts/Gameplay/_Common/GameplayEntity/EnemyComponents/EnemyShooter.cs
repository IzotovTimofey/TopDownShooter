using System.Collections;
using UnityEngine;

public class EnemyShooter : GameplayEntityShooter
{
    [SerializeField] private Transform _shootPoint;

    protected override IEnumerator ShootingCoroutine()
    {
        while (IsShooting)
        {
            if (CanShoot && !IsReloading)
            {
                BulletsFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right, CurrentWeapon.WeaponDamage, CurrentWeapon.AmmunitionVisual);
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