using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private float _playerDetectionDistance = 30f;
    [SerializeField] private float _attackDistance = 15f;
    [SerializeField] WPProvider _provider;

    private List<Transform> _wayPoints;
    private int _currentWayPoint = 0;

    private void Start()
    {
        _wayPoints = _provider.GetWayPoints();   
    }

    public Transform SetCurrentWayPoint()
    {
        return _wayPoints[_currentWayPoint];
    }

    public void SetNextWayPoint()
    {
        _currentWayPoint++;
        if (_currentWayPoint >= _wayPoints.Count)
            _currentWayPoint = 0;
    }
}
