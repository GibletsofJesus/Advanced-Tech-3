using UnityEngine;
using System.Collections;

public class speechBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void FormatString(string text, TextMesh textObject)
    {
        int maxLineChars = 35; //maximum number of characters per line...experiment with different values to make it work
        string[] words;
        var result = "";
        int charCount = 0;
        if (text != null)
        {
            words = text.Split(" "[0]); //Split the string into seperate words
            result = "";

            for (var index = 0; index < words.Length; index++)
            {

                var word = words[index].Trim();

                if (index == 0)
                {
                    result = words[0];
                    textObject.text = result;
                }

                if (index > 0)
                {
                    charCount += word.Length + 1; //+1, because we assume, that there will be a space after every word
                    if (charCount <= maxLineChars)
                    {
                        result += " " + word;
                    }
                    else
                    {
                        charCount = 0;
                        result += "\n " + word;
                    }
                    textObject.text = result;
                }
            }
            result = result.Replace("\\n", "\n ");
            textObject.text = result;
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (transform.localScale.x < .5f)
            transform.localScale += Vector3.one * Time.deltaTime * 0.5f;
        else
            Destroy(this.gameObject, 1);

        //transform.position += Vector3.up * Time.deltaTime;
	}
}
