using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Item item))
        {
            PickUp(item);
        }
    }

    private void PickUp(Item item)
    {
        item.OnPickUp(_player);
    }
}
