using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verbGroup : MonoBehaviour {

    public static int fontSize = 300;
    public static Font font;

    public string groupName;
    public string[] goodMessages;
    public string[] badMessages;

    private TextMesh textMesh;

    private int goodMessageIndex;
    private int badMessageIndex;

    void start()
    {
        textMesh = gameObject.GetComponent<TextMesh>();
        if(textMesh == null)
        {
            Debug.Log("Adding textMesh");
            textMesh = gameObject.AddComponent<TextMesh>();
            textMesh.text = groupName;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 0.03f;
            textMesh.anchor = TextAnchor.MiddleCenter;
        }
    }

    public string getGoodMessage()
    {
        return goodMessages[goodMessageIndex++];
    }

    public string getBadMessage()
    {
        return badMessages[badMessageIndex++];
    }

    public Transform buildVerbUI()
    {
        return transform;
    }

}