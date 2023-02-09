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


    public DashJumpingState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;
        character.animator.SetTrigger("dash");
        dashForce = 5f;
        dashUpwardForce = 0;
        dashDuration = 0.25f;
        dashStop = 0;
        gravityVelocity.y = 0;
        character.dashController.keepMomentum = true;


        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

        orientation = character.transform;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
            stateMachine.ChangeState(character.landing);
    }
    public override void HandleInput()
    {
        base.HandleInput(); 
        Vector3 inputVector = new Vector3(input.x, 0, input.y);
        if (inputVector != Vector3.zero)
        {
            velocity = inputVector;
            previousInput = inputVector;
        }
        else
            velocity = previousInput;
        velocity = new Vector3(input.x, 0, input.y);
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        if (velocity != Vector3.zero)
            forceToApply = velocity * dashForce + orientation.up * dashUpwardForce;

        if (dashDuration > dashStop)
        {
            character.controller.Move(forceToApply * dashForce * Time.deltaTime);
            dashStop += Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(character.falling);
        }
        grounded = character.controller.isGrounded;
    }
    public override void Exit()
    {
        base.Exit();

    }
}
