using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GrabMoveState : State
{

    float playerSpeed;
    bool dash;
    Vector3 grapplePoint;
    bool stop;

    Rigidbody rb;

    float counter;
    bool moving;

    float lastSqrMag;

    private Vector3 velocityToSet;
    public GrabMoveState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        moving = true;
        dash = false;
        stop = false;
        playerSpeed = character.grabSpeed;
        //character.animator.SetTrigger("dash"); 
        grapplePoint = character.GetComponent<Grappling>().GetGrapplePoint(); 

        rb = character.rb;
        rb.drag = 0;
        rb.useGravity = true;


        counter = -1;

        velocityToSet = (grapplePoint - character.transform.position).normalized * playerSpeed;

        lastSqrMag = Mathf.Infinity;
        //ExecuteGrapple();
    }
    public override void HandleInput()
    {
        if (dashAction.triggered)
        {
            character.GetComponent<Grappling>().StopGrapple();
            dash = character.dashController.checkIfDash();
        }

        //SpeedControl();
        float sqrMag = (grapplePoint - character.transform.position).sqrMagnitude;

        if (sqrMag > lastSqrMag)
        {
            //rb.velocity = character.transform.forward * playerSpeed;
            //stateMachine.ChangeState(character.jumping);
            //character.GetComponent<Grappling>().StopGrapple();
        }
        lastSqrMag = sqrMag;

    }
    public override void LogicUpdate()
    {
        if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }
        if(stop)
        {
            stateMachine.ChangeState(character.standing);
            character.GetComponent<Grappling>().StopGrapple();
        }
    }
    public override void PhysicsUpdate()
    {

        rb.velocity = velocityToSet;

    }
    public override void Exit()
    {

    }

}
