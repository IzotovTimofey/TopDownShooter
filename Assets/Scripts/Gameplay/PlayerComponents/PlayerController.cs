using UnityEngine;

public class PlayerController : GameplayEntity
{
    public static PlayerController Instance { get; private set; }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
