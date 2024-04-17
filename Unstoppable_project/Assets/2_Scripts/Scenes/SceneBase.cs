using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : InitBase
{
    protected Define.ESceneType eSceneType;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
