using System;
using System.Collections;
using UnityEngine;

public class PlayerShootingComponent : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;
    [SerializeField] private BulletsFactory _factory;

    [SerializeField] private Transform _shootPoint;

    private float _fireRate = 0.5f;
    private float _reloadTimer = 1.2f;
    private int _maxMagCapacity = 12;
    private int _currentAmmoCount;

    private bool _isReloading;
    private bool _isShooting;

    private void Awake()
    {
        _currentAmmoCount = _maxMagCapacity;
    }

    private void OnEnable()
    {
        _reader.OnPlayerShoot += Shoot;
        _reader.OnPlayerReload += OnReload;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= Shoot;
        _reader.OnPlayerReload -= OnReload;
    }

    private void Shoot(bool state)
    {
        _isShooting = state;
        if (_isShooting)
            StartCoroutine(ShootingCoroutine());
        else
            StopCoroutine(ShootingCoroutine());
        if (_currentAmmoCount == 0)
            StartCoroutine(ReloadingCoroutine());
    }

    private void OnReload()
    {
        StopCoroutine(ShootingCoroutine());
        StartCoroutine(ReloadingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        while (_isShooting)
        {
            _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection);
            _currentAmmoCount--;
            yield return new WaitForSeconds(_fireRate);
        }
    }
    private IEnumerator ReloadingCoroutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTimer);
        _currentAmmoCount = _maxMagCapacity;
        _isReloading = false;
    }
}
