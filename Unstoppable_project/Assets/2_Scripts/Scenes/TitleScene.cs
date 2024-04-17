using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        eSceneType = Define.ESceneType.TitleScene;

        return true;
    }
}
