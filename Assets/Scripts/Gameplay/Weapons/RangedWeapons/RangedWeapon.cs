using UnityEngine;

public abstract class RangedWeapon : ScriptableObject
{
    [SerializeField] private int _maxMagCapacity;
    [SerializeField] private float _reloadTimer;
    [SerializeField] private float _fireRate;

    private int _currentAmmoCount;

    public int MaxMagCapacity => _maxMagCapacity;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;
    public int CurrentAmmoCount => _currentAmmoCount;

    public void SetInitialAmmo()
    {
        _currentAmmoCount = _maxMagCapacity;
    }
    
    public void Shoot()
    {
        _currentAmmoCount--;
    }

    public void Reload()
    {
        _currentAmmoCount = _maxMagCapacity;
    }
}
