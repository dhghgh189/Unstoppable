using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        eSceneType = Define.ESceneType.GameScene;

        theApp.Game.GameStart();

        theApp.Sound.PlaySound(Define.ESoundType.Bgm, "Audio/Bgm/gameLoop");

        GameObject player = theApp.Res.Instantiate("Prefabs/Player");
        player.transform.position = new Vector3(-2f, -2.25f, 0f);
        theApp.Game.Player = player.GetComponent<PlayerController>();

        theApp.Res.Instantiate("Prefabs/Spawner");

        theApp.UI.ShowSceneUI<UI_GameScene>();

        return true;
    }
}
