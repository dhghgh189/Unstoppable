using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager
{
    PlayerController _player;
    public PlayerController Player 
    { 
        get { return _player; } 
        set 
        { 
            _player = value; 
        } 
    }

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

    public bool isGameOver { get; private set; } = false;

    public Action<Define.EBroadCastType, object> OnBroadCastEvent = null;

    public void OnBroadCastFunc(Define.EBroadCastType type, object obj)
    {
        OnBroadCastEvent?.Invoke(type, obj);
    }

    public void GameStart()
    {
        _score = 0;
        isGameOver = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        OnBroadCastEvent?.Invoke(Define.EBroadCastType.GameOver, null);
    }

    public void GenerateScoreEffect(Vector3 pos, float score)
    {
        GameObject go = theApp.Res.Instantiate("Prefabs/Effects/ScoreEffect", pooling: true);
        go.transform.position = pos;
        ScoreEffect effect = go.GetComponent<ScoreEffect>();
        effect.SetText($"{score}");
    }
}
