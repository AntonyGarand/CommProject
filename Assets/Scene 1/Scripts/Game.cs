using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public Vector3 centerPosition;
    public Vector3 scale;

    public Transform backgroundObject;

    public Verb[] verbs;
    public verbGroup[] verbGroups;
    private GameObject currentVerb;

    public GameObject goodEffect;
    public GameObject badEffect;

    public defile goodMessage;
    public IMessageViewer badMessage;

    public float dropSpeed;
    public float messageTime;

    private int currentVerbIndex;

    private dropState state;
    private float bottomHeight;
    private Vector3 verbSpawnPosition;
 
	void Start () {
        Debug.Log("Game Start");
        placeObjects();
        bottomHeight = 2 - (scale.y/2);
        state = dropState.WAITING;
        currentVerbIndex = 0;
        verbSpawnPosition = Vector3.zero;
        verbSpawnPosition.y = (scale.y / 2) - 1;
        verbSpawnPosition.z -= 1;
	}
	
    //Todo: Drop and switch the verbs
	void Update () {
        if(state == dropState.WAITING)
        {
            if(currentVerbIndex >= verbs.Length) {
                state = dropState.GAMEOVER;
            } else {
                Debug.Log("Dropping new verb!");
                StartCoroutine("drop");
            }
        }
	}

    IEnumerator drop()
    {
        Debug.Log("Verb drop start");
        state = dropState.DROPPING;
        //TODO: Spawn verb
        currentVerb = Instantiate(verbs[currentVerbIndex++].gameObject);
        Transform verbTransform = currentVerb.transform;
        verbTransform.position = verbSpawnPosition;
        while(verbTransform.position.y > bottomHeight)
        {
            verbTransform.Translate(0, -Time.deltaTime * dropSpeed, 0);
            yield return null;
        }
        StartCoroutine("moveToSide");
    }

    IEnumerator moveToSide()
    {
        state = dropState.MOVINGTOSIDE;
        Transform verbTransform = currentVerb.transform;
        Vector3 selectedSide = new Vector3();
        // Lazy side
        float movementRemaining = 2f;
        float rnd = currentVerb.GetComponent<Verb>().side;
        int selectedGroup;
        if (rnd == 1) {
            //Left
            selectedGroup = 1;
            selectedSide.x -= 2;
        } else if(rnd == 2) {
            //Right
            selectedSide.x += 2;
            selectedGroup = 2;
        } else {
            //Bottom
            selectedGroup = 3;
            selectedSide.y -= 0.1f;
        }

        while(movementRemaining > 0) {
            verbTransform.Translate(selectedSide * Time.deltaTime * dropSpeed);
            movementRemaining -= Time.deltaTime * dropSpeed;
            yield return null;
        }
        state = dropState.MESSAGE;
        StartCoroutine("showMessage", selectedGroup);
    }

    IEnumerator showMessage(int group)
    {
        string message = currentVerb.GetComponent<Verb>().message;
        Destroy(Instantiate(goodEffect, currentVerb.transform.position, goodEffect.transform.rotation), 4f);
        Destroy(currentVerb, 4f);
        goodMessage.showMessage(message);
        yield return new WaitForSeconds(messageTime);
        state = dropState.WAITING;
    }

    /// <summary>
    /// Build the initial platform and the three verb groups next to it
    /// </summary>
    public void placeObjects()
    {
        //Build the background
        backgroundObject.transform.position = centerPosition;
        backgroundObject.transform.localScale = scale;

        if(verbGroups.Length < 3)
        {
            Debug.Log("Need at least 3 verb groups!");
            return;
        }

        //Build and place the three verb groups
        Vector3 groupPosition = Vector3.zero;
        //First group is left, at -x scale, -y scale + 1
        groupPosition.z = centerPosition.z;
        groupPosition.y = 1 - (scale.y / 2);
        verbGroups[2].buildVerbUI().transform.position = groupPosition;
        groupPosition.x = -scale.x;
        groupPosition.y = 3 - (scale.y/2);
        verbGroups[0].buildVerbUI().transform.position = groupPosition;
        groupPosition.x *= -1;
        verbGroups[1].buildVerbUI().transform.position = groupPosition;

    }

    enum dropState {
        DROPPING,
        MOVINGTOSIDE,
        MESSAGE,
        WAITING,
        GAMEOVER
    }
}
