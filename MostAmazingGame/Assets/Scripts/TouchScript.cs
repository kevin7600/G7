using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {
    public float speed = 0.3f;
    // Update is called once per frame
    int fingerTouching=-1;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch curTouch = Input.GetTouch(i);
                Ray ray = Camera.main.ScreenPointToRay(curTouch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit && hit.collider.gameObject.name == gameObject.name)
                {
                    fingerTouching = curTouch.fingerId;

                }
                if (fingerTouching==curTouch.fingerId){
                    Vector2 touchDeltaPosition = curTouch.deltaPosition * .013f;
                    transform.Translate(touchDeltaPosition);
                }
                if (curTouch.fingerId==fingerTouching&&curTouch.phase==TouchPhase.Ended){//detect end of touch
                    fingerTouching = -1;
                }
                //if (curTouch.phase == TouchPhase.Began)
                //{

                //}
            }
        }
    }
}
