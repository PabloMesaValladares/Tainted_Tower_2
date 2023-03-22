using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMovement : MonoBehaviour
{
    float speed;
    bool move;
    Vector3 directionToGo;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            rb.AddForce(directionToGo * Time.deltaTime, ForceMode.Impulse);
        }
        else
            speed = 0;
    }

    public void MoveDirection(Vector3 direction)
    {
        directionToGo = direction;
        move = true;
    }
}
