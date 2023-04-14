using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheathedWeapon : MonoBehaviour
{
    public float timerBD;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < timerBD)
        {
            counter += Time.deltaTime;
        }
        else
            gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        counter = 0;
    }
}
