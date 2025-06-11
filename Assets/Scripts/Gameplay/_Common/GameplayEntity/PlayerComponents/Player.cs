using UnityEngine;

public class Player : GameplayEntity
{
    [SerializeField] private PlayerInteractor _interactor;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerShooter _shooter;

    public ModifiableStats Mstats => ModifiableStats;
    
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        _mover.GetSpeedValue(ModifiableStats.Speed);
        _interactor.GetPlayerReference(gameObject);
        _shooter.GetDamageModifierValue(ModifiableStats.Damage);
    }
}
