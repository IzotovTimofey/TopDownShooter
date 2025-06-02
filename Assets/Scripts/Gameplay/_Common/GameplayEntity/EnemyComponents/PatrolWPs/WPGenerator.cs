using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class WPGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefabWP;
    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;
    [SerializeField] private Transform _container;

    private Vector3 _randomSpawningPosition;

    public Transform CreateWP()
    {
        var wp = GenerateWP();
        Vector3 position = Vector3.zero;
        NavMeshHit hit;
        while (position == Vector3.zero)
        {
            var pos = GetSpawningPosition();
            if (NavMesh.SamplePosition(pos, out hit, 2, NavMesh.AllAreas))
            {
                position = hit.position;
                Debug.Log(position);
            }
        }
        wp.transform.position = position;
        return wp.transform;
    }

    private Vector3 GetSpawningPosition()
    {
        _randomSpawningPosition = new Vector3(
                Random.Range(_boundary1.position.x, _boundary2.position.x),
                Random.Range(_boundary1.position.y, _boundary2.position.y));
        return _randomSpawningPosition;
    }

    private GameObject GenerateWP()
    {
        Debug.Log("CreateWP");
        var wp = Instantiate(_prefabWP, _container);
        return wp;
    }
}
