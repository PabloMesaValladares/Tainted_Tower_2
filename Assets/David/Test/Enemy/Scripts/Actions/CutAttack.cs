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

    Controller cont;
    bool done;
    public override void Act(Controller controller)
    {
        if(!done)
        {
            controller.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            cont = controller;
            rb = controller.rb;
            rb.useGravity = false;
            SpeedControl();
            Vector3 forceToApply = controller.transform.forward * dashForce + controller.transform.up * dashUpwardForce;
            controller.rb.AddForce(forceToApply, ForceMode.Impulse);
            done = true;
        }
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

    public override void RestartVariables()
    {
        done = false;
        MoveEffect();
        rb.velocity = Vector3.zero;
        cont.gameObject.transform.LookAt(cont.player.transform.position);
        cont.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        rb.useGravity = true;
    }
}

