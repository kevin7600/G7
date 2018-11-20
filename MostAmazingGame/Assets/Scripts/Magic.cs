using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Magic : NetworkBehaviour {

    public const int timeSlots = 30;
    public const int mWidth = 100;
    public const int OffSetY2 = 3;

    [SyncVar(hook = "OnChangeMagic")]
    public int curSlots = timeSlots;
    public RectTransform magicBar;

    void Start()
    {
        curSlots = timeSlots;
        magicBar.sizeDelta = new Vector2(mWidth, magicBar.sizeDelta.y);
        StartCoroutine(RechargeMagic());
    }

    private void Update()
    {
        UpdateLocation();
        UpdateMagic();
    }

    public void UpdateLocation()
    {
        Transform mTransform = gameObject.transform;
        //update MagicBar Canvas
        magicBar.parent.parent.transform.position = new Vector3(mTransform.position.x, mTransform.position.y + OffSetY2, -1);
    }

    public void UpdateMagic()
    {
        magicBar.sizeDelta = new Vector2(((float)curSlots / (float)timeSlots) * mWidth, magicBar.sizeDelta.y); //client
    }


    public IEnumerator RechargeMagic()
    {
        float waitTimeSlot = this.GetComponent<PlayerController>().waitTime / timeSlots;
        while (true)
        {
            yield return new WaitForSeconds(waitTimeSlot);
            if (curSlots < timeSlots)
                curSlots += 1;
        }
    }

    void OnChangeMagic(int curslots)
    {
        curSlots = curslots;
        magicBar.sizeDelta = new Vector2(((float)curSlots / (float)timeSlots) * mWidth, magicBar.sizeDelta.y);
        //print("curSlots:  " + curSlots + "==" + this.netId);
    }
}
