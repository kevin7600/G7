using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {
    //private readonly string[] powerupTypes = { "Magazine", "Speed" };
    public string powerupType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            //if (playerController.GetHasPowerup())//already has a powerup
            //{
            //    return;
            //}
            if (powerupType == "Magazine")
            {
                DePowerUp(ref playerController);
                playerController.currentMagazineCapacity *= 2;
                playerController.magazineCount = playerController.currentMagazineCapacity;
            }
            else if (powerupType == "Speed")
            {
                DePowerUp(ref playerController);
                playerController.currentMoveSpeed *= (float)1.5;
            }

            //StartCoroutine(PowerupDuration(other.gameObject));
            Destroy(gameObject);
        }
    }
    private void DePowerUp(ref PlayerController playerController)
    {
        playerController.currentMagazineCapacity = playerController.defaultMagazineCapacity;
        if (playerController.magazineCount > playerController.currentMagazineCapacity)
        {
            playerController.magazineCount = playerController.currentMagazineCapacity;
        }
        playerController.currentMoveSpeed = playerController.defaultMoveSpeed;

    }
    //public IEnumerator PowerupDuration(GameObject playerObject)
    //{
    //    yield return new WaitForSeconds(3f);
    //    PlayerController playerController = playerObject.GetComponent<PlayerController>();
    //    playerController.currentMoveSpeed = playerController.moveSpeed;
    //    playerController.currentMagazineCapacity = playerController.magazineCapacity;



    //}
}
