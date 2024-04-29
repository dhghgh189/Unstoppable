using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] ScrollData[] datas;

    float offset = 0f;

    void Update()
    {
        if (theApp.Game.isGameOver)
            return;
            
        float t = Time.time;

        for (int i = 0; i < datas.Length; i++)
        {
            offset = Mathf.Repeat(datas[i].scrollSpeed * Time.time, 1f);
            datas[i].mat.mainTextureOffset = new Vector2(offset, 0f);
        }
    }
}

[Serializable]
public class ScrollData
{
    public SpriteRenderer sr;
    public float scrollSpeed;
    public Material mat
    {
        get { return sr.material; }
    }
}
