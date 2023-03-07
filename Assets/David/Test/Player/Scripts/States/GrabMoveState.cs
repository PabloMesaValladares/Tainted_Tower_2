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
    float overshootYAxis;
    bool stop;

    Rigidbody rb;

    float counter;
    

    public GrabMoveState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
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

        counter = -1;

        ExecuteGrapple();
    }
    public override void HandleInput()
    {
        //if (dashAction.triggered)
        //{
        //    dash = character.dashController.checkIfDash();
        //}

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
        if (counter > 3)
        {
            stop = true;
        }
        else
            counter += Time.deltaTime;
    }
    public override void Exit()
    {

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
    private Vector3 velocityToSet;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {

        velocityToSet = CalculateJumpVelocity(character.transform.position, targetPosition, trajectoryHeight);

        SetVelocity();

    }

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    public void ResetRestrictions()
    {
        //activeGrapple = false;
        //cam.DoFov(85f);
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
