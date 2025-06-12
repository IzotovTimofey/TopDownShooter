using UnityEngine;

public class Player : GameplayEntity
{
    [SerializeField] private PlayerInteractor _interactor;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerShooter _shooter;

    private TimerService _timerService;
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void SetUp(TimerService timerService)
    {
        _timerService = timerService;
    }

    protected override void Awake()
    {
        base.Awake();
        _mover.GetSpeedValue(ModifiableStats);
        _interactor.GetPlayerReference(gameObject);
    }

    private void Start()
    {
        ModifiableStats.GetTimerService(_timerService);
        _shooter.GetTimerService(_timerService);
    }

    public void AddHealth(int value, float duration)
    {
        ModifiableStats.BuffHealth(value,duration);
    }

    public void AddSpeed(int value, float duration)
    {
        ModifiableStats.BuffSpeed(value, duration);
    }
}
