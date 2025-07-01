using UnityEngine;

public class EnemyShooter : GameplayEntityShooter
{
    [SerializeField] private Transform _shootPoint;
    
    protected override void OnReload()
    {
        Debug.Log("EnemyShooting");
    }

    protected override void OnShoot()
    {
        ProjectilesFactory.SpawnProjectile(transform.rotation, _shootPoint.position, transform.right, DamageValue, CurrentWeapon.Projectile);
    }
}