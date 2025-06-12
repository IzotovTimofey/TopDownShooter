using System.Collections;
using UnityEngine;

public abstract class GameplayEntityShooter : MonoBehaviour
{
    [SerializeField] protected RangedWeapon StartingWeapon;
    protected bool IsShooting;
    protected bool CanShoot = true;
    protected bool IsReloading;
    protected PickedUpWeapon CurrentWeapon;
    protected BulletsFactory BulletsFactory;

    protected virtual void Awake()
    {
        CurrentWeapon = new PickedUpWeapon(StartingWeapon);
    }

    public void SetUp(BulletsFactory factory)
    {
        BulletsFactory = factory;
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
