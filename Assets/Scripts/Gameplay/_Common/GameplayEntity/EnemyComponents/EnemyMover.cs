using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _destinationReachingPoint = 1.5f;
    [SerializeField] private float _rotationSpeed = 140f;

    private Transform _currentTarget;
    private NavMeshAgent _navMeshAgent;
    private int _currentWP = 0;

    public List<Transform> PatrolWPs;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        SetRotation();
        _navMeshAgent.destination = _currentTarget.position;
    }

    private void SetRotation()
    {
        Vector3 targetVector = _currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void PatrolArea()
    {
        _currentTarget = PatrolWPs[_currentWP]; // TODO: Зачем происходит постоянно это переназначение в Update? У тебя не каждый кадр нужно менять _currentTarget
        if ((PatrolWPs[_currentWP].position - transform.position).magnitude <= _destinationReachingPoint) // Это ок, просто разбить на методы нужно. 
        // 1. Смена цели. 2. Проверка достижения таргетной точки, если достиг => Смена цели. Сейчас тут и то, и другое, перегруженный метод
        {
            _currentWP++;
        }

        if (_currentWP >= PatrolWPs.Count)
            _currentWP = 0;
    }
    
    public void SetDestination(Transform target)
    {
        _currentTarget = target;
    }

    public void AllowMovement(bool state)
    {
        _navMeshAgent.isStopped = state;
    }
}