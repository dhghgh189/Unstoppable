using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    [SerializeField] TextMeshPro txtScore;

    public void SetText(string text)
    {
        txtScore.text = text;
    }

    public void OnCallAnimFunc()
    {
        theApp.Res.Destroy(gameObject);
    }
}
