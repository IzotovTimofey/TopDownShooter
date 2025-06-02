using UnityEngine;

public class BulletsFactory : MonoBehaviour
{   
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletsPoolParent;
    [SerializeField] private int _startBulletsCapacity;

    private GenericPool<Bullet> _bulletsPool;
    
    public static BulletsFactory Instance { get; private set; } // TODO: Снова синглтон, убрать, сделать прокидывание ссылок через SceneBoot/DI Zenject
    
    private void Awake()
    {
        Instance = this;
        _bulletsPool = new(null, _bullet, _startBulletsCapacity, _bulletsPoolParent);
    }

    public void SpawnBullet(Quaternion angle, Vector3 startPoint, Vector3 direction)
    {
        Bullet bullet = _bulletsPool.GetObjectFromPool(true);
        bullet.BulletFly(angle, startPoint, direction);
    }
}
