
public class Player : GameplayEntity
{
    private TimerService _timerService;
    private ProjectilesFactory _projectilesFactory;

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}