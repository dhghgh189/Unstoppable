using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface ILoader<Key>
{
    public Key GetKey();
}

[CreateAssetMenu(fileName = "new Item Data", menuName = "Data/Create Item Data Asset", order = 2)]
public class ItemData : ScriptableObject, ILoader<int>
{
    public int ItemID;
    public string ItemName;
    public Define.EItemType ItemType;
    public Sprite ItemSprite;
    public string ClassName;
    public float Value;
    public float SpawnPercent;

    public int GetKey()
    {
        return ItemID;
    }
}

