using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void OnDragDelegate(PointerEventData delta);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onPress;
    public OnDragDelegate onBeginDrag;
    public OnDragDelegate onEndDrag;
    public OnDragDelegate onDrag;

    private bool isPointerDown = false;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();

        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        onDown?.Invoke(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(gameObject);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        onUp?.Invoke(gameObject);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        onSelect?.Invoke(gameObject);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        onUpdateSelect?.Invoke(gameObject);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
    }

    private void Update()
    {
        if (isPointerDown && onPress != null)
        {
            onPress(gameObject);
        }
    }
}