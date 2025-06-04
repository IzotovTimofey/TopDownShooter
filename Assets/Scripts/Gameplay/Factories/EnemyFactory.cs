using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private BulletsFactory _bulletsFactory;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyPoolParent;
    [SerializeField] private int _enemyPoolCapacity;
    [SerializeField] private int _enemyCount = 2;
    [SerializeField] private EnemyPatrolPoints[] _spawnPoints;

    private GenericPool<Enemy> _enemyPool;
    private int _currentPatrolRoute;

    private void Awake()
    {
        _enemyPool = new(null, _enemyPrefab, _enemyPoolCapacity, _enemyPoolParent);
    }

    private void Start()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetObjectFromPool(true);
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
        enemy.GetComponent<EnemyShooter>().GetBulletsFactoryReference(_bulletsFactory);
        enemy.SetUp(GetPatrolRoute());
        enemyAgent.Warp(_spawnPoints[_currentPatrolRoute].transform.position);
        _currentPatrolRoute++;

        return enemy;
    }

    private List<Transform> GetPatrolRoute()
    {
        return _spawnPoints[_currentPatrolRoute].GetPatrolRoute();
    }
}