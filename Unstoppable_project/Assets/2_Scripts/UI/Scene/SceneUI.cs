using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : UIBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        eUIType = Define.EUIType.Scene;

        theApp.UI.SetOrder(gameObject, false);

        return true;
    }
}
