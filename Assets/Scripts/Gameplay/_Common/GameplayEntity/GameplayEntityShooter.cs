using System.Collections;
using UnityEngine;

public abstract class GameplayEntityShooter : MonoBehaviour
{
    [SerializeField] protected RangedWeapon CurrentWeapon;
    protected bool IsShooting;
    protected bool CanShoot = true;
    protected bool IsReloading;

    protected virtual void Awake()
    {
        CurrentWeapon.SetInitialAmmo();
    }

    public void Shoot(bool state)
    {
        IsShooting = state;
        if (IsShooting)
        {
            StartCoroutine(nameof(ShootingCoroutine));
        }
        else
            StopCoroutine(nameof(ShootingCoroutine));
    }

    protected abstract IEnumerator ShootingCoroutine();

    protected IEnumerator LimitFireRateCoroutine()
    {
        yield return new WaitForSeconds(CurrentWeapon.FireRate);
        CanShoot = true;
    }

    protected abstract IEnumerator ReloadingCoroutine();
}
