using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected PlayerController _owner;
    protected ItemData _data;

    public ItemData Data { get { return _data; } }
    public Define.ItemType ItemType { get { return _data.ItemType; } }
    public float Value { get { return _data.Value; } }

    public void SetInfo(ItemData data, PlayerController owner)
    {
        _data = data;
        _owner = owner;
    }

    public abstract void Use();

    public static Item MakeItem(ItemData data, PlayerController owner)
    {
        Item item = null;

        string className = data.ClassName;
        var instance = Activator.CreateInstance(Type.GetType(className));
        if (instance == null)
        {
            Debug.Log("Invalid Class Name!");
            return null;
        }

        item = instance as Item;
        if (item == null)
        {
            Debug.Log("Invalid Item Class!");
            return null;
        }

        item.SetInfo(data, owner);

        return item;
    }
}
