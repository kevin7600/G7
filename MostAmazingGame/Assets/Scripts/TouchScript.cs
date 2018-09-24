using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {
    public float speed = 1f;
    // Update is called once per frame
    bool isTouching = false;
    private Ray ray;
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
                    if ((curTouch.phase == TouchPhase.Moved ||
                   curTouch.phase == TouchPhase.Stationary))
                    {
                        Vector2 touchDeltaPosition = curTouch.deltaPosition * .015f;
                        transform.Translate(touchDeltaPosition);

                    }
                }
                //if (curTouch.phase == TouchPhase.Began)
                //{

                //}
            }
        }
    }
}
