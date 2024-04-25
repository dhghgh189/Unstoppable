using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    float _score = 0;
    public float Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnBroadCastEvent?.Invoke(Define.EBroadCastType.ChangeScore, value);
        }
    }

    public Action<Define.EBroadCastType, object> OnBroadCastEvent = null;

    public void OnBroadCastFunc(Define.EBroadCastType type, object obj)
    {
        OnBroadCastEvent?.Invoke(type, obj);
    }
}
