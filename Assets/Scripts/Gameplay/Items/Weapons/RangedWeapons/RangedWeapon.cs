using UnityEngine;

public abstract class RangedWeapon : CollectableItem
{
    [SerializeField] private int _maxMagCapacity;
    [SerializeField] private float _reloadTimer;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _weaponDamage;

    [SerializeField] private Sprite _ammunitionVisual;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;
    public int WeaponDamage => _weaponDamage;
    public Sprite AmmunitionVisual => _ammunitionVisual;

    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out PlayerShooter playerShooter);
        playerShooter.GetWeapon(this);
    }
    
    
}
