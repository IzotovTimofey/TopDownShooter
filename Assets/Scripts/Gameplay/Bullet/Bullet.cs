using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20f;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void BulletFly(Vector3 direction, Vector3 startPoint)
    {
        //TODO: Исправить вращение пули, она смотрит не туда
        gameObject.transform.position = startPoint;
        _rb2D.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
