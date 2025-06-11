using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/HealthBuff", fileName = "New HealthBuff SO")]
public class HealthBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out Player playerController);
    }
}
