using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float ProjectileSpeed;
    protected int DamageValue;
    protected SpriteRenderer Renderer;

    private Rigidbody2D _rb2D;

    protected virtual void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int weaponDamage, Sprite sprite)
    {
        Renderer.sprite = sprite;
        DamageValue = weaponDamage;
        transform.position = startPoint;
        transform.rotation = angle;
        _rb2D.linearVelocity = (direction * ProjectileSpeed);
    }

    protected void DealDamage(Health health)
    {
        health.TakeDamage(DamageValue);
    }
    
    protected void Release()
    {
        gameObject.SetActive(false);
    }
}
