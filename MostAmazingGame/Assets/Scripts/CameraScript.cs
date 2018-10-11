using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    protected GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset =new Vector3(0, 0, -10);
	}
	
    public void SetPlayer(GameObject player)//called in playerController.cs to set the player
    {
        this.player = player;
    }
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
