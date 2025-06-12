
public class ModifiableStats
{
    private int _health;
    private float _speed;

    private TimerService _timerService;

    public int Health => _health;
    public float Speed => _speed;
    
    public ModifiableStats(GamePlayEntityStats stats)
    {
        _health = stats.HealthValue;
        _speed = stats.SpeedValue;
    }

    public void GetTimerService(TimerService timerService)
    {
        _timerService = timerService;
    }

    public void BuffHealth(int value, float duration)
    {
        _health += value;
        _timerService.StartTimer(duration, () => _health -= value);
    }

    public void BuffSpeed(int value, float duration)
    {
        _speed += value;
        _timerService.StartTimer(duration, () => _speed -= value);
    }
}