using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    // Update is called once per frame
    void Update () {

        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        //if (Input.touchCount > 1)
        //    return;
        //if (Input.touchCount == 1)
        //{
        //    if (Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //        //RaycastHit hit = new RaycastHit();
        //       // RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y), Vector2.zero, 0f);
        //        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //        //if (Physics.Raycast(ray, out hit))
        //        if (hit)
        //        {
        //            //print("Hit something!");
        //            if (hit.collider.gameObject.name == gameObject.name)
        //            isTouching = true;
        //        }
        //    }
        //    if (isTouching && (Input.touches[0].phase == TouchPhase.Moved || 
        //                       Input.touches[0].phase == TouchPhase.Stationary))
        //    {
        //        Vector2 touchDeltaPosition = Input.touches[0].deltaPosition;
        //        transform.Translate(touchDeltaPosition.x * speed * Time.deltaTime, 
        //                            touchDeltaPosition.y * speed * Time.deltaTime, 0);
        //    }
        //    if (isTouching && Input.touches[0].phase == TouchPhase.Ended)
        //        isTouching = false;
        //}
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        ////if (Physics2D.Raycast(ray.origin, ray.direction))
        //if (hit && hit.collider.gameObject.tag == "box")
        //print("Hit something!");

    }
}
