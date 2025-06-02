using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerShooter _player;
    [SerializeField] private TMP_Text _ammoCounter;

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
