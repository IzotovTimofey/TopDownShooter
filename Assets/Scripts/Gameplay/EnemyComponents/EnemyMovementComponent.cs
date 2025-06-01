using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementComponent : MonoBehaviour
{
    [SerializeField] private float _destinationReachingPoint = 1.5f;
    [SerializeField] private float _rotationSpeed = 140f;

    private Transform _currentTarget;
    private NavMeshAgent _navMeshAgent;
    private int _currentWP = 0;
    private PlayerController _player;
    private bool _inRange;

    public List<Transform> PatrolWPs;
    public event UnityAction PlayerInRange;
    public bool InRange => _inRange;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void Update()
    {
        DetectPlayer();
        SetRotation();
        _navMeshAgent.destination = _currentTarget.position;
    }

    private void SetRotation()
    {
        Vector3 currentVector = transform.right;
        Vector3 targetVector = _currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void DetectPlayer()
    {
        if ((_player.transform.position - transform.position).magnitude <= 10f)
        {
            _inRange = false;
            _navMeshAgent.isStopped = false;
            _currentTarget = _player.transform;
            if ((_player.transform.position - transform.position).magnitude <= 5f)
            {
                _inRange = true;
                PlayerInRange?.Invoke();
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            PatrolArea();
            _navMeshAgent.isStopped = false;
        }
    }

    private void PatrolArea()
    {
        _currentTarget = PatrolWPs[_currentWP];
        if ((PatrolWPs[_currentWP].position - transform.position).magnitude <= _destinationReachingPoint)
        {
            _currentWP++;
        }

        if (_currentWP >= PatrolWPs.Count)
            _currentWP = 0;
    }
}