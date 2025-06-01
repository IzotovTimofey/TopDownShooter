using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyShootingComponent : MonoBehaviour // TODO: Почему не унаследовано вместе с PlayerShooting от единого Shooting компонента-родителя? Поведение идентичное, 
// Способ триггера только отличается. Сейчас дубляж кода
{
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _fireRate = 2;

    private BulletsFactory _bulletFactory;
    private bool _isShooting = false;

    private void Start()
    {
        _bulletFactory = BulletsFactory.Instance;
    }

    public void Shoot()
    {
        _isShooting = true;
        StartCoroutine(ShootingCoroutine()); 
    }

    private IEnumerator ShootingCoroutine()
    {
        while (_isShooting)
        {
            _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right);
            yield return new WaitForSeconds(_fireRate);
            Debug.Log("Shooting");
        }
    }

    public void StopShooting()
    {
        _isShooting = false;
    }

}
