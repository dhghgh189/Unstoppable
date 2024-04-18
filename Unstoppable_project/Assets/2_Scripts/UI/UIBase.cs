using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBase : InitBase
{
    protected Define.EUIType eUIType;

    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    protected void Bind<T>(Type enumType) where T : UnityEngine.Object
    {
        string[] objNames = Enum.GetNames(enumType);

        if (objNames.Length <= 0)
        {
            Debug.Log($"Bind Error! : {enumType.Name}");
            return;
        }

        UnityEngine.Object[] objects = new UnityEngine.Object[objNames.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < objNames.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, objNames[i]);
            else
                objects[i] = Util.FindChild<T>(gameObject, objNames[i]);

            if (objects[i] == null)
                Debug.Log($"Bind Error! : {objNames[i]}");
        }
    }

    protected void BindObjects(Type enumType)
    {
        Bind<GameObject>(enumType);
    }
    protected void BindTexts(Type enumType)
    {
        Bind<TextMeshProUGUI>(enumType);
    }
    protected void BindImages(Type enumType)
    {
        Bind<Image>(enumType);
    }
    protected void BindButtons(Type enumType)
    {
        Bind<Button>(enumType);
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"Invalid Get Type! : {typeof(T).Name}");
            return null;
        }

        if (index < 0 || index >= objects.Length)
        {
            Debug.Log($"Out of Index! : {typeof(T).Name}, index '{index}'");
        }

        return objects[index] as T;
    }

    protected GameObject GetObject(int index)
    {
        return Get<GameObject>(index);
    }
    protected TextMeshProUGUI GetText(int index)
    {
        return Get<TextMeshProUGUI>(index);
    }
    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }
    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }

    protected void BindUIEvent(GameObject go, Action<PointerEventData> action = null, Define.EUIEventType eventType = Define.EUIEventType.Click)
    {
        UI_EventComponent evt = go.AddComponent<UI_EventComponent>();

        switch (eventType)
        {
            case Define.EUIEventType.Click:
                evt.OnClickEvent -= action;
                evt.OnClickEvent += action;
                break;
        }
    }
}
