using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    public Twitter.API.Tweet thisTweet;
    public Twitter.API.TwitterUser thisUser;
    public GameObject faceTop, faceBot, selectionArrow;
    private float angle = 0;
    public float radius = 10;
    public float offset;
    float animSpeed=.6f,moveSpeed=1;
    public Animator headAnimator,bodyAnimator;

    public Vector3 target;
    public bool selected;
    public bool allowMove=true;
    unitManager manager;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Base")
        {
            Debug.Log("col");
            allowMove = false;
        }
        if (col.gameObject.tag!="Speech")
            Debug.Log(col.gameObject.tag);
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Base")
            allowMove = true;
    }

    void Awake()
    {
        manager = Camera.main.GetComponent<unitManager>();
    }

    public void OnMouseDown()
    {
       manager.unitSelection(gameObject);
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

    bool allowBlert=true;

    IEnumerator blertReset()
    {
        yield return new WaitForSeconds(.5f);
        GameObject speeeech = Instantiate(Resources.Load("speechThing") as GameObject);
        speeeech.transform.position = transform.position;
        //speeeech.transform.parent = transform;
        //speeeech.GetComponent<Animator>().Play("speechBoob");
        speeeech.GetComponent<Rigidbody>().AddForce(transform.forward * 12f, ForceMode.VelocityChange);
        speeeech.GetComponent<speechBubble>().FormatString(thisUser.mostRecentTweet, speeeech.GetComponent<TextMesh>());

        yield return new WaitForSeconds(0.5f);
        speeeech.transform.parent = null;

        yield return new WaitForSeconds(1f);
        allowBlert = true;
    }

    #endregion

    void Update()
    {
        #region selectionTings
        if (GetComponentInChildren<Renderer>().isVisible && Input.GetMouseButton(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = (Screen.height - camPos.y);
            if (Camera.main.GetComponent<unitManager>().selection.Contains(camPos))
                manager.addUnitToSelection(gameObject);
            else if (manager.selectedUnits.Contains(gameObject))
            {
                selected = false;
                manager.selectedUnits.Remove(gameObject);
            }
        }
        #endregion

        #region blerting
        if (Input.GetKeyDown(KeyCode.K) && allowBlert)
        {
            allowBlert = false;
            StartCoroutine(blertReset());
            headAnimator.Play("openMouth");
        }
        if (Random.value > .97f && allowBlert)
        {
            allowBlert = false;
            StartCoroutine(blertReset());
            headAnimator.Play("openMouth");
        }
        #endregion

        #region face and move in direction
        Quaternion rot =new Quaternion();
        var y = Mathf.Atan2((target.x - transform.position.x), (target.z - transform.position.z)) * Mathf.Rad2Deg;
        rot.eulerAngles = new Vector3(0, y,0);
        transform.rotation = rot;

        Vector3 distance = target - transform.position;
        float newAnimSpeed = 0, newMoveSpeed = 0;
        if (distance.sqrMagnitude > .1f && distance.sqrMagnitude < 25f && allowMove)
        {
            transform.Translate(distance.normalized * Time.deltaTime*moveSpeed, Space.World);
            bodyAnimator.SetBool("walking", true);
            newAnimSpeed = .6f;
            newMoveSpeed = 1;
        }
        else if (distance.sqrMagnitude > .1f && allowMove)
        {
            transform.Translate(distance.normalized * Time.deltaTime*moveSpeed, Space.World);
            bodyAnimator.SetBool("walking", true);
            newAnimSpeed = 1.8f;
            newMoveSpeed = 3;
        }
        else
        {
            newAnimSpeed = .6f;
            bodyAnimator.SetBool("walking", false);
        }
        
        moveSpeed = Mathf.Lerp(moveSpeed, newMoveSpeed, Time.deltaTime*3);
        bodyAnimator.SetFloat("speed", animSpeed = Mathf.Lerp(animSpeed, newAnimSpeed, Time.deltaTime*3));
        #endregion

        if (selected)
            selectionArrow.SetActive(true);
        else
            selectionArrow.SetActive(false);

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
        angle += (.1f / radius) * Mathf.Rad2Deg * Time.deltaTime * animSpeed;
        transform.rotation = Quaternion.Euler(0, (-angle * Mathf.Rad2Deg) - offset, 0);

        walkInCircle = false;
        target = transform.position;
    }
}
