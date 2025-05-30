using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementComponent : MonoBehaviour
{
    [SerializeField] private float _destinationReachingPoint = 0.1f;

    private Transform _currentTarget;
    private EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;
    private Action _onDestinationReached;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        _navMeshAgent.destination = _currentTarget.position;
        if ((_currentTarget.transform.position - _navMeshAgent.transform.position).magnitude <= _destinationReachingPoint)
            _onDestinationReached?.Invoke();
        //var enemyPos = _enemyStateMachine.SetCurrentWayPoint().transform.position;
        //enemyPos.z = 0;
        //_navMeshAgent.destination = enemyPos;
        //if (new Vector3(transform.position.x, transform.position.y, 0) == enemyPos)
        //    _enemyStateMachine.SetNextWayPoint();
    }

    public void SetDestination(Transform target, Action onDestinationChanged)
    {
        _onDestinationReached = onDestinationChanged;
        _currentTarget = target;
    }
}
