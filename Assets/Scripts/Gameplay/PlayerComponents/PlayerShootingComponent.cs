using UnityEngine;

public class PlayerShootingComponent : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionSetter _directionSetter;
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
        Vector3 targetDirection = _directionSetter.LookingDirection();
        _factory.SpawnBullet(targetDirection, _shootPoint.position);
        
        //GameObject projectile = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        //projectile.GetComponent<Rigidbody2D>().AddForce(_shootPoint.right * _bulletSpeed, ForceMode2D.Impulse);
    }

}
