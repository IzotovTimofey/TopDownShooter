using UnityEngine;

public class PlayerController : GameplayEntity
{
    // TODO: Missing Animator на игроке
    public static PlayerController Instance { get; private set; }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}
