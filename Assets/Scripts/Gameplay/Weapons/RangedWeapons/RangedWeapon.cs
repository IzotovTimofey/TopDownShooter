using UnityEngine;

public abstract class RangedWeapon : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _maxMagCapacity;
    [SerializeField] private float _reloadTimer;
    [SerializeField] private float _fireRate;
    
    public int MaxMagCapacity => _maxMagCapacity;
    public float ReloadTimer => _reloadTimer;
    public float FireRate => _fireRate;
    public Sprite Sprite => _sprite;
}
