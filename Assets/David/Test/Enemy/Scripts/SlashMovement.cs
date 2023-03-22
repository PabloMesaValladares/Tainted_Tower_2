using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMovement : MonoBehaviour
{
    public float speed;
    bool move;
    [SerializeField]
    Vector3 directionToGo;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
        else
            speed = 0;
    }

    public void MoveDirection(Vector3 direction)
    {
        directionToGo = direction;
        move = true;
        transform.LookAt(directionToGo);
    }
}
