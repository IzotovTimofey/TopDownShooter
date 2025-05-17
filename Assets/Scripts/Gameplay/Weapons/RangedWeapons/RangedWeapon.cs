using UnityEngine;

public abstract class RangedWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject _bulletPrefab;

    protected int _clipSize;
    protected float _reloadTimer;
    protected float _fireRate;
    protected float _bulletSpeed;
    
    public Transform ShootPoint;
    public abstract void Shoot();
}
