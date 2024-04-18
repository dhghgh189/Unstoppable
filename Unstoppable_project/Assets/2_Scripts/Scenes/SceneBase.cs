using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneBase : InitBase
{
    protected Define.ESceneType eSceneType;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        EventSystem evt = FindAnyObjectByType<EventSystem>();
        if (evt == null)
        {
            GameObject go = new GameObject("@EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        return true;
    }
}
