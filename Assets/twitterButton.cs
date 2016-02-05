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
    public Texture test;

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
        Twitter.API.GetUserTimeline(details.userID,Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret),count);
        //Twitter.API.GetTwitterAccessToken(details.consumerKey, details.consumerSecret);
    }
    
    public static IEnumerator setAvatar(string url)
    {
        WWW web = new WWW(url);
        yield return web;
        GameObject.Find("Avatar").GetComponent<Image>().sprite = Sprite.Create(web.texture,new Rect(0,0,web.texture.width,web.texture.height), new Vector2(.5f,.5f));
    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}
