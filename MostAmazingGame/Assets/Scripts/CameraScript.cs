using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    protected GameObject player;
    private Vector3 offset=new Vector3(0, 0, -20);
	
    public void SetPlayer(GameObject player)//called in playerController.cs to set the player
    {
        this.player = player;
    }
	// Update is called once per frame
	void LateUpdate () {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
