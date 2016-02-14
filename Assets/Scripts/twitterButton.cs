using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Net;
using System.Security.Cryptography;

public class twitterButton : MonoBehaviour {
    
    loginButton details;
    public int count;
    public List<Twitter.API.Tweet> tweets;
    public InputField usernameInput;

    public string[] data;
    public List<string> IDs = new List<string>();
    List<GameObject> babs = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        details = GameObject.Find("Login Button").GetComponent<loginButton>();
    }

    public void postTweet()
    {
        string m_Tweet = GameObject.Find("Tweet Input").GetComponent<InputField>().text;

        StartCoroutine(Twitter.API.PostTweet(m_Tweet, details.consumerKey, details.consumerSecret, details.m_AccessTokenResponse,
                           new Twitter.PostTweetCallback(this.OnPostTweet)));
    }

    void OnPostTweet(bool success)
    {
        print("OnPostTweet - " + (success ? "succedded." : "failed."));
    }

    public void GetTweets()
    {
        if (usernameInput.text != null)
        {
            Twitter.API.GetUserTimeline(usernameInput.text, Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret), count, this);
        }
        else
        {
            Twitter.API.GetUserTimeline(details.ScreenName, Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret), count, this);
        }
    }

    public void GetProfile()
    {
        Twitter.API.GetProfile(usernameInput.text, Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret), this);
    }

    public void GetProfile(string ID)
    {
        Twitter.API.GetProfile(ID, Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret), this);
    }

    public void BABY(Twitter.API.TwitterUser user)
    {
        //Make a new zombie
        //with properties n shit
        GameObject newGuy = Instantiate(Resources.Load("zombiething")) as GameObject;
        newGuy.GetComponent<Actor>().thisUser = user;
        StartCoroutine(setAvatar(user.avatarURL, newGuy.GetComponent<Actor>()));
        newGuy.name = user.displayName;
        newGuy.GetComponent<Actor>().offset = babs.Count * (360f / 100f);
        babs.Add(newGuy);
    }

    public void GetFollowers()
    {
        IDs.Clear();
        Twitter.API.GetFollowerIDs(usernameInput.text, Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret), this);

        Debug.Log(IDs.Count);
        if (IDs.Count < 100)
            for (int i = 0; i < IDs.Count; i++)
                GetProfile(IDs[i]);
        else
            for (int i = 0; i < 100; i++)
                GetProfile(IDs[i]);

    }

    public static IEnumerator setAvatar(string url, Actor guy)
    {
        WWW web = new WWW(url);
        yield return web;
        guy.getFace(web.texture);
        //Sprite avatar = Sprite.Create(web.texture,new Rect(0,0,web.texture.width,web.texture.height), new Vector2(.5f,.5f));
    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}