using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float ProjectileSpeed;
    protected int DamageValue;

    protected Rigidbody2D _rb2D;

    protected virtual void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public abstract void LaunchProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int weaponDamage);

    protected void DealDamage(Health health)
    {
        health.TakeDamage(DamageValue);
    }
    
    protected void Release()
    {
        gameObject.SetActive(false);
    }
}
