using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
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

    public enum EItemType
    {
        Passive,
        Active,
    }

    public enum EBroadCastType
    {
        ChangeScore,
        AddItem,
        UseItem,
        GameOver,
    }

    public const int ITEM_SLOT_MAX = 2;
}
