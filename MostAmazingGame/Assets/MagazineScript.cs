using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;


public class MagazineScript : NetworkBehaviour
{
    public int magazineCapacity = 2;
    [SyncVar(hook = "OnFire")]
    public int magazineCount;
    public RectTransform magazineBar;
    public int test;

    void OnFire(int magazineCount)
    {
        magazineBar.sizeDelta = new Vector2(magazineCount, magazineBar.sizeDelta.y);
    }
    // Use this for initialization
    void Start () {
        magazineCount = magazineCapacity;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
