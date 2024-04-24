using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<Define.EInputEventType> OnJumpInput = null;
    public Action<Define.EInputEventType> OnSlideInput = null;
    public Action<Define.EInputEventType> OnUseItemInput = null;

    public void OnUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            OnJumpInput?.Invoke(Define.EInputEventType.JumpDown);
        if (Input.GetButtonUp("Jump"))
            OnJumpInput?.Invoke(Define.EInputEventType.JumpUp);

        if (Input.GetKey(KeyCode.DownArrow))
            OnSlideInput?.Invoke(Define.EInputEventType.SlideDown);
        if (Input.GetKeyUp(KeyCode.DownArrow))
            OnSlideInput?.Invoke(Define.EInputEventType.SlideUp);

        if (Input.GetKeyDown(KeyCode.E))
            OnUseItemInput?.Invoke(Define.EInputEventType.UseItem);
    }
}
