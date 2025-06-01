using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    // TODO: Нарушение SOLID, класс делает сразу несколько вещей, исправить. Каким боком к спавну врагов относится спавн каких то WP непонятных - не ясно
    // + читать сложнее становится сразу, класс засран
    [SerializeField] private Transform _containerWP;
    [SerializeField] private GameObject _prefabWP;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyPoolParent;
    [SerializeField] private int _enemyPoolCapacity;
    [SerializeField] private int _enemyCount = 2;

    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;
    
    private GenericPool<EnemyController> _enemyPool;
    private Vector3 _randomSpawningPosition;
    
    public static EnemyFactory Instance { get; private set; } // TODO: 3ий синглтон за игру.
    private void Awake()
    {
        _enemyPool = new(null, _enemyPrefab, _enemyPoolCapacity, _enemyPoolParent);
    }

    private void Start()
    {
        for (int i = 0; i <_enemyCount ; i++)
        {
            SpawnEnemy();
        }
    }

    private EnemyController SpawnEnemy()
    {
        EnemyController enemyController = _enemyPool.GetObjectFromPool(true);
        Vector3 position = Vector3.zero;
        //NavMeshHit hit; // Это объявление можно сделать внутри метода (см 45 стр)
        while (position == Vector3.zero)
        {
            var pos = GetSpawningPosition();
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 2, NavMesh.AllAreas))
            {
                position = hit.position;
            }
        }
        enemyController.transform.position = position; // TODO: Enemy - NavMeshAgent, нельзя его просто так пихать на конкретную позицию, должно быть использование _agent.Warp(pos)
        
        var WP1 = ProvideWP(new Vector3(position.x - 5, position.y)); // TODO: Почему 5? магическое число. В целом сомнительный метод генерации WP
        var WP2 = ProvideWP(new Vector3(position.x + 5, position.y)); // Во 1 - не там, во 2, почему 5, как избегать стен, а если хотим в большем диапазоне?
        enemyController.GetComponent<EnemyMovementComponent>().PatrolWPs.Add(WP1.transform); // Полное отсутствие гибкости, невозможность настраивать патруль ВНЕ скриптов
        enemyController.GetComponent<EnemyMovementComponent>().PatrolWPs.Add(WP2.transform); // Создать отдельный Патруль контейнер, расставить такие контейнеры на карте, 
        // и спавнить врагов в эти самые контейнеры. Не должны ВСЕ противники быть патрульными, поведение может быть разным. Патруль - обычно используется для КОНКРЕТНЫХ мест
        // а тут ты сам себе в колено стреляешь, пытаясь придумать систему, котороый никто не станет пользоваться. 
        // По логике - разбить спавн на этапы: 1. Заполнить патруль контейнеры врагами (они там будут патрулировать, сам патруль контейнер пускай их уже направляет)
        // 2. Остальные враги - просто пусть бегают между случайными точками с небольшим диапазоном, чтобы не стояли просто как будто зависли. ЛИБО: Что лучше вообще, подумать
        // о спавне врагов на конркретных местах. В играх никогда враги не спавнятся просто в случайных местах в шутерах таких (если только это не какие то условные арены)
        // Если есть конкретный уровень с конкретным строением - Level дизайнеры обычно сами расставляют врагов по точкам, продумывая бои, продумывая сценарии, и всё такое. 
        // Сейчас у тебя система к этому не готова.
        return enemyController;
    }

    private Vector3 GetSpawningPosition()
    {
        _randomSpawningPosition = new Vector3(
            Random.Range(_boundary1.position.x, _boundary2.position.x),
            Random.Range(_boundary1.position.y, _boundary2.position.y));
        return _randomSpawningPosition;
    }

    public GameObject ProvideWP(Vector3 position) // TODO: Неверное название метода, он делает не это
    {// Модификатор доступа, почему паблик?
        var wp = Instantiate(_prefabWP, _containerWP);
        wp.transform.position = position;
        return wp;
    }
}
