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

        theApp.Sound.PlaySound(Define.ESoundType.Bgm, "Audio/Bgm/gameLoop");

        GameObject player = theApp.Res.Instantiate("Prefabs/Player");
        player.transform.position = new Vector3(-2f, -2.25f, 0f);

        theApp.Res.Instantiate("Prefabs/Spawner");

        theApp.UI.ShowSceneUI<UI_GameScene>();

        // TEST
        //int itemID = 100; // fish item id
        int itemID = 200; // feather item id
        float itemMoveSpeed = 3f;
        float offsetX = 3.5f;
        float offsetY = -0.5f;
        for (int i = 0; i < 3; i++)
        {
            GameObject go = theApp.Res.Instantiate("Prefabs/ItemHolder");
            go.transform.position = new Vector3(offsetX, offsetY, 0f);

            ItemHolder itemHolder = go.GetComponent<ItemHolder>();
            itemHolder.SetInfo(itemID, itemMoveSpeed);

            offsetX += 3f;
        }

        return true;
    }
}
