using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class maker : MonoBehaviour {

    public twitterButton data;
    public List<Twitter.API.Tweet> tweets;
    public GameObject thing;
    List<GameObject> babs = new List<GameObject>();
    public GameObject zombieGuy;

    public void TweetKebab()
    {
        for (int i=0; i < babs.Count;i++)
        {
            Destroy(babs[i]);
        }
        babs.Clear();
        tweets = data.tweets;

        for (int i=0; i < tweets.Count;i++)
        {
            GameObject newThing = Instantiate(thing,new Vector3(0,1 + 1.25f*i,0),transform.rotation) as GameObject;
            newThing.GetComponent<thing>().tweet = tweets[i];
            newThing.GetComponent<thing>().hi();
            babs.Add(newThing);
        }

        Debug.Log(babs.Count);
    }
}
