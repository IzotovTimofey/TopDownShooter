using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/TemporaryBuffs/DamageBuff", fileName = "New DamageBuff SO")]
public class DamageBuff : TemporaryBuff
{
    public override void OnPickUp(GameObject player)
    {
        player.TryGetComponent(out PlayerShooter playerShooter);
        foreach (var weapon in playerShooter.PickedUpWeapons)
        {
            weapon.BuffDamage(BuffValue, BuffDuration);
        }
    }
}
