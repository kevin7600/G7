using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour{

    public float moveSpeed = 1f;
    public float currentAngle;
    public float bulletTTL = 5.0f;

    private Joystick joystick;
    private ShootButton shootButton;

    public GameObject bullet;
	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<Joystick>();
        shootButton = FindObjectOfType<ShootButton>();
        currentAngle = 0;
	}
	
	// Update is called once per frame
	void Update () {

	    if (shootButton.Pressed&& !shootButton.fired)
        {
            Vector3 gunPosition = transform.position + (transform.rotation * new Vector3(1, 0, 0));
            GameObject myBullet = Instantiate(bullet, gunPosition, transform.rotation);
           
            Destroy(myBullet, bulletTTL);
            shootButton.fired = true;
        }
        else if (!shootButton.Pressed && shootButton.fired)
        {
            shootButton.fired = false;
        }
	}
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

        float angleDeg;
        if (joystick.Horizontal == 0&& joystick.Vertical>0)
        {
            angleDeg = 90;
        }
        else if (joystick.Horizontal==0 && joystick.Vertical < 0)
        {
            angleDeg = 270;
        }
        else if (joystick.Horizontal==0 && joystick.Vertical == 0)
        {
            angleDeg = currentAngle;
        }
        else
        {
            float angleRad = Mathf.Atan(joystick.Vertical / joystick.Horizontal);
            angleDeg = angleRad * (180 / Mathf.PI);

            if (joystick.Horizontal < 0)
            {
                angleDeg += 180;
            }
        }


        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        currentAngle = angleDeg;
            //joystick.Direction.x*100;
        movement *= moveSpeed;
        this.transform.position += movement;
    }

}
