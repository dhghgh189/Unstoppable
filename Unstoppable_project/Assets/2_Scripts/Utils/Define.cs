using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ESceneType
    {
        TitleScene,
        GameScene,
    }

    public enum EUIType
    {
        Scene,
        Popup,
    }

    public enum EUIEventType
    {
        Click,
    }

    public enum EInputEventType
    {
        JumpDown,
        JumpUp,
        SlideDown,
        SlideUp,
        UseItem,
    }

    public enum ESoundType
    {
        Bgm,
        Sfx,
        Max,
    }

    public enum ItemType
    {
        Passive,
        Active,
    }
}
