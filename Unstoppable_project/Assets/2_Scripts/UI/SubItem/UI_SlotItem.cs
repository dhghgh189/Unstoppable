using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotItem : UIBase
{
    enum Images
    {
        ItemIcon,
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImages(typeof(Images));

        return true;
    }

    public void SetInfo(Sprite itemIcon)
    {
        GetImage((int)Images.ItemIcon).sprite = itemIcon;
        GetComponent<Image>().SetNativeSize();
    }
}
