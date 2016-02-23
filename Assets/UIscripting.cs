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
        infoText.text += "<size=35>"+currentUser.displayName + "</size>\n" + "\n";
        infoText.text += "<color=#0867E1>@" + currentUser.username;
        infoText.text += "\n\n</color><size=20>" +currentUser.bio.Replace("\\n", "\n") + "\n" + "\n</size>";
        infoText.text += "Join date: "+currentUser.joinDate.Remove(10,15) + "\n" + "\n";
        infoText.text += "<size=20>Total tweets:</size><color=#00E314> " + currentUser.totalTweets + "</color>\n" + "\n";
        infoText.text += "<size=20>Followers:</size> <color=#E30000>" + currentUser.followers + "</color>\n" + "\n";
    }
}
