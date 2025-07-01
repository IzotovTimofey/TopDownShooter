using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/SpeedBuff", fileName = "New SpeedBuff SO")]
public class SpeedBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        if (player.TryGetComponent(out Player playerCharacter))
            playerCharacter.ModifiableStats.BuffSpeed(BuffValue, BuffDuration);
    }
}