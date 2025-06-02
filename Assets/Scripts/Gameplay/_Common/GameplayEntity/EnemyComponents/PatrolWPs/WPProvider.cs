using System.Collections.Generic;
using UnityEngine;

public class WPProvider : MonoBehaviour
{
    [SerializeField] private WPGenerator _generator;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private int _wayPointCount = 2;

    public List<Transform> GetWayPoints()
    {
        for (int i = 0; i < _wayPointCount; i++)
        {
            _wayPoints.Add(_generator.CreateWP());
            
        }
        return _wayPoints;
    }
}
