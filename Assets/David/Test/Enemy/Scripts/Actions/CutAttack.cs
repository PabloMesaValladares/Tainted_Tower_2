using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/CutAttack")]

public class CutAttack : Action
{
    Rigidbody rb;
    public float dashForce;
    public float dashUpwardForce;

    public delegate void Move();
    public static Move MoveEffect;

    public override void Act(Controller controller)
    {
        rb = controller.rb;
        SpeedControl();
        Vector3 forceToApply = controller.transform.forward * dashForce + controller.transform.up * dashUpwardForce;
        controller.rb.AddForce(forceToApply, ForceMode.Impulse);
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
}

