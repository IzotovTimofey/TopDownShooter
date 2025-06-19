using System.Collections;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float _additiveRadiusOnExplosion;
    [SerializeField] private float _explosionDuration;
    [SerializeField] private float _projectileLifeTime;

    private CircleCollider2D _collider;
    private bool _isExploding;

    protected override void Awake()
    {
        base.Awake();
        _collider = gameObject.GetComponent<CircleCollider2D>();
    }

    public override void LaunchProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int weaponDamage)
    {
        DamageValue = weaponDamage;
        transform.position = startPoint;
        transform.rotation = angle;
        _rb2D.linearVelocity = (direction * ProjectileSpeed);
        StartCoroutine(nameof(MissleFlyCoroutine));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out GameplayEntity controller))
        {
            if (!_isExploding)
                StartCoroutine(nameof(ExplodeCoroutine));
            DealDamage(controller.Health);
        }
        else if (col.gameObject.tag == "Map")
        {
            if (!_isExploding)
                StartCoroutine(nameof(ExplodeCoroutine));
        }
    }

    private IEnumerator MissleFlyCoroutine()
    {
        yield return new WaitForSeconds(_projectileLifeTime);
        StartCoroutine(nameof(ExplodeCoroutine));
    }

    private IEnumerator ExplodeCoroutine()
    {
        StopMoving();
        _isExploding = true;
        _collider.radius += _additiveRadiusOnExplosion;
        yield return new WaitForSeconds(_explosionDuration);
        _collider.radius -= _additiveRadiusOnExplosion;
        _isExploding = false;
        Release();
    }

    private void StopMoving()
    {
        StopCoroutine(nameof(MissleFlyCoroutine));
        _rb2D.linearVelocity = Vector2.zero;
    }
}