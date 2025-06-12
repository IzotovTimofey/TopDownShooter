
public class PickedUpWeapon
{
    private int _maxMagCapacity;
    private int _currentAmmoCount;
    private float _reloadTimer;
    private float _fireRate;
    private int _weaponDamage;
    private TimerService _timerService;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public int CurrentAmmoCount => _currentAmmoCount;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;
    public int WeaponDamage => _weaponDamage;

    public void GetTimerService(TimerService timerService)
    {
        _timerService = timerService;
    }
    
    public PickedUpWeapon(RangedWeapon weapon)
    {
        _maxMagCapacity = weapon.MaxMagCapacity;
        _reloadTimer = weapon.ReloadTimer;
        _fireRate = weapon.FireRate;
        _weaponDamage = weapon.WeaponDamage;
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

    public void BuffDamage(int value, float duration)
    {
        _weaponDamage += value;
        _timerService.StartTimer(duration, () => _weaponDamage -=value);
    }
}
