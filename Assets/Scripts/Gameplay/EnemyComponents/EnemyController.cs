using UnityEngine;

public class EnemyController : GameplayEntity
{
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
