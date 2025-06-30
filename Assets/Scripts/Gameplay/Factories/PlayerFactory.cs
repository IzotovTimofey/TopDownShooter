using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private PlayerInstaller _playerPrefab;
    [SerializeField] private TimerService _timerService;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ProjectilesFactory _projectilesFactory;
    
    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrefab, _container);
        player.GetReferences(_projectilesFactory, _timerService);
        player.transform.position = _spawnPoint.position;
    }
}
