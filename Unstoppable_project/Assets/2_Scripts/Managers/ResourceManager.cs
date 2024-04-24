using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManager 
{
    private Dictionary<string, UnityEngine.Object> _objects = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        string key = path.Substring(path.LastIndexOf('/') + 1);

        if (_objects.TryGetValue(key, out UnityEngine.Object obj))
            return obj as T;

        obj = Resources.Load<T>(path);

        if (obj == null)
        {
            Debug.Log($"Load Error! : {path}");
            return null;
        }

        _objects.Add(obj.name, obj);

        return obj as T;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(path);

        if (prefab == null) 
            return null;

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.name = prefab.name;

        if (parent != null)
            go.transform.SetParent(parent);

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        GameObject.Destroy(go);
    }
}
