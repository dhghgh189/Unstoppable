using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventComponent : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> OnClickEvent = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(eventData);
    }
}
