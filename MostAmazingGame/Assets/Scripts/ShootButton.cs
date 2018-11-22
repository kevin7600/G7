using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool pressed;
    private bool fired;
    public void OnPointerUp(PointerEventData eventData)
    {
        fired = false;
        pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
    public bool AttemptFire()//return true if can fire, assumes player fired, and makes player unable to fire until shoot button is released
    {
        if (pressed && !fired)
        {
            fired = true;
            return true;
        }
        return false;

    }
}
