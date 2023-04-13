using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class BossActivate : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    GameObject player;

    private void Start()
    {
        enemy.GetComponent<Controller>().detect = false;
        //GetComponentInParent<Controller>().enabled = false;
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        enemy.GetComponent<Controller>().detect = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
        enemy.GetComponent<Controller>().detect = false;
    }

}
