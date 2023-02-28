using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float time;
    private float stopw;
    private bool started;

    public UnityEvent timerHasStopped;

    public void StartTimer(float t)
    {
        time = t;
        started = true;
    }

    private void Update()
    {
        if (started)
            time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            started = false;
            timerHasStopped.Invoke();
        }
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
