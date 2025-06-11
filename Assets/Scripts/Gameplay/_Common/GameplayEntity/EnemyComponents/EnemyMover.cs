using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 140f;

    private Transform _currentTarget;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        SetRotation();
        _navMeshAgent.destination = _currentTarget.position;
    }

    private void SetRotation()
    {
        Vector3 targetVector = _currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void SetDestination(Transform target)
    {
        _currentTarget = target;
    }

    public void AllowMovement(bool state)
    {
        _navMeshAgent.isStopped = state;
    }

    public void GetSpeedValue(float value)
    {
        _navMeshAgent.speed = value;
    }
}