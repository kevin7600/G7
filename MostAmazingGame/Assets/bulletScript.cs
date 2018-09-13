using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {
    public Rigidbody rb;
    public float bulletSpeed=1;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //Vector3 targetForward = targetRot * Vector3.forward;
        rb.velocity = transform.rotation * new Vector3(bulletSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update () {
    }
}
