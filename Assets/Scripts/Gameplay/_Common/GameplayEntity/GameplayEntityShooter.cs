using System.Collections;
using UnityEngine;

public abstract class GameplayEntityShooter : MonoBehaviour
{
    protected float FireRate;
    protected bool IsShooting;
    protected bool CanShoot = true;
    
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
        yield return new WaitForSeconds(FireRate);
        CanShoot = true;
    }
}
