using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPointScript : MonoBehaviour
{
    GameObject player; 
    PlayerController character;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        character = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {

        other.GetComponent<Grappling>().StopGrapple();
    }
}
