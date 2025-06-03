using System.Collections;
using UnityEngine;

public class
    EnemyShooter : MonoBehaviour // TODO: Почему не унаследовано вместе с PlayerShooting от единого Shooting компонента-родителя? Поведение идентичное, 
// Способ триггера только отличается. Сейчас дубляж кода
{
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _fireRate = 2;

    private BulletsFactory _bulletFactory;
    private bool _isShooting;

    public void GetBulletsFactoryReference(BulletsFactory factory)
    {
        _bulletFactory = factory;
    }

    public void Shoot(bool state)
    {
        _isShooting = state;
        if (_isShooting)
        {
            StartCoroutine(nameof(ShootingCoroutine));
        }
        else
            StopCoroutine(nameof(ShootingCoroutine));
    }

    private IEnumerator ShootingCoroutine()
    {
        while (_isShooting)
        {
            _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}