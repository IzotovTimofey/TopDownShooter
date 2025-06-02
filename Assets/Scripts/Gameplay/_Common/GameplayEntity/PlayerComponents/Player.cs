using UnityEngine;

public class Player : GameplayEntity
{
    // TODO: Missing Animator на игроке
    public static Player Instance { get; private set; }

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
