using UnityEngine;

public class PlayerController : GameplayEntity
{
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
