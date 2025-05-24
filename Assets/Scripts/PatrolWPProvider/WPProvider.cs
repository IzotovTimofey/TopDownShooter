using System.Collections.Generic;
using UnityEngine;

public class WPProvider : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;

    public List<Transform> GetWayPoints()
    {
        return _wayPoints;
    }
}
