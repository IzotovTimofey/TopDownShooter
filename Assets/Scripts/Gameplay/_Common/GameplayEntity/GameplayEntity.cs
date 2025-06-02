using UnityEngine;

public abstract class GameplayEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private Health _health;

    public Health Health => _health;


    protected virtual void Awake()
    {
        _health = new Health(_maxHealth);
    }

    protected virtual void OnEnable()
    {
        _health.EntityDied += OnDeath;
    }

    protected virtual void OnDisable()
    {
        _health.EntityDied -= OnDeath;
    }

    protected abstract void OnDeath();
}
