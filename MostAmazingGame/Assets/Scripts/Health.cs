using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

    public int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;

    //public RectTransform healthBar;

    void Start()
    {
        //print("health location:  " + healthBar.position);
        //healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;

            // called on the Server, but invoked on the Clients
            CmdDeath();
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        //healthBar.sizeDelta = new Vector2(((float)currentHealth/(float)maxHealth)*100, healthBar.sizeDelta.y);
    }

    [Command]
    void CmdDeath()
    {
        RpcDeath();
    }

    [ClientRpc]
    void RpcDeath()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        if (isLocalPlayer)
        {
            StartCoroutine(DeathFunction());
        }
    }
    public IEnumerator DeathFunction()
    {
        gameObject.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(3f);
        
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraScript cameraScript = camera.GetComponent<CameraScript>();
        cameraScript.enabled = false;
        camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
        camera.GetComponent<Camera>().fieldOfView = 83;

    }

}