using System.Collections.Generic;
using UnityEngine;

public class ProjectilesFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _projectiles;
    [SerializeField] private Transform _projectilesPoolParent;
    [SerializeField] private int _startProjectilesCapacity;

    private Dictionary<int, GenericPool<Projectile>> _projectilesPool = new();

    private void Awake()
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            _projectilesPool.Add(i, new GenericPool<Projectile>(null, _projectiles[i], _startProjectilesCapacity, _projectilesPoolParent));
        }
    }

    public void SpawnProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int DamageValue, int index)
    {
        GenericPool<Projectile> projectilePool = null;
        if (_projectilesPool.TryGetValue(index, out projectilePool))
        {
            Projectile projectile = projectilePool.GetObjectFromPool(true);
            projectile.LaunchProjectile(angle, startPoint, direction, DamageValue);
        }
    }
}