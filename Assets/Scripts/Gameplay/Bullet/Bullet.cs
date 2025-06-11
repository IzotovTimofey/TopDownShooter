using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private int _damageValue = 5;

    private int _totalDamage;
    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void BulletFly(Quaternion angle, Vector3 startPoint, Vector3 direction, int damageModifier)
    {
        _totalDamage = _damageValue + damageModifier;
        transform.position = startPoint;
        transform.rotation = angle;
        _rb2D.linearVelocity = (direction * _bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GameplayEntity controller))
        {
            DealDamage(controller.Health);
            Debug.Log(_totalDamage);
        }
        else if (collision.gameObject.tag == "Map")
        {
            Release();
        }
    }

    private void DealDamage(Health health)
    {
        health.TakeDamage(_totalDamage);
        Release();
    }

    private void Release()
    {
        gameObject.SetActive(false);
    }
}   