using System.Collections;
using UnityEngine;

public class
    EnemyShooter : GameplayEntityShooter
{
    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _bulletFactory;

    private void Awake()
    {
        FireRate = 2;
    }

    public void GetBulletsFactoryReference(BulletsFactory factory)
    {
        _bulletFactory = factory;
    }

    protected override IEnumerator ShootingCoroutine()
    {
        while (IsShooting)
        {
            if (CanShoot)
            {
                _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right);
                CanShoot = false;
                StartCoroutine(nameof(LimitFireRateCoroutine));
            }
            yield return new WaitForSeconds(FireRate);
        }
    }
}