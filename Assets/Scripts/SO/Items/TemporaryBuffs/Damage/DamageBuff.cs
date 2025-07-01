using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/DamageBuff", fileName = "New DamageBuff SO")]
public class DamageBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        if (player.TryGetComponent(out Player playerCharacter))
            playerCharacter.ModifiableStats.BuffDamage(BuffValue, BuffDuration);
    }
}
