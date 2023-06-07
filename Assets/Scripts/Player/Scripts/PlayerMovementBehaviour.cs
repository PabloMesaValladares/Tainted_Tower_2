using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    private Rigidbody controller;
    private float speed; // speed movement sides
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            controller = rb;
    }

    public void setSpeed(float sp)
    {
        speed = sp;
    }

    public void setDir(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void setDir(Vector3 dir, float sp)
    {
        moveDirection = dir;
        speed = sp;
    }

    public void StopMovement()
    {
        moveDirection = Vector3.zero;
    }
    public void MoveForward(float speed)
    {
        moveDirection = Vector3.forward * speed;
    }
    public void MoveDown(float speed)
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }
    public void MoveBack(float speed)
    {
        moveDirection = Vector3.back * speed;
    }
    public void MoveRight(float speed)
    {
        moveDirection = Vector3.right * speed;
    }
    public void MoveLeft(float speed)
    {
        moveDirection = Vector3.left * speed;
    }
    public void MoveToByTime(Vector3 pos, float s)
    {
        speed = s * Time.deltaTime;
        moveDirection = pos;
    }
    public void MoveToSpecificPoint(Vector3 target)
    {
        Vector3 newDir = target - transform.position;
        controller.velocity = newDir;
    }
    public void MoveBackByTime(float s)
    {
        speed = s * Time.deltaTime;
        moveDirection = transform.position + Vector3.back;
    }
    public void AddGravity(float gravity)
    {
        moveDirection.y -= gravity * Time.deltaTime;
    }

    public void MoveRB()
    {
        controller.MovePosition(transform.position + moveDirection * Time.deltaTime);
    }

    public void MoveRB(Vector3 dir, float speed)
    {
        controller.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
    }

    public void MoveV3()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed);
    }

    public void MoveV3(Vector3 DirectionToGo, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, DirectionToGo, speed);
    }

    public void LerpV3(Vector3 DirectionToGo, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, DirectionToGo, speed);
    }
    public void LerpV3()
    {
        transform.position = Vector3.Lerp(transform.position, moveDirection, speed);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
