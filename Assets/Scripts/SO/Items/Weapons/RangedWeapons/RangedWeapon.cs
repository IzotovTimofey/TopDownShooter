using UnityEngine;

public abstract class RangedWeapon : CollectableItem
{
    [SerializeField] private int _maxMagCapacity;
    [SerializeField] private float _reloadTimer;
    [SerializeField] private float _fireRate;
    [SerializeField] private ProjectileType _projectile;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;
    public ProjectileType Projectile => _projectile;

    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out PlayerShooter playerShooter);
        playerShooter.GetWeapon(this);
    }
    
    
}
