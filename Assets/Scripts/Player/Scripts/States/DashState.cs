using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
[CreateAssetMenu(menuName = "Player/State/Dash")]

public class DashState : State
{
    Transform orientation;

    float dashForce;
    float dashUpwardForce;
    float dashDuration;
    float dashStop;
    float maxYSpeed;
    Vector3 previousInput;

    Rigidbody rb;

    public DashState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.ResetTrigger("attack");
        character.animator.ResetTrigger("move");
        character.animator.ResetTrigger("fall");
        character.animator.SetTrigger("dash");


        dashForce = character.dashController.dashForce;
        dashUpwardForce = character.dashController.dashUpwardForce;
        dashDuration = character.dashController.dashDuration;
        dashStop = 0;
        character.dashController.keepMomentum = true;
        velocity = Vector3.zero;
        previousInput = Vector3.zero;
        orientation = character.transform;
        character.GetComponent<HealthBehaviour>().invencibility = true;

        maxYSpeed = character.dashController.maxYSpeed;

        rb = character.rb;
        rb.drag = 0;
        rb.useGravity = true;

        character.Trail.Play();
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

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(new Vector3(forceToApply.x, 0, forceToApply.z)), character.rotationDampTime);

        if (dashDuration > dashStop)
        {
            rb.AddForce(forceToApply, ForceMode.Impulse);
            dashStop += Time.deltaTime;
        }
        else
        {
            character.animator.SetTrigger("move");
            character.dashController.LastDashSpeed = forceToApply;

            if (stateMachine.previousState == character.attacking)
            {
                stateMachine.ChangeState(character.standing);
            }
            else
                stateMachine.ChangeState(stateMachine.previousState);


        }
    }
    public override void Exit()
    {
        base.Exit();

        character.GetComponent<HealthBehaviour>().invencibility = false;
        character.animator.ResetTrigger("dash");
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

        if(rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }
    }
}
