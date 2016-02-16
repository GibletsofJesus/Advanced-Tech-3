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

    public Vector3 target = Vector3.one*10;
    public bool selected;

    public void OnMouseDown()
    {
        var UI = GameObject.FindGameObjectWithTag("UIchanger").GetComponent<UIscripting>(); 
        UI.newCam = GetComponentInChildren<Camera>();
        UI.currentUser = thisUser;
        UI.UpdateUI();

        Camera.main.GetComponent<cameraOrbitControls>().selectedUnit = gameObject;
    }

    #region facetings
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
        GameObject speeeech = Instantiate(Resources.Load("speechThing") as GameObject);
        speeeech.transform.position = transform.position;
        //speeeech.transform.parent = transform;
        //speeeech.GetComponent<Animator>().Play("speechBoob");
        speeeech.GetComponent<Rigidbody>().AddForce(transform.forward*6f,ForceMode.VelocityChange);
        speeeech.GetComponent<speechBubble>().FormatString(thisUser.mostRecentTweet, speeeech.GetComponent<TextMesh>());
    }

    bool allowBlert=true;

    IEnumerator blertReset()
    {
        yield return new WaitForSeconds(2f);
        allowBlert = true;
    }

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && allowBlert)
        {
            allowBlert = false;
            StartCoroutine(blertReset());
            headAnimator.Play("openMouth");
            Invoke("blert", .5f);
        }
        if (Random.value > .97f && allowBlert)
        {
            allowBlert = false;
            StartCoroutine(blertReset());
            headAnimator.Play("openMouth");
            Invoke("blert", .5f);
        }

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        var rot = Quaternion.LookRotation(target);
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        transform.rotation = rot;
        Vector3 distance = target - transform.position;
        if (distance.sqrMagnitude > .1f)
        {
            transform.Translate(distance.normalized * Time.deltaTime, Space.World);
        }
           

        if (walkInCircle)
        circleWalk();
    }
    public bool walkInCircle;
    void circleWalk()
    {
        float x = 0;
        float y = 0;

        x = radius * Mathf.Cos(angle + (Mathf.Deg2Rad * offset));
        y = radius * Mathf.Sin(angle + (Mathf.Deg2Rad * offset));

        transform.localPosition = new Vector3(x, 1.1875f, y);
        angle += (.1f / radius) * Mathf.Rad2Deg * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, (-angle * Mathf.Rad2Deg) - offset, 0);
    }
}
