using UnityEngine;

public class Enemy : GameplayEntity
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private EnemyShooter _enemyShooter;
    [SerializeField] private Detector _detector;

    protected override void OnEnable()
    {
        base.OnEnable();
        _detector.PlayerDetected += OnPlayerDetect;
        _detector.CheckRange += OnPlayerInRange;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _detector.PlayerDetected -= OnPlayerDetect;
        _detector.CheckRange -= OnPlayerInRange;
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnPlayerDetect(Transform player)
    {
        _enemyMover.SetDestination(player);
    }

    private void OnPlayerInRange(bool state)
    {
        _enemyShooter.Shoot(state);
        _enemyMover.AllowMovement(state);
    }
}