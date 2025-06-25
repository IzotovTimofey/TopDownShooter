using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private CollectableItem _item;
    
    public void OnPickUp(GameObject player)
    {
        _item.OnPickUp(player);
        Release();
    }

    private void Release()
    {
        gameObject.SetActive(false);
    }
}
