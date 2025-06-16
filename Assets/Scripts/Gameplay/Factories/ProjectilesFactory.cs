using UnityEngine;

public class ProjectilesFactory : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletsPoolParent;
    [SerializeField] private int _startBulletsCapacity;

    private GenericPool<Projectile> _projectilesPool;
    
    private void Awake()
    {
        _projectilesPool = new(null, _bullet, _startBulletsCapacity, _bulletsPoolParent);
    }

    public void SpawnBullet(Quaternion angle, Vector3 startPoint, Vector3 direction, int DamageValue)
    {
        Projectile projectile = _projectilesPool.GetObjectFromPool(true);
        projectile.LaunchProjectile(angle, startPoint, direction, DamageValue);
    }
}
