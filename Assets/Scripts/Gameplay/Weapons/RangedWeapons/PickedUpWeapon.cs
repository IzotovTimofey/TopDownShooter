
public class PickedUpWeapon
{
    private int _maxMagCapacity;
    private int _currentAmmo;
    private float _reloadTimer;
    private float _fireRate;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public int CurrentAmmo => _currentAmmo;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;

        public PickedUpWeapon(RangedWeapon weapon)
    {
        _maxMagCapacity = weapon.MaxMagCapacity;
        _reloadTimer = weapon.ReloadTimer;
        _fireRate = weapon.FireRate;
        _currentAmmo = _maxMagCapacity;
    }

    public void Shoot()
    {
        if (_currentAmmo > 0)
            _currentAmmo--;
    }

    public void Reload()
    {
        _currentAmmo = _maxMagCapacity;
    }
}
