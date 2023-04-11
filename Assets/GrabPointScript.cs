using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPointScript : MonoBehaviour
{
    GameObject player;
    PlayerController character;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        character = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponentInParent<PlayerController>().rb.velocity = character.transform.forward * character.grabSpeed;
        other.gameObject.GetComponentInParent<PlayerController>().changeState(character.jumping);
        other.gameObject.GetComponentInParent<Grappling>().StopGrapple();
    }
}
