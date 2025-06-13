using System;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _projectileLifeTime;

    public override void LaunchProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int weaponDamage)
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        throw new NotImplementedException();
    }

    private void Explode()
    {
        
    }
}
