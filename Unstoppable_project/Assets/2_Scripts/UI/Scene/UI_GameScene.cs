using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_GameScene : SceneUI
{
    enum Texts
    {
        txtScore,
    }

    enum GameObjects
    {
        SlotParent,
        GameOverParent,
    }

    enum Buttons
    {
        btnRetry,
    }

    UI_SlotItem[] slotItems = new UI_SlotItem[Define.ITEM_SLOT_MAX];

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));
        BindObjects(typeof(GameObjects));
        BindButtons(typeof(Buttons));

        BindUIEvent(GetButton((int)Buttons.btnRetry).gameObject, (evt) =>
        {
            theApp.Game.OnBroadCastEvent -= ReceiveBroadCast;
            theApp.Scene.LoadScene(Define.ESceneType.GameScene);
        });

        GetObject((int)GameObjects.GameOverParent).SetActive(false);

        GameObject slotParent = GetObject((int)GameObjects.SlotParent);

        for (int i = 0; i < slotItems.Length; i++)
        {
            UI_SlotItem slotItem = theApp.UI.MakeSubItem<UI_SlotItem>(parent: slotParent.transform);
            slotItem.transform.localScale = Vector3.one;
            slotItems[i] = slotItem;
            slotItem.gameObject.SetActive(false);
        }

        theApp.Game.OnBroadCastEvent -= ReceiveBroadCast;
        theApp.Game.OnBroadCastEvent += ReceiveBroadCast;

        RefreshText(0);

        return true;
    }

    void ReceiveBroadCast(Define.EBroadCastType type, object obj)
    {
        switch (type)
        {
            case Define.EBroadCastType.ChangeScore:
                RefreshText((int)obj);
                break;
            case Define.EBroadCastType.AddItem:
            case Define.EBroadCastType.UseItem:
                RefreshSlotItem(obj as List<Item>);
                break;
            case Define.EBroadCastType.GameOver:
                OnGameOver();
                break;
        }
    }

    void RefreshText(int score)
    {
        GetText((int)Texts.txtScore).text = $"Score : {score}";
    }

    void RefreshSlotItem(List<Item> ItemSlot)
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            if (i <= ItemSlot.Count - 1)
            {
                slotItems[i].gameObject.SetActive(true);
                slotItems[i].SetInfo(ItemSlot[i].Data.ItemSprite);
            }
            else
            {
                slotItems[i].gameObject.SetActive(false);
            }
        }
    }

    void OnGameOver()
    {
        GetObject((int)GameObjects.GameOverParent).SetActive(true);
    }
}
