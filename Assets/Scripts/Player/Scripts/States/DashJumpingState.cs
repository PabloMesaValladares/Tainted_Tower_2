using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashJumpingState : State
{
    Transform orientation;

    float dashForce;
    float dashUpwardForce;
    float dashDuration;
    float dashStop;

    Vector3 previousInput;
    bool grounded;


    float maxYSpeed;
    Rigidbody rb;
    public DashJumpingState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;

        dashForce = character.dashController.dashForce;
        dashUpwardForce = character.dashController.dashUpwardForce;
        dashDuration = character.dashController.dashDuration;
        dashStop = 0;
        character.dashController.keepMomentum = true;
        velocity = Vector3.zero;
        previousInput = Vector3.zero;
        orientation = character.transform;

        maxYSpeed = character.dashController.maxYSpeed;

        rb = character.rb;
        rb.drag = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        character.Trail.Play();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
            stateMachine.ChangeState(character.standing);
    }
    public override void HandleInput()
    {
        base.HandleInput();
        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        Vector3 inputVector = new Vector3(input.x, 0, input.y);
        if (inputVector != Vector3.zero)
        {
            velocity = inputVector;
            previousInput = inputVector;
        }
        else
            velocity = previousInput;


        velocity = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        velocity.y = 0f;
        SpeedControl();

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        if (velocity != Vector3.zero)
            forceToApply = velocity * dashForce + orientation.up * dashUpwardForce;

        forceToApply.y = 0;

        if (dashDuration > dashStop)
        {
            rb.AddForce(forceToApply, ForceMode.Impulse);
            dashStop += Time.deltaTime;
        }
        else
        {
            character.dashController.LastDashSpeed = forceToApply;
            character.dashController.dashForce = dashForce;
            stateMachine.ChangeState(character.falling);
        }
        //grounded = character.controller.isGrounded;
    }
    public override void Exit()
    {
        base.Exit();

        rb.useGravity = true;
        character.Trail.Stop();
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > dashForce)
        {
            Vector3 limitedVel = flatVel.normalized * dashForce;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if (rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }
    }
}
