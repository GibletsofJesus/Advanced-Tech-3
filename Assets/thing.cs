using UnityEngine;
using System.Collections;

public class thing : MonoBehaviour {

    public Twitter.API.Tweet tweet;
    public TextMesh text;

    public void hi()
    {
        text.text = tweet.Text;
        transform.localScale = new Vector3(numbersToSliders.findRank(tweet.Favs)*2, 0.5f, numbersToSliders.findRank(tweet.RTs)*2);
        text.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
    }
}
