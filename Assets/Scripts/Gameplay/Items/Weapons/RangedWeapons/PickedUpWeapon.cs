
public class PickedUpWeapon
{
    private int _maxMagCapacity;
    private int _currentAmmoCount;
    private float _reloadTimer;
    private float _fireRate;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public int CurrentAmmoCount => _currentAmmoCount;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;

        public PickedUpWeapon(RangedWeapon weapon)
    {
        _maxMagCapacity = weapon.MaxMagCapacity;
        _reloadTimer = weapon.ReloadTimer;
        _fireRate = weapon.FireRate;
        _currentAmmoCount = _maxMagCapacity;
    }

    public void Shoot()
    {
        if (_currentAmmoCount > 0)
            _currentAmmoCount--;
    }

    public void Reload()
    {
        _currentAmmoCount = _maxMagCapacity;
    }
}
