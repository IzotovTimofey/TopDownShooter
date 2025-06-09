using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Item item))
        {
            PickUp(item.GetComponent<ScriptableObject>());
        }
    }

    private void PickUp(ScriptableObject obj)
    {
        if (obj is RangedWeapon)
        {
            
        }
        
    }
}
