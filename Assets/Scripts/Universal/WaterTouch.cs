using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTouch : MonoBehaviour
{

    Vector3 startingPos;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = startingPos;
    }
}
