using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour{
    public Color[] colors = {Color.yellow, Color.grey, Color.red, Color.green };

    public int defaultMagazineCapacity = 3;
    [HideInInspector]
    public int currentMagazineCapacity;
    public int magazineCount;

    public float defaultMoveSpeed = 10f;
    public float currentMoveSpeed;

    private float currentAngle;

    private Joystick joystick;
    private ShootButton shootButton;
    public GameObject bullet;
   

    public override void OnStartLocalPlayer()
    {
        currentMoveSpeed = defaultMoveSpeed;
        currentMagazineCapacity = defaultMagazineCapacity;
        magazineCount = defaultMagazineCapacity;
        joystick = FindObjectOfType<Joystick>();
        shootButton = FindObjectOfType<ShootButton>();
        currentAngle = 0;
        
        CmdSetPlayerColor();//sync player colors across network

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraScript cameraScript = camera.GetComponent<CameraScript>();
        cameraScript.SetPlayer(this.gameObject);
    }
    public IEnumerator RechargeBullet()
    {
        yield return new WaitForSeconds(3f);
        magazineCount=currentMagazineCapacity;
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Fire();
        Move();
    }

    void Move()
    {
        Vector2 movement = new Vector3(joystick.Horizontal, joystick.Vertical);
        movement.Normalize();
        movement *= currentMoveSpeed;
        float angleDeg;
        if (joystick.Horizontal == 0 && joystick.Vertical > 0)
        {
            angleDeg = 90;
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical < 0)
        {
            angleDeg = 270;
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
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
        currentAngle = angleDeg;
        print(movement);
        CmdMove(angleDeg,movement);
    }

    [Command]
    void CmdMove(float angleDeg, Vector2 movement)
    {
        print(movement);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = movement;//for smoothness of client movement
        //rb.position += movement;
        rb.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        
    }
    void Fire()
    {
        if (magazineCount > 0)//so you can't spam the shoot button
        {
            if (shootButton.AttemptFire())
            {
                CmdFire();
                magazineCount--;
                if (magazineCount <= 0)
                {
                    StartCoroutine(RechargeBullet());
                }
            }
        }
    }
    [Command]
    void CmdFire()
    {
        Vector3 gunPosition = transform.position + (transform.rotation * new Vector3(1, 0, 0))*(gameObject.GetComponent<BoxCollider2D>().size.x/(float)2.8);
        GameObject myBullet = Instantiate(bullet, gunPosition, transform.rotation);
        
        NetworkServer.Spawn(myBullet);
        Destroy(myBullet, myBullet.GetComponent<bulletScript>().bulletTTL);
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
