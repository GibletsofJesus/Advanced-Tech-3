using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class maker : MonoBehaviour {

    public twitterButton data;
    public List<Twitter.API.Tweet> tweets;
    public GameObject thing;
    List<GameObject> allMyThings = new List<GameObject>();

    public void things()
    {
        Debug.Log(allMyThings.Count);
        for (int i=0; i < allMyThings.Count;i++)
        {
            Destroy(allMyThings[i]);
        }
        allMyThings.Clear();
        Debug.Log(allMyThings.Count);
        tweets = data.tweets;

        for (int i=0; i < tweets.Count;i++)
        {
            GameObject newThing = Instantiate(thing,new Vector3(0,1 + 1.25f*i,0),transform.rotation) as GameObject;
            newThing.GetComponent<thing>().tweet = tweets[i];
            newThing.GetComponent<thing>().hi();
            allMyThings.Add(newThing);
        }

        Debug.Log(allMyThings.Count);
    }

    public void makeBuildings()
    {
        //Pick a random position inside a square
        //have said square change size depending on # of tweets
    }
}
