using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooter : GameplayEntityShooter
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerDirectionProvider _directionProvider;

    [SerializeField] private Transform _shootPoint;

    private BulletsFactory _factory; 
    // TODO: Элементы настройки, почему не филды? Вынести в SO как параметры для настройки

    private float _reloadTimer = 1.2f;
    private int _maxMagCapacity = 12;
    private int _currentAmmoCount;
    
    private bool _isReloading;

    public event UnityAction<int, int> AmmoValueChanged;

    private void Awake()
    {
        _currentAmmoCount = _maxMagCapacity;
        FireRate = 0.5f;
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

    public void SetUp(BulletsFactory factory)
    {
        _factory = factory;
    }

    private void OnReload()
    {
        StartCoroutine(nameof(ReloadingCoroutine));
    }

    protected override IEnumerator ShootingCoroutine() // TODO: После перезарядки стрельба не продолжается если кнопка всё ещё зажата, приходится отжать и зажать снова
    {
        while (IsShooting)
        {
            if (CanShoot && !_isReloading)
            {
                _factory.SpawnBullet(_directionProvider.MouseLookAngle, _shootPoint.position, _directionProvider.IdleDashDirection);
                _currentAmmoCount--;
                AmmoValueChanged?.Invoke(_currentAmmoCount, _maxMagCapacity);
                CanShoot = false;
                StartCoroutine(nameof(LimitFireRateCoroutine));
            }
            if (_currentAmmoCount <= 0)
                yield return StartCoroutine(nameof(ReloadingCoroutine));
            else
                yield return new WaitForSeconds(FireRate);
        }
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