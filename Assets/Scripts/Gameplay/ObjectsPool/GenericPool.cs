using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GenericPool<T> where T : Component
{
    private readonly DiContainer _diContainer;
    private readonly List<T> _objectsPool;
    
    private readonly GameObject _prefab;
    private readonly Transform _poolParent;

    public GenericPool(DiContainer diContainer, GameObject prefab, int initCapacity, Transform parent)
    {
        _diContainer = diContainer;
        
        _objectsPool = new List<T>(initCapacity);
        
        _prefab = prefab;
        _poolParent = new GameObject(prefab.name + "Pool").transform;

        if (parent != null)
        {
            _poolParent.parent = parent;
        }

        if (initCapacity > 0)
        {
            for (var i = 0; i < initCapacity; i++)
            {
                _objectsPool.Add(CreateObjectFromPrefab());
            }
        }
    }

    public T GetObjectFromPool(bool active)
    {
        T result;
        if (_objectsPool.Count > 0)
        {
            result = _objectsPool.Select(o => o).FirstOrDefault(o => !o.gameObject.activeSelf);
            if (result != null)
            {
                result.gameObject.SetActive(active);
                return result;
            }
        }
        
        result = CreateObjectFromPrefab(active);
        return result;
    }
    
    private T CreateObjectFromPrefab(bool activeState = false)
    {
        GameObject createdObj;
        if (_diContainer != null)
        {
            createdObj = _diContainer.InstantiatePrefab(_prefab, _poolParent);	
        }
        else
        {
            createdObj = GameObject.Instantiate(_prefab, _poolParent, false);
        }
        
        createdObj.SetActive(activeState);
        var result = createdObj.GetComponent<T>();
        return result;
    }
}
