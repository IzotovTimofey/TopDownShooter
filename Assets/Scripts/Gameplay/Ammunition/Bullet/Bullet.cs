using UnityEngine;

public class Bullet : Projectile
{
    public override void LaunchProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int weaponDamage)
    {
        DamageValue = weaponDamage;
        transform.position = startPoint;
        transform.rotation = angle;
        _rb2D.linearVelocity = (direction * ProjectileSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GameplayEntity controller))
        {
            DealDamage(controller.Health);
        }
        else if (collision.gameObject.tag == "Map")
        {
            Release();
        }
    }
}   