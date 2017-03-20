using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verb : MonoBehaviour {

    private static int fontSize = 300;
    public string verbName;
    public int verbGroupNumber;

    private TextMesh textMesh;

    void Start()
    {
        textMesh = gameObject.GetComponent<TextMesh>();
        if(textMesh == null)
        {
            Debug.Log("Adding textMesh");
            textMesh = gameObject.AddComponent<TextMesh>();
            textMesh.text = verbName;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 0.03f;
            textMesh.anchor = TextAnchor.MiddleCenter;
        }
    }
}
