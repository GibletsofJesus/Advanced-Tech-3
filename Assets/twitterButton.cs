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

    // Use this for initialization
    void Start ()
    {
        details = GameObject.Find("Login Button").GetComponent<loginButton>();
    }

    public void getAccount()
    {
        StartCoroutine(Twitter.API.GetAccountDetails(details.userID, details.ScreenName, details.consumerKey, details.consumerSecret, details.m_AccessTokenResponse,
                           new Twitter.GetAvatarCallback(this.OnAccountGet)));
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

    void OnAccountGet(bool success)
    {
        Debug.Log("Account grabbing - " + (success ? "succedded." : "failed."));
    }

    public void getm8s()
    {
        StartCoroutine(Twitter.API.SorryAlan(details.ScreenName, details.consumerKey, details.consumerSecret, details.m_AccessTokenResponse,
                           new Twitter.PostTweetCallback(this.OnAccountGet)));
    }

    public void test()
    {
        // oauth application keys
        var oauth_token = details.Token;
        var oauth_token_secret = details.TokenSecret;
        var oauth_consumer_key = details.consumerKey;
        var oauth_consumer_secret = details.consumerSecret;

        // oauth implementation details
        var oauth_version = "1.0";
        var oauth_signature_method = "HMAC-SHA1";

        // unique request details
        var oauth_nonce = Convert.ToBase64String(
            new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        var timeSpan = DateTime.UtcNow
            - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

        // message api details
        var status = "Updating status via REST API if this works";
        var resource_url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        var screen_name = details.ScreenName;
        // create oauth signature
        var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                        "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}";

        var baseString = string.Format(baseFormat,
                                    oauth_consumer_key,
                                    oauth_nonce,
                                    oauth_signature_method,
                                    oauth_timestamp,
                                    oauth_token,
                                    oauth_version,
                                     Uri.EscapeDataString(screen_name)
                                    );

        baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

        var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                "&", Uri.EscapeDataString(oauth_token_secret));

        string oauth_signature;
        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
        {
            oauth_signature = Convert.ToBase64String(
                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
        }

        // create the request header
        var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                           "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                           "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                           "oauth_version=\"{6}\"";

        var authHeader = string.Format(headerFormat,
                                Uri.EscapeDataString(oauth_nonce),
                                Uri.EscapeDataString(oauth_signature_method),
                                Uri.EscapeDataString(oauth_timestamp),
                                Uri.EscapeDataString(oauth_consumer_key),
                                Uri.EscapeDataString(oauth_token),
                                Uri.EscapeDataString(oauth_signature),
                                Uri.EscapeDataString(oauth_version)
                        );


        // make the request

        ServicePointManager.Expect100Continue = false;

        var postBody = "screen_name=" + Uri.EscapeDataString(screen_name);//
        resource_url += "?" + postBody;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
        request.Headers.Add("Authorization", authHeader);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";


        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        Debug.Log(responseData);
    }

    public void test2()
    {
        // You need to set your own keys and screen name
        var oAuthConsumerKey = "superSecretKey";
        var oAuthConsumerSecret = "superSecretSecret";
        var oAuthUrl = "https://api.twitter.com/oauth2/token";
        var screenname = "aScreenName";

        // Do the Authenticate
        var authHeaderFormat = "Basic {0}";

        var authHeader = string.Format(authHeaderFormat,
            Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
            Uri.EscapeDataString((oAuthConsumerSecret)))
        ));

        var postBody = "grant_type=client_credentials";

        HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);
        authRequest.Headers.Add("Authorization", authHeader);
        authRequest.Method = "POST";
        authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (Stream stream = authRequest.GetRequestStream())
        {
            byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
            stream.Write(content, 0, content.Length);
        }

        authRequest.Headers.Add("Accept-Encoding", "gzip");

        WebResponse authResponse = authRequest.GetResponse();
        // deserialize into an object
        TwitAuthenticateResponse twitAuthResponse;
        /*using (authResponse)
        {
            using (var reader = new StreamReader(authResponse.GetResponseStream()))
            {
                
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objectText = reader.ReadToEnd();
                twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
            }
        }

        // Do the timeline
        var timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count=5";
        var timelineUrl = string.Format(timelineFormat, screenname);
        HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
        var timelineHeaderFormat = "{0} {1}";
        timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
        timeLineRequest.Method = "Get";
        WebResponse timeLineResponse = timeLineRequest.GetResponse();
        var timeLineJson = string.Empty;
        using (timeLineResponse)
        {
            using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
            {
                timeLineJson = reader.ReadToEnd();
            }
        }*/
    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}
