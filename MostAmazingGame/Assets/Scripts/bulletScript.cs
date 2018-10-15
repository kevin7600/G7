using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {
    private Rigidbody2D rb;
    public float bulletSpeed=100;
    public float bulletTTL = 4.0f;
    public bool canHitPlayer = false;
    public GameObject bulletOwner;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //Vector3 targetForward = targetRot * Vector3.forward;
        rb.velocity = transform.rotation * new Vector3(bulletSpeed, 0, 0);
        StartCoroutine(bulletTimer());
    }
    public IEnumerator bulletTimer()
    {
        yield return new WaitForSeconds(.6f);
        canHitPlayer = true;
    }
    // Update is called once per frame
    void Update () {
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    gameObject.SetActive(false);
    //    print(rb.velocity.normalized);
    //    if (other.gameObject.tag=="box"){
    //        other.gameObject.SetActive(false);

    //    }

    //}

    private Vector2 lastVelocity;

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            //reflect
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        // hit player to reduce health
        if (collision.gameObject.tag == "Player") {
            if (bulletOwner.Equals(collision.gameObject)) {
                if (!canHitPlayer)
                {
                    return;
                }
            }
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
            }

            Destroy(gameObject);
        }
        rb.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);

    }
}
