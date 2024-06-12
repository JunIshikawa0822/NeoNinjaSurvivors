using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    public event Action pointerDownEvent;
    public event Action pointerUpEvent;
    public event Action pointerOverEvent;
    public event Action pointerExitEvent;

    [System.NonSerialized]
    public Image buttonImage;

    public void ButtonInit()
    {
        buttonImage = GetComponent<Image>();
    }

    public void PointerDownEvent()
    {
        if (pointerDownEvent == null) return;
        pointerDownEvent?.Invoke();
    }

    public void PointerUpEvent()
    {
        if (pointerUpEvent == null) return;
        pointerUpEvent?.Invoke();
    }

    public void PointerOverEvent()
    {
        if (pointerExitEvent == null) return;
        pointerOverEvent?.Invoke();
    }

    public void PointerExitEvent()
    {
        if (pointerExitEvent == null) return;
        pointerExitEvent?.Invoke();
    }
}
