using System;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [SerializeField] private float _shootingDistance = 6f;

    private Transform _player;
    private bool _canShoot;
    private bool _playerInArea;
    private float _distanceToPlayer;

    public event UnityAction<Transform> PlayerDetected;
    public event UnityAction<bool> CheckRange;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            OnPlayerDetect(player);
            CheckDistance(_distanceToPlayer);
        }
    }

    private void Update()
    {
        if (_playerInArea)
        {
            _distanceToPlayer = (_player.transform.position - transform.position).magnitude;
            CheckDistance(_distanceToPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            OnPlayerOutOfRange();
    }

    private void OnPlayerDetect(Player player)
    {
        _player = player.transform;
        _playerInArea = true;
        PlayerDetected?.Invoke(_player);
    }

    private void OnPlayerOutOfRange()
    {
        _playerInArea = false;
        _player = null;
    }

    private void CheckDistance(float distance)
    {
        if (distance <= _shootingDistance && !_canShoot)
        {
            _canShoot = true;
            CheckRange?.Invoke(_canShoot);
        }
        else if (distance >= _shootingDistance)
        {
            _canShoot = false;
            CheckRange?.Invoke(_canShoot);
        }
    }
}