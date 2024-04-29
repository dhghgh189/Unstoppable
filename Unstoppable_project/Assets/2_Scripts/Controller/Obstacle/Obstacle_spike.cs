using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_spike : Obstacle
{
    SpriteRenderer sr;
    BoxCollider2D col;

    float _minX;
    float _maxX;

    float _updateScoreTick = 0.1f;
    float _currentTick;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        _currentTick = _updateScoreTick;

        return true;
    }

    public void SetSize(int iSize)
    {
        Vector2 newSize = new Vector2(iSize, 1);
        sr.size = newSize;
        col.size = newSize;

        _minX = col.bounds.min.x;
        _maxX = col.bounds.max.x;
    }

    public override void CheckCondition()
    {
        float minX = transform.position.x + _minX;
        float maxX = transform.position.x + _maxX;

        if (_currentTick >= _updateScoreTick)
        {
            if (theApp.Game.Player.PosX > minX)
            {
                theApp.Game.Score += score;
                Vector3 pos = theApp.Game.Player.transform.position;
                pos.x += Random.Range(-0.5f, 0.5f);
                theApp.Game.GenerateScoreEffect(pos, score);
                _currentTick = 0f;
            }
        }

        _currentTick += Time.deltaTime;

        if (theApp.Game.Player.PosX > maxX)
        {
            isPassed = true;
        }
    }
}
