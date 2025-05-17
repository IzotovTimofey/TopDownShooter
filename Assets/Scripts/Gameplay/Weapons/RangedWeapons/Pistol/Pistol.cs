using UnityEngine;

public class Pistol : RangedWeapon
{
    private void Awake()
    {
        _clipSize = 12;
        _fireRate = 1;
        _reloadTimer = 1.5f;
        _bulletSpeed = 20f;
    }
    public override void Shoot()
    {
        GameObject projectile = Instantiate(_bulletPrefab, ShootPoint.position, ShootPoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(ShootPoint.up * _bulletSpeed, ForceMode2D.Impulse);
    }
}
