using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GrabMoveState : State
{

    Transform orientation;
    float dashForce;
    float dashUpwardForce;
    float playerSpeed;
    bool dash;
    Vector3 grapplePoint;
    Vector3 Offset;
    float overshootYAxis;
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
        playerSpeed = character.sprintSpeed;
        //character.animator.SetTrigger("dash"); 
        dashForce = character.dashController.dashForce;
        dashUpwardForce = character.dashController.dashUpwardForce;
        overshootYAxis = character.GetComponent<Grappling>().overshootYAxis;
        grapplePoint = character.GetComponent<Grappling>().GetGrapplePoint(); 
        orientation = character.transform;

        rb = character.rb;
        rb.drag = 0;
        rb.useGravity = true;


        counter = -1;

        velocityToSet = (grapplePoint - character.transform.position).normalized * dashForce;

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
            rb.velocity = Vector3.zero;
            stateMachine.ChangeState(character.standing);
            character.GetComponent<Grappling>().StopGrapple();
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
    float dist;

    public override void PhysicsUpdate()
    {
        if (moving == false || counter > 2)
        {
            stop = true;
        }

        dist = Vector3.Distance(grapplePoint, character.transform.position);

        Debug.Log(lastSqrMag);

        rb.velocity = velocityToSet;

        //if (dist > 8)
        //{

        //    float step = dashForce * Time.deltaTime;
        //    character.transform.position = Vector3.MoveTowards(character.transform.position, grapplePoint, step);
        //}
        //else
        //{
        //    rb.velocity = character.transform.forward * dashForce;
        //    //if (grapplePoint.y > character.transform.position.y)
        //    //    stateMachine.ChangeState(character.jumping);
        //    //else
        //    //    stateMachine.ChangeState(character.standing);
        //    //character.GetComponent<Grappling>().StopGrapple();
        //}

        //if (dist < 3 && character.ground.returnCheck())
        //{
        //    moving = false;
        //}
        //else
        //{
        //    counter += Time.deltaTime;
        //}
    }
    public override void Exit()
    {

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

    private void ExecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(character.transform.position.x, character.transform.position.y - 1f, character.transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        JumpToPosition(grapplePoint, highestPointOnArc);
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {

        velocityToSet = CalculateJumpVelocity(character.transform.position, targetPosition, trajectoryHeight);

        Debug.Log(velocityToSet);

        rb.AddForce(velocityToSet, ForceMode.Impulse);
        //rb.velocity = velocityToSet * 2;

    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

}
