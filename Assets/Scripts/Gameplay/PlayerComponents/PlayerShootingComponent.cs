using UnityEngine;

public class PlayerShootingComponent : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;
    [SerializeField] private BulletsFactory _factory;

    [SerializeField] private Transform _shootPoint;

    private void OnEnable()
    {
        _reader.OnPlayerShoot += Shoot;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= Shoot;
    }

    private void Shoot()
    {
        Quaternion targetRotation = _directionProvider.GetLookDirection();
        _factory.SpawnBullet(targetRotation, _shootPoint.position);
        
        //GameObject projectile = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        //projectile.GetComponent<Rigidbody2D>().AddForce(_shootPoint.right * _bulletSpeed, ForceMode2D.Impulse);
    }

}
