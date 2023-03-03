using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    public void SetVel(Vector3 cVel)
    {
        moveDirection = cVel;
    }

    public void moveRB(float speed)
    {
        rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
    }

    public void moveRB(Vector3 cV, float speed)
    {
        rb.AddForce(cV * speed * 10f, ForceMode.Force);
    }

    public void JumpRB(Vector3 cV, float speed, float force)
    {
        rb.AddForce(cV * speed * 10f * force, ForceMode.Force);
    }

}
