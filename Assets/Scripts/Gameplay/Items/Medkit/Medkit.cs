using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/Medkit", fileName = "New Medkit SO")]
public class Medkit : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _healingValue = 25;

    public int HealingValue => _healingValue;
}
