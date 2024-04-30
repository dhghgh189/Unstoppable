using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    GameObject _original;
    IObjectPool<GameObject> _pool;
    Transform _root;
    Transform Root
    {
        get 
        {
            if (_root == null)
            {
                GameObject go = new GameObject($"@{_original.name}_Pool");
                _root = go.transform;
            }
            return _root;
        }
    }

    public Pool(GameObject original)
    {
        _original = original;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public void Push(GameObject go)
    {
        if (go.activeSelf)
            _pool.Release(go);
    }

    public GameObject Pop()
    {
        return _pool.Get();
    }

    public GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(_original);
        go.name = _original.name;
        go.transform.SetParent(Root);
        return go;
    }

    public void OnGet(GameObject go)
    {
        go.SetActive(true);
    }

    public void OnRelease(GameObject go) 
    {
        go.SetActive(false);
    }

    public void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }
}

public class PoolManager
{
    Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public GameObject Pop(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab.name) == false)
            CreatePool(prefab);

        return _pools[prefab.name].Pop();
    }

    public bool Push(GameObject go)
    {
        if (_pools.ContainsKey(go.name) == false)
            return false;

        _pools[go.name].Push(go);
        return true;
    }

    public void CreatePool(GameObject original)
    {
        Pool pool = new Pool(original);
        _pools.Add(original.name, pool);
    }

    public void Clear()
    {
        _pools.Clear();
    }
}
