using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private int _order = 1;

    private Stack<PopupUI> _popupUIs = new Stack<PopupUI>();
    private SceneUI _sceneUI;

    public T GetSceneUI<T>() where T : SceneUI
    {
        T ui = _sceneUI as T;
        if (ui == null)
        {
            Debug.Log($"GetSceneUI Error! : {typeof(T).Name}");
            return null;
        }

        return ui;
    }

    public Transform UI_Root
    {
        get
        {
            GameObject go = GameObject.Find("@UI_Root");
            if (go == null)
                go = new GameObject("@UI_Root");
            return go.transform;
        }
    }

    public void SetOrder(GameObject go, bool isPopup, int order = 0)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.Log($"SetOrder Error! : {go.name}");
            return;
        }

        if (isPopup)
            canvas.sortingOrder = _order++;
        else
            canvas.sortingOrder = order;
    }

    public T MakeSubItem<T>(string name = null, Transform parent = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = theApp.Res.Instantiate($"UI/SubItem/{name}", parent);
        if (go == null)
            return null;

        T subItem = go.GetComponent<T>();
        if (subItem == null)
        {
            Debug.Log($"Invalid SubItem! : {name}");
            return null;
        }

        return subItem;
    }

    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = theApp.Res.Instantiate($"UI/Scene/{name}", UI_Root);
        if (go == null)
            return null;

        T sceneUI = go.GetComponent<T>();
        if (sceneUI == null)
        {
            Debug.Log($"Invalid Scene UI! : {name}");
            return null;
        }

        _sceneUI = sceneUI;

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = theApp.Res.Instantiate($"UI/Popup/{name}", UI_Root);
        if (go == null)
            return null;

        T popupUI = go.GetComponent<T>();
        if (popupUI == null)
        {
            Debug.Log("Invalid Popup UI !!");
            return null;
        }

        _popupUIs.Push(popupUI);

        return popupUI;
    }

    public void ClosePopupUI()
    {
        if (_popupUIs.Count <= 0)
        {
            Debug.Log("Popup stack is empty.");
            return;
        }

        PopupUI popup = _popupUIs.Pop();
        GameObject.Destroy(popup.gameObject);

        _order--;
    }

    public void ClosePopupUI(PopupUI popup)
    {
        if (_popupUIs.Count <= 0)
        {
            Debug.Log("Popup stack is empty.");
            return;
        }

        if (_popupUIs.Peek() != popup)
        {
            Debug.Log($"ClosePopupUI Error! : {popup.name}");
            return;
        }

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_popupUIs.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();

        if (_sceneUI != null)
        {
            theApp.Res.Destroy(_sceneUI.gameObject);
            _sceneUI = null;
        }    
    }
}
