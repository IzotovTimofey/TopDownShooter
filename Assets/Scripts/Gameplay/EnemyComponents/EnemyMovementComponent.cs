using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementComponent : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
    }

    private void Update()
    {
        var enemyPos = _enemyStateMachine.SetCurrentWayPoint().transform.position;
        enemyPos.z = 0;
        _navMeshAgent.destination = enemyPos;
        if (new Vector3(transform.position.x, transform.position.y, 0) == enemyPos)
            _enemyStateMachine.SetNextWayPoint();
    }
}
