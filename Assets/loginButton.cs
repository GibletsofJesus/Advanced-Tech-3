using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loginButton : MonoBehaviour {

    public string consumerKey, consumerSecret;
    public string userID, ScreenName, Token, TokenSecret;
    int casey;
    public Twitter.AccessTokenResponse m_AccessTokenResponse;
    public Twitter.RequestTokenResponse m_RequestTokenResponse;

    public InputField text;

    // Use this for initialization
    void Start () {
        LoadTwitterUserInfo();
	}

    void LoadTwitterUserInfo()
    {
        m_AccessTokenResponse = new Twitter.AccessTokenResponse();

        m_AccessTokenResponse.UserId = userID;
        m_AccessTokenResponse.ScreenName = ScreenName;
        m_AccessTokenResponse.Token = Token;
        m_AccessTokenResponse.TokenSecret = TokenSecret;

        if (!string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.TokenSecret))
        {
            string log = "LoadTwitterUserInfo - succeeded";
            log += "\n    UserId : " + m_AccessTokenResponse.UserId;
            log += "\n    ScreenName : " + m_AccessTokenResponse.ScreenName;
            log += "\n    Token : " + m_AccessTokenResponse.Token;
            log += "\n    TokenSecret : " + m_AccessTokenResponse.TokenSecret;
            Debug.Log(log);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (string.IsNullOrEmpty(consumerKey) ||string.IsNullOrEmpty(consumerSecret))
        {
            GetComponentInChildren<Text>().text = "You need to register your game or application first.\n Click this button, register and fill CONSUMER_KEY and CONSUMER_SECRET of Demo game object.";
            casey = 1;
        }
        else
        {
            if (!string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName))
            {
                GetComponentInChildren<Text>().text = m_AccessTokenResponse.ScreenName + "\nClick to register with a different Twitter account";
            }

            else
            {
                GetComponentInChildren<Text>().text = "You need to register your game or application first.";
            }
            casey = 0;
        }
    }

    public void doThings()
    {
        switch (casey)
        {
            case 1:
                Application.OpenURL("http://dev.twitter.com/apps/new");
                break;
            case 0:
                StartCoroutine(Twitter.API.GetRequestToken(consumerKey, consumerSecret,
                    new Twitter.RequestTokenCallback(this.OnRequestTokenCallback)));
                break;
            default:
                break;
        }
    }


    void OnRequestTokenCallback(bool success, Twitter.RequestTokenResponse response)
    {
        if (success)
        {
            string log = "OnRequestTokenCallback - succeeded";
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);

            m_RequestTokenResponse = response;

            Twitter.API.OpenAuthorizationPage(response.Token);
        }
        else
        {
            print("OnRequestTokenCallback - failed.");
        }
    }
}
