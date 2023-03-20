using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    public void Init(float v, Vector3 d)
    {
        velocity = v;
    }

    public void Init(float v)
    {
        velocity = v;
    }

    public float Init()
    {
        return velocity;
    }

    public void Move(float vel, Vector3 dir)
    {
        transform.position = transform.position + vel * dir * Time.deltaTime;
    }

    public void MoveRigidBody(GameObject g, Vector3 v,float vel)
    {
        GetComponent<Rigidbody>().MovePosition(g.transform.position + (v * vel) * Time.deltaTime);
    }
    public void Rotate3D(GameObject g, float vel, float powerRotation)
    {
        g.transform.Rotate(0, vel * powerRotation * Time.deltaTime, 0);
    }
    
    public void MoveGameObject(GameObject f, Vector3 g, float vel)
    {
        f.transform.position +=  g * vel * Time.deltaTime;
    }

    //Esto es solo para los followers.
    public void MoveVector(Vector3 g, float vel)
    {
        transform.position = Vector3.MoveTowards(transform.position, g, vel * Time.deltaTime);
    }

    public void MoveLerp(Vector3 f, float vel)
    {
        transform.position = Vector3.Lerp(transform.position, f, Time.deltaTime * vel);
    }

}
