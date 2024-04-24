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
            OnScoreChange?.Invoke(value);

            // TEST
            Debug.Log($"Change Score : {value}");
        }
    }

    public Action<float> OnScoreChange = null;
}
