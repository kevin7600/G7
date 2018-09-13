using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool Pressed;
    public bool fired;
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
}
