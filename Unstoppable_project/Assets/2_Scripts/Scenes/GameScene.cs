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

        theApp.Res.Instantiate("Prefabs/Spawner");

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
