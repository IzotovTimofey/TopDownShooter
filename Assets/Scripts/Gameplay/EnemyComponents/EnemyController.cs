using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class EnemyController : GameplayEntity
{
    [SerializeField] private GameObject _prefabWP;
    [SerializeField] private BulletsFactory _bulletFactory;
    [SerializeField] private float _fireRate = 2;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayerController _player;

    private bool _isShooting = false;
    private NavMeshAgent _agent;
    private Vector3 _startingPosition;
    private Transform _currentTarget;
    private Transform _startingWPTransform;
    private Transform _rightWP;
    private Transform _leftWP;
    private List<Transform> _listWPs = new ();
    private int _currentWP = 0;

    // Патрулирование между точками
    // Движение за игроком
    // Обнаружение игрока
    // Поворот в сторону цели
    // Стрельба
    protected override void Awake()
    {
        base.Awake();
        _startingPosition = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        CreateStartingWP();
        CreatePatrolWPs();
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        Shoot();
    }

    private void Update()
    {
        SetRotation();
        DetectPlayer();
        MoveTo();
    }

    private void Shoot()
    {
        _isShooting = true;
        StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        while (_isShooting)
        {
            _bulletFactory.SpawnBullet(transform.rotation, _shootPoint.position, transform.right);
            yield return new WaitForSeconds(_fireRate);
        }
    }
    private void SetRotation()
    {
        Vector3 currentVector = transform.right;
        Vector3 targetVector = _currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0,0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
    }

    private void MoveTo()
    {
        _agent.destination = _currentTarget.transform.position;
    }

    private void DetectPlayer()
    {
        if ((_player.transform.position - transform.position).magnitude <= 10f)
        {
            _currentTarget = _player.transform;
        }
        else
        {
            PatrolArea();
        }
        
    }

    private void CreateStartingWP()
    {
        var wp = Instantiate(_prefabWP, _startingPosition, quaternion.identity);
        _startingWPTransform = wp.transform;
        _currentTarget = _startingWPTransform;
        _listWPs.Add(wp.transform);
    }

    private void CreatePatrolWPs()
    {
        Vector3 rightPosition = new Vector3(_startingPosition.x + 5, _startingPosition.y);
        Vector3 leftPosition = new Vector3(_startingPosition.x - 5, _startingPosition.y);
        _rightWP = Instantiate(_prefabWP, rightPosition, Quaternion.identity).transform;
        _leftWP = Instantiate(_prefabWP, leftPosition, Quaternion.identity).transform;
        _listWPs.Add(_rightWP.transform);
        _listWPs.Add(_leftWP.transform);
    }

    private void PatrolArea()
    {
        _currentTarget = _listWPs[_currentWP];
        if ((_listWPs[_currentWP].position - transform.position).magnitude <= 2f)
        {
            _currentWP++;
        }
        if (_currentWP >= _listWPs.Count)
            _currentWP = 0;
    }
}
