using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _patrolRoute;
    
    public List<Transform> GetPatrolRoute()
    {
        return _patrolRoute;
    }
}
