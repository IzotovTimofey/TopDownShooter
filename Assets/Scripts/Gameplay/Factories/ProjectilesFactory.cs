using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesFactory : MonoBehaviour
{
    [Serializable]
    private class ProjectilePool
    {
        public Projectile ProjectilePrefab;
        public int PoolCapacity;
    }
    
    [SerializeField] private Transform _projectilesPoolParent;
    [SerializeField] private List<ProjectilePool> _projectilePools;

    private Dictionary<ProjectileType, GenericPool<Projectile>> _projectilesPool = new();

    private void Awake()
    {
        for (int i = 0; i < _projectilePools.Count; i++)
        {
            _projectilesPool.Add(_projectilePools[i].ProjectilePrefab.ProjectileType,
                new GenericPool<Projectile>(null,
                _projectilePools[i].ProjectilePrefab.gameObject,
                _projectilePools[i].PoolCapacity,
                _projectilesPoolParent));
        }
    }

    public void SpawnProjectile(Quaternion angle, Vector3 startPoint, Vector3 direction, int damageValue, ProjectileType projectileType)
    {
        GenericPool<Projectile> projectilePool = null;
        if (_projectilesPool.TryGetValue(projectileType, out projectilePool))
        {
            Projectile projectile = projectilePool.GetObjectFromPool(true);
            projectile.LaunchProjectile(angle, startPoint, direction, damageValue);
        }
    }
}