using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheathedWeapon : MonoBehaviour
{
    public float timerBD;
    float counter;
    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
   

    public void StartTimer()
    {
        timer.StartTimer(timerBD);
        timer.enabled = true;
    }
}
