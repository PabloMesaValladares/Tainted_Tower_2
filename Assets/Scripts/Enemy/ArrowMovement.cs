using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    bool move;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = false;
    }


    private void Update()
    {
        if(move)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
    }
    public void Move()
    {
        move = true;
    }
}
