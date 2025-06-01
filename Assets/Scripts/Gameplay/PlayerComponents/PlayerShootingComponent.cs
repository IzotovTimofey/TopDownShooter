using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootingComponent : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;
    [SerializeField] private BulletsFactory _factory;

    [SerializeField] private Transform _shootPoint;

    private float _fireRate = 0.5f; // TODO: Элементы настройки, почему не филды? Вынести в SO как параметры для настройки
    private float _reloadTimer = 1.2f;
    private int _maxMagCapacity = 12;
    private int _currentAmmoCount;

    private bool _isShooting;
    private bool _isReloading;

    public event UnityAction<int, int> AmmoValueChanged;

    private void Awake()
    {
        _currentAmmoCount = _maxMagCapacity;
    }

    private void OnEnable()
    {
        _reader.OnPlayerShoot += TriggerShooting;
        _reader.OnPlayerReload += OnReload;
    }

    private void OnDisable()
    {
        _reader.OnPlayerShoot -= TriggerShooting;
        _reader.OnPlayerReload -= OnReload;
    }

    private void TriggerShooting(bool state)
    {
        _isShooting = state;
        if (_isShooting)
            StartCoroutine(nameof(ShootingCoroutine)); // TODO: Запомнить и проверить остальные места. Когда тебе нужно остановить запущенную корутину,
                // Запускай и останавливай её через "nameof". иначе она НЕ остановится, StopCoroutine НЕ сработает
        else
            StopCoroutine(nameof(ShootingCoroutine));
    }

    private void OnReload()
    {
        StopCoroutine(ShootingCoroutine());
        StartCoroutine(ReloadingCoroutine());
    }

    private IEnumerator ShootingCoroutine() // TODO: После перезарядки стрельба не продолжается если кнопка всё ещё зажата, приходится отжать и зажать снова
    {
        while (_isShooting && _currentAmmoCount > 0 && !_isReloading)
        {
            _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection);
            _currentAmmoCount--;
            AmmoValueChanged?.Invoke(_currentAmmoCount, _maxMagCapacity);
            yield return new WaitForSeconds(_fireRate);
        }
        if (_currentAmmoCount <= 0)
            OnReload();
    }
    private IEnumerator ReloadingCoroutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTimer);
        _currentAmmoCount = _maxMagCapacity;
        AmmoValueChanged?.Invoke(_currentAmmoCount, _maxMagCapacity);
        _isReloading = false;
    }
}
