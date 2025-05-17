using UnityEngine;
using Zenject;

public class BulletsFactory : MonoBehaviour
{
    [Inject] private readonly DiContainer _container;
    
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletsPoolParent;
    [SerializeField] private int _startBulletsCapacity;

    private GenericPool<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new(_container, _bullet, _startBulletsCapacity, _bulletsPoolParent);
    }

    public void SpawnBullet(Vector3 direction, Vector3 startPoint)
    {
        Bullet bullet = _bulletsPool.GetObjectFromPool(true);
        bullet.BulletFly(direction, startPoint);
        //bullet.Setup(startPoint, direction);
    }
}
