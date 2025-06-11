using UnityEngine;

public abstract class TemporaryBuff : CollectableItem
{
    [SerializeField] protected float BuffDuration; 
    [SerializeField] protected int BuffValue;
}
