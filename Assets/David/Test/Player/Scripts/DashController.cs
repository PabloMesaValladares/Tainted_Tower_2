using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    bool dash;
    [Header("Dash")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    public float dashStop;
    public float dashspeedChangeFactor;


    public float previousSpeed;

    [HideInInspector]
    public Vector3 LastDashSpeed;
    public bool keepMomentum;

    [Header("Cooldown")]
    public float dashCooldown = 3.0f;
    [SerializeField]
     float dashCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        dashCooldownTimer = 0;
        dash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dash)
        {
            if(dashCooldownTimer < dashCooldown)
            {
                dashCooldownTimer += Time.deltaTime;
            }
            else
            {
                dash = true;
                dashCooldownTimer = 0;
            }
        }
    }

    public bool checkIfDash()
    {
        return dash;
    }
    
    public void startCooldown()
    {
        dash = false;
    }
}
