using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/HealthBuff", fileName = "New HealthBuff SO")]
public class HealthBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        if (player.TryGetComponent(out Player playerCharacter))
            playerCharacter.ModifiableStats.BuffHealth(BuffValue, BuffDuration);
    }
}