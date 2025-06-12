using Unity.Cinemachine;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private TimerService _timerService;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BulletsFactory _bulletsFactory;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private AmmoDisplay _ammoDisplay;
    
    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrefab, _container);
        var playerShooter = player.GetComponent<PlayerShooter>();
        var playerController = player.GetComponent<Player>();
        playerController.SetUp(_timerService);
        _camera.Follow = player.transform;
        _ammoDisplay.SetUp(playerShooter);
        playerShooter.SetUp(_bulletsFactory);
        player.transform.position = _spawnPoint.position;
    }
}
