using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIscripting : MonoBehaviour {

    public Text infoText;
    public Twitter.API.TwitterUser currentUser;
    public Camera currentCam, newCam;
	public void UpdateUI()
    {
        if (currentCam!=null)
            currentCam.enabled = false;

        currentCam = newCam;
        currentCam.enabled = true;
        infoText.text = "";
        infoText.text += currentUser.displayName + "\n" + "\n";
        infoText.text += "@"+currentUser.username + "\n" + "\n";
        infoText.text += currentUser.bio + "\n" + "\n";
        infoText.text += currentUser.joinDate.Remove(10,15) + "\n" + "\n";
        infoText.text += currentUser.totalTweets + "\n" + "\n";
        infoText.text += currentUser.followers + "\n" + "\n";
    }
}
