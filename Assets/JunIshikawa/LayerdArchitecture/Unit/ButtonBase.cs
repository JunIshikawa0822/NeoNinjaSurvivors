using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour
{
    public event Action pointerDownEvent;
    public event Action pointerUpEvent;
    public event Action pointerOverEvent;
    public event Action pointerExitEvent;

    public event Action buttonSelectEvent;
    public event Action buttonDeselectEvent;

    [System.NonSerialized]
    public Image buttonImage;

    [System.NonSerialized]
    public EventTrigger buttonEventTrigger;

    [System.NonSerialized]
    public Outline buttonOutline;

    public void ButtonInit()
    {
        buttonImage = GetComponent<Image>();
        buttonEventTrigger = GetComponent<EventTrigger>();
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

    public void PanelSelectEvent()
    {
        if (buttonSelectEvent == null) return;
        buttonSelectEvent?.Invoke();
    }

    public void PanelDeselectEvent()
    {
        if (buttonDeselectEvent == null) return;
        buttonDeselectEvent?.Invoke();
    }
}
