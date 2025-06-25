using UnityEngine;

public abstract class CollectableItem : ScriptableObject
{
    public abstract void OnPickUp(GameObject player);
}
