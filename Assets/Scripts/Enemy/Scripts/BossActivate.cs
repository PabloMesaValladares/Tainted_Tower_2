using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class BossActivate : MonoBehaviour
{
    private void Start()
    {

        GetComponentInParent<Controller>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        GetComponentInParent<Controller>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {

        GetComponentInParent<Controller>().enabled = false;
    }
}
