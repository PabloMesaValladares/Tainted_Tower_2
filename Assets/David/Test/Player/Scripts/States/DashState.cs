using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DashState : State
{
    Transform orientation;

    float dashForce;
    float dashUpwardForce;
    float dashDuration;
    float dashStop;
    Vector3 previousInput;
    public DashState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.SetTrigger("dash");
        dashForce = character.dashController.dashForce;
        dashUpwardForce = character.dashController.dashUpwardForce;
        dashDuration = character.dashController.dashDuration;
        dashStop = character.dashController.dashStop;
        character.dashController.keepMomentum = true;
        velocity = Vector3.zero;
        previousInput = Vector3.zero;
        orientation = character.transform;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
            character.animator.SetTrigger("move");
            character.dashController.LastDashSpeed = forceToApply;

            if (character.dashController.previousSpeed == character.sprintSpeed)
            {
                Debug.Log("Vuelvo a esprintar");
                stateMachine.ChangeState(character.sprinting);
            }
            else
                stateMachine.ChangeState(character.standing);
            
        }
    }
    public override void Exit()
    {
        base.Exit();

    }
}
