using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {
    private string[] powerupTypes = { "Magazine", "Speed" };
    public string powerupType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.GetHasPowerup())//already has a powerup
            {
                return;
            }
            for (int i = 0; i < powerupTypes.Length; i++)
            {
                if (powerupType == "Magazine")
                {
                    playerController.magazineCapacity *= 2;
                }
                else if (powerupType == "Speed")
                {
                    playerController.currentMoveSpeed *= (float)1.2;
                }

            }
            StartCoroutine(PowerupDuration(other.gameObject));
            playerController.SetHasPowerup(true);
            this.gameObject.SetActive(false);
        }
    }
    public IEnumerator PowerupDuration(GameObject playerObject)
    {
        yield return new WaitForSeconds(3f);
        PlayerController playerController = playerObject.GetComponent<PlayerController>();
        playerController.currentMoveSpeed = playerController.moveSpeed;
        playerController.currentMagazineCapacity = playerController.magazineCapacity;



    }
}
