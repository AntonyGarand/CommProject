using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defile : MonoBehaviour, IMessageViewer{

    public GameObject template;
    public float speed = 5f;
    public float liveTime = 10f;

    public Vector3 direction = Vector3.zero;
    private GameObject currentGameobject;
    
    public void showMessage(string message)
    {
        GameObject newDefiler = Instantiate(template);
        newDefiler.GetComponent<TextMesh>().text = message;
        currentGameobject = newDefiler;
        StartCoroutine("animate");
    }

    IEnumerator animate()
    {
        Destroy(currentGameobject, liveTime);
        while (currentGameobject != null)
        {
            Debug.Log("Speed: " + speed);
            Debug.Log("Time: " + Time.deltaTime);
            Debug.Log("Vector: " + direction.normalized);
            Debug.Log("Movement: " + direction.normalized * speed * Time.deltaTime);
            //currentGameobject.transform.Translate(direction.normalized * speed * Time.deltaTime);
            yield return null;
        }
    }
}
