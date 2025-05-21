using UnityEngine;

public class BulletsFactory : MonoBehaviour
{   
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletsPoolParent;
    [SerializeField] private int _startBulletsCapacity;

    private GenericPool<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new(null, _bullet, _startBulletsCapacity, _bulletsPoolParent);
    }

    public void SpawnBullet(Quaternion angle, Vector3 startPoint, Vector3 direction)
    {
        Bullet bullet = _bulletsPool.GetObjectFromPool(true);
        bullet.BulletFly(angle, startPoint, direction);
        //bullet.Setup(startPoint, direction);
    }
}
