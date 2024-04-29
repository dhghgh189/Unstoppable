using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public SceneBase CurrentScene { get { return GameObject.FindAnyObjectByType<SceneBase>(); } }

    public void LoadScene(Define.ESceneType type)
    {
        // Scene ���� �� ��� �Ŵ����� �ϰ� Clear
        theApp.Clear();

        // Scene �̸����� LoadScene ȣ��
        SceneManager.LoadScene(type.ToString());
    }
}
