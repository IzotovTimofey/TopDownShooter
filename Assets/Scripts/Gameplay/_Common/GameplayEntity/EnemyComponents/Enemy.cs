using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameplayEntity
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private EnemyShooter _enemyShooter;
    [SerializeField] private Detector _detector;
    [SerializeField] private float _patrolPointReachDistance = 1.5f;

    [SerializeField] private List<Transform> _patrolRoute;
    private int _currentPatrolPoint;

    protected override void OnEnable()
    {
        base.OnEnable();
        _detector.PlayerDetected += OnPlayerDetect;
        _detector.InShootingRange += OnPlayerInRange;
        _detector.PlayerFled += GetCurrentPatrolPoint;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _detector.PlayerDetected -= OnPlayerDetect;
        _detector.InShootingRange -= OnPlayerInRange;
        _detector.PlayerFled -= GetCurrentPatrolPoint;
    }

    public void SetUp(List<Transform> patrolRoute)
    {
        _patrolRoute = patrolRoute;
    }

    private void Start()
    {
        GetCurrentPatrolPoint();
    }

    private void Update()
    {
        if ((_patrolRoute[_currentPatrolPoint].transform.position - transform.position).magnitude <= _patrolPointReachDistance)
        {
            SetNextPatrolPoint();
            GetCurrentPatrolPoint();
        }
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

    private void GetCurrentPatrolPoint()
    {
        _enemyMover.SetDestination(_patrolRoute[_currentPatrolPoint]);
    }

    private void SetNextPatrolPoint()
    {
        _currentPatrolPoint++;
        if (_currentPatrolPoint >= _patrolRoute.Count)
            _currentPatrolPoint = 0;
    }
}