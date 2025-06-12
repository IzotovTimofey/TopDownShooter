using UnityEngine;

public class GamePlayEntityStats : ScriptableObject
{
    [SerializeField] private int _healthValue;
    [SerializeField] private float _speedValue;

    public int HealthValue => _healthValue;
    public float SpeedValue => _speedValue;
}
