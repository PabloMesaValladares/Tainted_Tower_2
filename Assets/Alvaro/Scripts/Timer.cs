using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private readonly bool started;

    public void StartTimer(float t)
    {
        time = t;
    }

    public float UpdateTimer()
    {
        if (started)
            time -= Time.deltaTime;

        if (time <= 0)
            time = 0;

        return time;
    }
}
