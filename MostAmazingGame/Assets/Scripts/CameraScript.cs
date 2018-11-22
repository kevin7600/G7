using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    protected GameObject player;
    private Vector3 offset=new Vector3(0, 0, -20);
    public float smoothSpeed = .08f;
    public void SetPlayer(GameObject player)//called in playerController.cs to set the player
    {
        this.player = player;
    }
	// Update is called once per frame
	void LateUpdate () {
        if (player != null)
        {

            Vector3 desiredPosition=player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
