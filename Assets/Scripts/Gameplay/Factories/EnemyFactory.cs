using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    // TODO: Нарушение SOLID, класс делает сразу несколько вещей, исправить. Каким боком к спавну врагов относится спавн каких то WP непонятных - не ясно
    // + читать сложнее становится сразу, класс засран
    [SerializeField] private BulletsFactory _bulletsFactory;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyPoolParent;
    [SerializeField] private int _enemyPoolCapacity;
    [SerializeField] private int _enemyCount = 2;
    [SerializeField] private EnemyPatrolPoints[] _spawnPoints;

    private GenericPool<Enemy> _enemyPool;
    private int _currentPatrolRoute;

    private void Awake()
    {
        _enemyPool = new(null, _enemyPrefab, _enemyPoolCapacity, _enemyPoolParent);
    }

    private void Start()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetObjectFromPool(true);
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
        enemy.GetComponent<EnemyShooter>().GetBulletsFactoryReference(_bulletsFactory);
        enemy.SetUp(GetPatrolRoute());
        enemyAgent.Warp(_spawnPoints[_currentPatrolRoute].transform.position);
        _currentPatrolRoute++;

        return enemy;

        // TODO: Enemy - NavMeshAgent, нельзя его просто так пихать на конкретную позицию, должно быть использование _agent.Warp(pos)
        // Во 1 - не там, во 2, почему 5, как избегать стен, а если хотим в большем диапазоне?
        // Полное отсутствие гибкости, невозможность настраивать патруль ВНЕ скриптов
        // Создать отдельный Патруль контейнер, расставить такие контейнеры на карте, 
        // и спавнить врагов в эти самые контейнеры. Не должны ВСЕ противники быть патрульными, поведение может быть разным. Патруль - обычно используется для КОНКРЕТНЫХ мест
        // а тут ты сам себе в колено стреляешь, пытаясь придумать систему, котороый никто не станет пользоваться. 
        // По логике - разбить спавн на этапы: 1. Заполнить патруль контейнеры врагами (они там будут патрулировать, сам патруль контейнер пускай их уже направляет)
        // 2. Остальные враги - просто пусть бегают между случайными точками с небольшим диапазоном, чтобы не стояли просто как будто зависли. ЛИБО: Что лучше вообще, подумать
        // о спавне врагов на конркретных местах. В играх никогда враги не спавнятся просто в случайных местах в шутерах таких (если только это не какие то условные арены)
        // Если есть конкретный уровень с конкретным строением - Level дизайнеры обычно сами расставляют врагов по точкам, продумывая бои, продумывая сценарии, и всё такое. 
        // Сейчас у тебя система к этому не готова.
    }

    private List<Transform> GetPatrolRoute()
    {
        return _spawnPoints[_currentPatrolRoute].GetPatrolRoute();
    }
}