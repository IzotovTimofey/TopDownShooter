using UnityEngine;

public abstract class GameplayEntity : MonoBehaviour
{
    [SerializeField] private GamePlayEntityStats _stats;
    private ModifiableStats _modifiableStats;

    private Health _health;

    public Health Health => _health;
    public ModifiableStats ModifiableStats => _modifiableStats;

    protected virtual void Awake()
    {
        _modifiableStats = new ModifiableStats(_stats);
        _health = new Health(_modifiableStats);
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
