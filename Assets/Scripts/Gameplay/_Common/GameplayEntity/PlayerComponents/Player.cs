using UnityEngine;

public class Player : GameplayEntity
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerShooter _shooter;

    private TimerService _timerService;
    private ProjectilesFactory _projectilesFactory;

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void AddHealth(int value, float duration)
    {
        ModifiableStats.BuffHealth(value, duration);
    }

    public void AddSpeed(int value, float duration)
    {
        ModifiableStats.BuffSpeed(value, duration);
    }
}