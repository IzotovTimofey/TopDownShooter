using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShooter _shooter;
    [SerializeField] private PlayerMover _mover;
    
    private TimerService _timerService;
    private ProjectilesFactory _projectilesFactory;

    public void GetReferences(ProjectilesFactory factory, TimerService service)
    {
        _projectilesFactory = factory;
        _timerService = service;
    }

    private void Start()
    {
        _player.ModifiableStats.ProvideTimerService(_timerService);
        _shooter.SetUp(_projectilesFactory, _player.ModifiableStats);
        _mover.SetSpeedValue(_player.ModifiableStats);
    }
}
