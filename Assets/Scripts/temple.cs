using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class temple : MonoBehaviour {

    public int collectedbles = 0;
    public GameObject UI, endGameCanvas,endFire;

    public Twitter.API.TwitterUser thisUser;
    bool once;
    void Update()
    {
        if (collectedbles > 3 && !once)
        {
            once = true;
            //ending sequence
            Camera.main.GetComponent<cameraOrbitControls>().enabled = false;
            Camera.main.transform.position = new Vector3(-12.1f, 12.9f, 14.1f);
            Camera.main.transform.rotation = Quaternion.Euler(25, 140, 0);
            var eUI = GameObject.FindGameObjectWithTag("UIchanger").GetComponent<UIscripting>();
            eUI.currentCam.enabled = false;
            UI.SetActive(false);
            endGameCanvas.SetActive(true);
            StartCoroutine(endGameText());
        }

    }
    

     IEnumerator endGameText()
    {
        yield return new WaitForSeconds(2.5f);
        endFire.GetComponent<ParticleSystem>().enableEmission = true;
        Text[] texts = endGameCanvas.GetComponentsInChildren<Text>();

        string a = "\n \nYou collected all the\nMcGuffins!";
        string b = "Go You!";
        string c = "Woo!";

        yield return new WaitForSeconds(1.5f);
        for (int i=0; i < a.Length; i++)
        {
            texts[0].text += a[i];
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < b.Length; i++)
        {
            texts[1].text += b[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < c.Length; i++)
        {
            texts[2].text += c[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void OnMouseDown()
    {
        if (thisUser.ID != "")
        {
            Debug.Log(thisUser.ID);
            var UI = GameObject.FindGameObjectWithTag("UIchanger").GetComponent<UIscripting>();
            UI.newCam = GetComponentInChildren<Camera>();
            UI.currentUser = thisUser;
            UI.UpdateUI();
        }
    }

}
