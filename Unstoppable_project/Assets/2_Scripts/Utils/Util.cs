using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static GameObject FindChild(GameObject parent, string childName)
    {
        Transform childTransform = FindChild<Transform>(parent, childName);
        if (childTransform == null)
            return null;

        return childTransform.gameObject;
    }

    public static T FindChild<T>(GameObject parent, string childName) where T : UnityEngine.Object
    {
        foreach (T component in parent.GetComponentsInChildren<T>())
        {
            if (component.name == childName)
                return component;
        }

        Debug.Log($"Can't find child : {childName}");
        return null;
    }
}
