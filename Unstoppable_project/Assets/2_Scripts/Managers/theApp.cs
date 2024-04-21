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

                _instance._sound.Init();
            }

            return _instance;
        }
    }

    private ResourceManager _res = new ResourceManager();
    private UIManager _ui = new UIManager();
    private InputManager _input = new InputManager();
    private SoundManager _sound = new SoundManager();

    public static ResourceManager Res
    {
        get { return Instance._res; }
    }
    public static UIManager UI
    {
        get { return Instance._ui; }
    }
    public static InputManager Input
    { 
        get { return Instance._input; } 
    }
    public static SoundManager Sound
    { 
        get { return Instance._sound; } 
    }

    private void Update()
    {
        _input.OnUpdate();
    }
}
