using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private float stopw;
    private bool started;

    public void StartTimer(float t)
    {
        time = t;
        started = true;
    }

    public float UpdateTimer()
    {
        if (started)
            time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            started = false;
        }

        return time;
    }

    public float UpdateStopWatch(bool state)
    {
        if(state)
        {
            if (started)
                stopw += Time.deltaTime;
        }
        
        return stopw;
    }

    public void RestartStopWatch()
    {
        stopw = 0;
    }
}
