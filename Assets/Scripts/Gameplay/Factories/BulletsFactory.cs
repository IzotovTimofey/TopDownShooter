using UnityEngine;

public class BulletsFactory : MonoBehaviour
{   
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletsPoolParent;
    [SerializeField] private int _startBulletsCapacity;

    private GenericPool<Bullet> _bulletsPool;
    // TODO: сделать прокидывание ссылок через SceneBoot/DI Zenject
    
    private void Awake()
    {
        _bulletsPool = new(null, _bullet, _startBulletsCapacity, _bulletsPoolParent);
    }

    public void SpawnBullet(Quaternion angle, Vector3 startPoint, Vector3 direction, int DamageValue, Sprite sprite)
    {
        Bullet bullet = _bulletsPool.GetObjectFromPool(true);
        bullet.LaunchProjectile(angle, startPoint, direction, DamageValue, sprite);
    }
}
