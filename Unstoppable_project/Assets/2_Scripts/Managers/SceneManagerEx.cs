using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public SceneBase CurrentScene { get { return GameObject.FindAnyObjectByType<SceneBase>(); } }

    public void LoadScene(Define.ESceneType type)
    {
        // Scene 변경 시 모든 매니저를 일괄 Clear
        theApp.Clear();

        // Scene 이름으로 LoadScene 호출
        SceneManager.LoadScene(type.ToString());
    }
}
