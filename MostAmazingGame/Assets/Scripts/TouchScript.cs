using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {
    public float speed = 0.2f;
    // Update is called once per frame
    bool isTouching = false;

    void Update()
    {

        if (Input.touchCount > 1)
            return;
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    if (hit.collider.gameObject.name == gameObject.name)
                        isTouching = true;
                }
            }
            if (isTouching && (Input.touches[0].phase == TouchPhase.Moved ||
                               Input.touches[0].phase == TouchPhase.Stationary))
            {
                Vector2 touchDeltaPosition = Input.touches[0].deltaPosition;
                transform.Translate(touchDeltaPosition.x * speed * Time.deltaTime,
                                    touchDeltaPosition.y * speed * Time.deltaTime, 0);
            }
            if (isTouching && Input.touches[0].phase == TouchPhase.Ended)
                isTouching = false;
        }

    }
}
