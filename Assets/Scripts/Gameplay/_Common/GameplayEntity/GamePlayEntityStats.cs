using UnityEngine;

public class GamePlayEntityStats : ScriptableObject
{
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damageValue;
    [SerializeField] private float _speedValue;

    public int HealthValue => _healthValue;
    public int DamageValue => _damageValue;
    public float SpeedValue => _speedValue;
}
