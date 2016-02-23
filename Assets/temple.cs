using UnityEngine;
using System.Collections;

public class temple : MonoBehaviour {

    public Twitter.API.TwitterUser thisUser;
    public void OnMouseDown()
    {
        var UI = GameObject.FindGameObjectWithTag("UIchanger").GetComponent<UIscripting>();
        UI.newCam = GetComponentInChildren<Camera>();
        UI.currentUser = thisUser;
        UI.UpdateUI();
    }
}
