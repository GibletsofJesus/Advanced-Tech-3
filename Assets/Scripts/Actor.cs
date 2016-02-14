using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    public Twitter.API.Tweet thisTweet;
    public Twitter.API.TwitterUser thisUser;
    public GameObject faceTop, faceBot;
    private float angle = 0;
    public float radius = 10;
    public float offset;
    public float speed;
    public Animator headAnimator;
    	
    public void getFace(Texture2D avatarTex)
    { 
        faceTop.GetComponent<Renderer>().material.mainTexture = avatarTex;
        faceTop.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0.5f);
        faceTop.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 0.5f);
        faceBot.GetComponent<Renderer>().material.mainTexture = avatarTex;
        faceBot.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 0.5f);
    }

    void blert()
    {
        headAnimator.Play("openMouth");
        GameObject speeeech = Instantiate(Resources.Load("speechThing") as GameObject);
        speeeech.transform.position = transform.position;
        speeeech.transform.parent = transform;
        speeeech.GetComponent<Rigidbody>().AddForce(-speeeech.transform.forward*100);
        speeeech.GetComponent<speechBubble>().FormatString(thisUser.mostRecentTweet, speeeech.GetComponent<TextMesh>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            blert();

        if (Random.value == .42f)
            blert();
        float x = 0;
        float y = 0;

        x = radius * Mathf.Cos(angle + (Mathf.Deg2Rad * offset));
        y = radius * Mathf.Sin(angle + (Mathf.Deg2Rad * offset));

        transform.localPosition = new Vector3(x, 1.1875f, y);
        angle += (.1f / radius) * Mathf.Rad2Deg * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, (-angle * Mathf.Rad2Deg) - offset, 0);
    }
}
