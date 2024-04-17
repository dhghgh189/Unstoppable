using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class theApp : MonoBehaviour
{
    private static theApp _instance;

    public static theApp Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@theApp");
                if (go == null)
                {
                    go = new GameObject("@theApp");
                    go.AddComponent<theApp>();
                }
                _instance = go.GetComponent<theApp>();

                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    private ResourceManager _res = new ResourceManager();
    public static ResourceManager Res
    {
        get { return Instance._res; }
    }
}
