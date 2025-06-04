
public class Player : GameplayEntity
{
    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
