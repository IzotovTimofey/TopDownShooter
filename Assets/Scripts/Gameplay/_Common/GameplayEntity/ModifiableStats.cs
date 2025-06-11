
public class ModifiableStats
{
    private int _health;
    private int _damage;
    private float _speed;

    public int Health => _health;
    public int Damage => _damage;
    public float Speed => _speed;
    public ModifiableStats(GamePlayEntityStats stats)
    {
        _health = stats.HealthValue;
        _damage = stats.DamageValue;
        _speed = stats.SpeedValue;
    }
}
