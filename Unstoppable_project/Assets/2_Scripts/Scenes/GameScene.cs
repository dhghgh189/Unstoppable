using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        eSceneType = Define.ESceneType.GameScene;

        return true;
    }
}
