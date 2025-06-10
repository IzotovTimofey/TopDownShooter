using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private CollectableItem _item;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.sprite = _item.Sprite;
    }

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
