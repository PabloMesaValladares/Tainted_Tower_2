using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public float timer;
    [SerializeField] private float time;

    private void Awake()
    {
        time = timer;
    }

    private void Update()
    {
        if(UpdateTimer() == 0)
        {
            Destroy(gameObject);
        }
    }

    private float UpdateTimer()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
        }

        return time;
    }
}
