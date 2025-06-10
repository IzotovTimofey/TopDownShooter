using UnityEngine;

public abstract class CollectableItem : ScriptableObject
{
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => _sprite;

    public abstract void OnPickUp(GameObject player);
}
