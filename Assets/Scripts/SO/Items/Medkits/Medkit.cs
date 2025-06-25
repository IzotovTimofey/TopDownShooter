using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/Medkit", fileName = "New Medkit SO")]
public class Medkit : CollectableItem
{
    [SerializeField] private int _healingValue = 25;

    public int HealingValue => _healingValue;

    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out Player playerHealth);
        playerHealth.Health.Heal(_healingValue);
    }
}
