using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/SpeedBuff", fileName = "New SpeedBuff SO")]
public class SpeedBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out Player playerController);
        playerController.AddSpeed(BuffValue, BuffDuration);
    }
}
