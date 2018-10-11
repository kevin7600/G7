using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour{
    public Color[] colors = {Color.yellow, Color.grey, Color.red, Color.green };

    public float moveSpeed = 1f;
    public float currentAngle;
    public float bulletTTL = 10.0f;

    private Joystick joystick;
    private ShootButton shootButton;

    public GameObject bullet;
	// Use this for initialization
	//void Start () {
    //    joystick = FindObjectOfType<Joystick>();
    //    shootButton = FindObjectOfType<ShootButton>();
    //    currentAngle = 0;
    //}

    public override void OnStartLocalPlayer()
    {

        joystick = FindObjectOfType<Joystick>();
        shootButton = FindObjectOfType<ShootButton>();
        currentAngle = 0;
        
        CmdSetPlayerColor();//sync player colors across network

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraScript cameraScript = camera.GetComponent<CameraScript>();
        cameraScript.SetPlayer(this.gameObject);
    }


    // Update is called once per frame
    void Update () {
        //print("Id: "+netId);
        if(!isLocalPlayer)
        {
            return;
        }

        if (shootButton.Pressed&& !shootButton.fired)
        {
            CmdFire();
            shootButton.fired = true;

        }
        else if (!shootButton.Pressed && shootButton.fired)
        {
            shootButton.fired = false;
        }
	}
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

        float angleDeg;
        if (joystick.Horizontal == 0 && joystick.Vertical>0)
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
    [Command]
    void CmdFire()
    {
        Vector3 gunPosition = transform.position + (transform.rotation * new Vector3(1, 0, 0));
        GameObject myBullet = Instantiate(bullet, gunPosition, transform.rotation);
        NetworkServer.Spawn(myBullet);
        Destroy(myBullet, bulletTTL);
    }
    [Command]
    void CmdSetPlayerColor()
    {
        RpcUpdatePlayerColor();
    }
    [ClientRpc]
    void RpcUpdatePlayerColor()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            SpriteRenderer sprite = players[i].GetComponent<SpriteRenderer>();
            int id = int.Parse(players[i].GetComponent<NetworkIdentity>().netId.ToString());
            sprite.material.color = colors[(id - 1) % colors.Length];
        }
    }
}
