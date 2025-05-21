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
        _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position);
    }

}
