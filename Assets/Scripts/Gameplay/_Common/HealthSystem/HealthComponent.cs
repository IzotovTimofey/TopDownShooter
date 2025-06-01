using UnityEngine.Events;

public class HealthComponent
{
    private int _maxHealth; 
    private int _currentHealth;

    public event UnityAction EntityDied;

    public HealthComponent(int healthValue)
    {
        _maxHealth = healthValue;
        _currentHealth = healthValue;
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
