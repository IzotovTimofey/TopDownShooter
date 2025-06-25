using System.Collections;
using UnityEngine;

public abstract class GameplayEntityShooter : MonoBehaviour
{
    [SerializeField] protected RangedWeapon StartingWeapon;
    protected bool IsReloading;
    protected PickedUpWeapon CurrentWeapon;
    protected ProjectilesFactory ProjectilesFactory;

    private bool _isShooting;
    private bool _canShoot = true;
    private Coroutine _shootingCoroutine;
    private Coroutine _reloadingCoroutine;

    protected virtual void Awake()
    {
        CurrentWeapon = new PickedUpWeapon(StartingWeapon);
    }

    public void SetUp(ProjectilesFactory factory)
    {
        ProjectilesFactory = factory;
    }

    public void Shoot(bool state)
    {
        _isShooting = state;
        if (_isShooting)
        {
            _shootingCoroutine = StartCoroutine(ShootingCoroutine());
        }
        else
        {
            if (_shootingCoroutine != null)
                StopCoroutine(_shootingCoroutine);
        }
    }

    private IEnumerator ShootingCoroutine()
    {
        while (_isShooting)
        {
            if (_canShoot && !IsReloading)
            {
                OnShoot();
                CurrentWeapon.Shoot();
                OnReload();
                _canShoot = false;
                _reloadingCoroutine = StartCoroutine(ReloadingCoroutine());
                yield return _reloadingCoroutine;
            }
            else
                yield return null;
        }
    }

    protected IEnumerator ReloadingCoroutine()
    {
        if (CurrentWeapon.CurrentAmmoCount <= 0 || IsReloading)
        {
            yield return new WaitForSeconds(CurrentWeapon.ReloadTimer);
            CurrentWeapon.Reload();
            IsReloading = false;
            OnReload();
        }
        else
            yield return new WaitForSeconds(CurrentWeapon.FireRate);

        _canShoot = true;
    }

    protected abstract void OnReload();

    protected abstract void OnShoot();
}