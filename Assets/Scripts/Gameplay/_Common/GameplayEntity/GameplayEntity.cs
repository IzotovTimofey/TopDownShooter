using UnityEngine;

public abstract class GameplayEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private HealthComponent _healthComponent;

    public HealthComponent HealthComponent => _healthComponent;


    private void Awake()
    {
        _healthComponent = new HealthComponent(_maxHealth);
    }

    private void OnEnable()
    {
        _healthComponent.EntityDied += OnDeath;
    }

    private void OnDisable()
    {
        _healthComponent.EntityDied -= OnDeath;
    }

    protected abstract void OnDeath();
}
