using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : UIBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        eUIType = Define.EUIType.Popup;

        theApp.UI.SetOrder(gameObject, true);

        return true;
    }
}
