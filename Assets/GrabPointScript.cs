using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPointScript : MonoBehaviour
{
    Renderer renderer;
    GameObject player;
    Grappling grappling;
    Transform mark;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        grappling = player.GetComponent<Grappling>();
        mark = grappling.markpos;
    }

    
}
