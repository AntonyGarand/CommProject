using UnityEngine;
using System.Collections;

public class SimpleTimer : MonoBehaviour
{
    public float targetTime = 0;

    SimpleTimer(float targetTime)
    {
        this.targetTime = targetTime;
    }

    void Update()
    {
        if (targetTime <= 0.0f)
        {

        }

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        
    }

}