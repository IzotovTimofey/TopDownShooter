using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoCounter;

    private PlayerShooter _player;
    
    public void SetUp(PlayerShooter playerShooter)
    {
        _player = playerShooter;
    }

    private void OnEnable()
    {
        _player.AmmoValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.AmmoValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int currentValue, int maxValue)
    {
        _ammoCounter.text = ($"{currentValue}/{maxValue}");
    }
}
