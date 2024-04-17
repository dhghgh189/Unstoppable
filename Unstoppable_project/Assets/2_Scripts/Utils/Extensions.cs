using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T FindChild<T>(this GameObject parent, string childName) where T : Component
    {
        return Util.FindChild<T>(parent, childName);
    }
}
