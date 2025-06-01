using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform _containerWP;
    [SerializeField] private GameObject _prefabWP;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyPoolParent;
    [SerializeField] private int _enemyPoolCapacity;
    [SerializeField] private int _enemyCount = 2;

    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;
    
    private GenericPool<EnemyController> _enemyPool;
    private Vector3 _randomSpawningPosition;
    
    public static EnemyFactory Instance { get; private set; }
    private void Awake()
    {
        _enemyPool = new(null, _enemyPrefab, _enemyPoolCapacity, _enemyPoolParent);
    }

    private void Start()
    {
        for (int i = 0; i <_enemyCount ; i++)
        {
            SpawnEnemy();
        }
    }

    private EnemyController SpawnEnemy()
    {
        EnemyController enemyController = _enemyPool.GetObjectFromPool(true);
        Vector3 position = Vector3.zero;
        NavMeshHit hit;
        while (position == Vector3.zero)
        {
            var pos = GetSpawningPosition();
            if (NavMesh.SamplePosition(pos, out hit, 2, NavMesh.AllAreas))
            {
                position = hit.position;
            }
        }
        enemyController.transform.position = position;
        var WP1 = ProvideWP(new Vector3(position.x - 5, position.y));
        var WP2 = ProvideWP(new Vector3(position.x + 5, position.y));
        enemyController.GetComponent<EnemyMovementComponent>().PatrolWPs.Add(WP1.transform);
        enemyController.GetComponent<EnemyMovementComponent>().PatrolWPs.Add(WP2.transform);
        return enemyController;
    }

    private Vector3 GetSpawningPosition()
    {
        _randomSpawningPosition = new Vector3(
            Random.Range(_boundary1.position.x, _boundary2.position.x),
            Random.Range(_boundary1.position.y, _boundary2.position.y));
        return _randomSpawningPosition;
    }

    public GameObject ProvideWP(Vector3 position)
    {
        var wp = Instantiate(_prefabWP, _containerWP);
        wp.transform.position = position;
        return wp;
    }
}
