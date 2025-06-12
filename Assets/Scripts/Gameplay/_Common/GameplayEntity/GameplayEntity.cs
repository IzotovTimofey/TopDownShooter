using UnityEngine;

public abstract class GameplayEntity : MonoBehaviour
{
    [SerializeField] private GamePlayEntityStats _stats;
    protected ModifiableStats ModifiableStats;

    private Health _health;

    public Health Health => _health;

    protected virtual void Awake()
    {
        ModifiableStats = new ModifiableStats(_stats);
        _health = new Health(ModifiableStats);
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
