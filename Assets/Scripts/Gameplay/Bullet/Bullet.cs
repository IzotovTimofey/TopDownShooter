using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private int _damageValue = 25;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void BulletFly(Quaternion direction, Vector3 startPoint)
    {
        transform.position = startPoint;
        transform.rotation = direction;
        _rb2D.linearVelocity = (Vector2.right * _bulletSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out GameplayEntity controller))
        {
            DealDamage(controller.HealthComponent);
        }
        gameObject.SetActive(false);
    }

    private void DealDamage(HealthComponent healthComponent)
    {
        healthComponent.TakeDamage(_damageValue);
    }
}
