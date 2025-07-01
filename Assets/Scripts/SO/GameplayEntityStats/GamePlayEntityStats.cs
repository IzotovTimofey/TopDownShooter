using UnityEngine;

public class GamePlayEntityStats : ScriptableObject
{
    [SerializeField] private int _healthValue;
    [SerializeField] private float _speedValue;
    [SerializeField] private int _damageValue;

    public int HealthValue => _healthValue;
    public float SpeedValue => _speedValue;
    public int DamageValue => _damageValue;
}
