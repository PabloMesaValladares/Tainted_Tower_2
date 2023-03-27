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

    float counter;
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
            //SpeedControl();

            rb.AddForce(transform.forward * speed, ForceMode.Force);
            counter += Time.deltaTime;

            if (counter > 10)
            {
                rb.velocity = Vector3.zero;
                move = false;
                gameObject.SetActive(false);
            }
        }
        
        
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    public void MoveDirection(Vector3 direction)
    {
        //rb.velocity = Vector3.zero;
        directionToGo = direction;
        move = true;
        transform.LookAt(directionToGo);
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Slashed");
    }
}
