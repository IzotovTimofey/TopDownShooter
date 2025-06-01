using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementComponent : MonoBehaviour // TODO: Убирай везже приписку Component, как и Manager и Controller. Она не несёт никакой смысловой нагрузки за собой
{
    [SerializeField] private float _destinationReachingPoint = 1.5f;
    [SerializeField] private float _rotationSpeed = 140f;

    private Transform _currentTarget;
    private NavMeshAgent _navMeshAgent;
    private int _currentWP = 0;
    private PlayerController _player;
    private bool _inRange;

    public List<Transform> PatrolWPs;
    public event UnityAction PlayerInRange;
    public bool InRange => _inRange; // TODO: Зачем тебе и эвент, который говорит начать стрелять, и булка, которую ты проверяешь в апдейте, чтобы понять можно ли стрелять. В чём смысл? Оставь уже что то одно

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Start()
    {
        _player = PlayerController.Instance; // TODO: Осуждаю. Все боты с начала игры сразу знают о том, где игрок, и в апдейте проверяют до него расстояние. 
        // OnTriggerEnter для кого придумали? И в апдейте ничего проверять не придётся
    }

    private void Update()
    {
        DetectPlayer();
        SetRotation();
        _navMeshAgent.destination = _currentTarget.position;
    }

    private void SetRotation()
    {
        Vector3 currentVector = transform.right; //TODO: Неиспользуемая переменная
        Vector3 targetVector = _currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void DetectPlayer() // TODO: Убрать полностью, подпишу ошибки внутри, для общего развития, но этого тут быть не должно
    {
        
        if ((_player.transform.position - transform.position).magnitude <= 10f) // 2 раза считаешь растояние до игрока, посчитай 1 раз, сохрани в переменную, работай с ней
        {
            _inRange = false;
            _navMeshAgent.isStopped = false;
            _currentTarget = _player.transform; // Судя по этой строке, у тебя игрок УЖЕ в зоне, но эвент "Игрок в зоне" - вызывается при следующем условии
            // Может стоило тогда назвать эвент "Начать стрелять"?
            if ((_player.transform.position - transform.position).magnitude <= 5f) // Магические, ненастраиваемые числа - 10 и 5
            {
                _inRange = true;
                PlayerInRange?.Invoke(); // Название события не корректное, в зоне он стал уже выше. Либо 2 события надо, в зоне, и начать стрелять, либо изменить логику немного
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            PatrolArea();
            _navMeshAgent.isStopped = false;
        }
    }

    private void PatrolArea()
    {
        _currentTarget = PatrolWPs[_currentWP]; // TODO: Зачем происходит постоянно это переназначение в Update? У тебя не каждый кадр нужно менять _currentTarget
        if ((PatrolWPs[_currentWP].position - transform.position).magnitude <= _destinationReachingPoint) // Это ок, просто разбить на методы нужно. 
        // 1. Смена цели. 2. Проверка достижения таргетной точки, если достиг => Смена цели. Сейчас тут и то, и другое, перегруженный метод
        {
            _currentWP++;
        }

        if (_currentWP >= PatrolWPs.Count)
            _currentWP = 0;
    }
}