using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    public const int mWidth = 100;
    public const int OffSetY = 2;
    public const int OffSetY2 = 3;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    void Start(){
        currentHealth = maxHealth;
        healthBar.sizeDelta = new Vector2(mWidth, healthBar.sizeDelta.y);
    }

    private void Update()
    {
        UpdateLocation();
    }

    public void UpdateLocation()
    {
        Transform mTransform = gameObject.transform;
        //update HealthBar Canvas
        healthBar.parent.parent.transform.position = new Vector3(mTransform.position.x, mTransform.position.y + OffSetY, -1);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
            // called on the Server, but invoked on the Clients
            CmdDeath();
        }
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(((float)health / (float)maxHealth) * mWidth, healthBar.sizeDelta.y);
        print("health:  " + health);
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
        camera.transform.position = new Vector3(0, 18, camera.transform.position.z);
        camera.GetComponent<Camera>().orthographicSize = 33;
        Destroy(gameObject);

    }
}
/*
public class Health : NetworkBehaviour
{

    public int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;

    public RectTransform healthBar;

    void Start()
    {
        print("health location:  " + healthBar.position);
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        currentHealth = maxHealth;
        print("currentHealth:  " + currentHealth);
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
        healthBar.sizeDelta = new Vector2(((float)currentHealth/(float)maxHealth)*100, healthBar.sizeDelta.y);
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
        camera.transform.position = new Vector3(0, 18, camera.transform.position.z);
        camera.GetComponent<Camera>().orthographicSize = 33;
        Destroy(gameObject);

    }

}*/
