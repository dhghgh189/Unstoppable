using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_spike : Obstacle
{
    SpriteRenderer sr;
    BoxCollider2D col;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        return true;
    }

    public void SetSize(int iSize)
    {
        Vector2 newSize = new Vector2(iSize, 1);
        sr.size = newSize;
        col.size = newSize;
    }
}
