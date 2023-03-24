using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeCut : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;
    [Header("Parameters")]
    public float attackCounter;
    [SerializeField] Vector3 playerPos;
    [Header("Debug")]
    [SerializeField] float counter;
    Rigidbody rb;

    public float dashCoold;
    public float dashForce;
    public float dashUpwardForce;

    bool look;

    // Start is called before the first frame update
    void Start()
    {
        look = true;
        rb = GetComponent<Rigidbody>();
        //player = GameObject.FindGameObjectWithTag("Player");
        counter = 0;
        playerPos = player.transform.position;
    }

    private void OnEnable()
    {
        CutAttack.MoveEffect += DoSlash;
        CutAttack.StopEffect += LookAround;
    }


    // Update is called once per frame
    void Update()
    {
        if(look)
        {
            playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);
        }
        
    }

    public void DoSlash()
    {
        look = false;
        SpeedControl();
        Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
        //Invoke(nameof(LookAround), dashCoold);
    }

    public void LookAround()
    {
        look = true;
        rb.velocity = Vector3.zero;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > dashForce)
        {
            Vector3 limitedVel = flatVel.normalized * dashForce;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void StopDash()
    {
        rb.velocity = Vector3.zero;
        counter = 0;
    }
}
