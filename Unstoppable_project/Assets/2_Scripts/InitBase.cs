using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBase : MonoBehaviour
{
    protected bool isInit = false;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (isInit)
            return false;

        isInit = true;

        return true;
    }
}
