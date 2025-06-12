using UnityEngine.Events;

public class Health
{
    private int _maxHealth; 
    private int _currentHealth;

    public event UnityAction EntityDied;

    public Health(ModifiableStats stats)
    {
        _maxHealth = stats.Health;
        _currentHealth = _maxHealth;
    }
    public void Heal(int healValue)
    {
        _currentHealth += healValue;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        _currentHealth -= damageValue;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            EntityDied?.Invoke();
        }
    }
}
