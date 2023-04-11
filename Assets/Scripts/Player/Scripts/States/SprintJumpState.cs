using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintJumpState : State
{
    float gravityValue;
    bool grounded;
    float jumpHeight;
    float playerSpeed;
    bool dash;

    Vector3 airVelocity;

    Vector3 playerPrevPos;



    public SprintJumpState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();
        grounded = false;
        dash = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpForce;
        playerSpeed = character.sprintSpeed;
        gravityVelocity.y = 0;
        playerPrevPos = character.transform.position;
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!grounded)
        {
            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;

            //character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        }
        if (character.transform.position.y < playerPrevPos.y)
        {
            character.animator.SetTrigger("fall");
            stateMachine.ChangeState(character.falling);
        }
        playerPrevPos = character.transform.position;
        gravityVelocity.y += gravityValue * Time.deltaTime;
        //grounded = character.controller.isGrounded;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }


        if (dash)
        {
            stateMachine.ChangeState(character.dashjumping);
            character.dashController.startCooldown();
        }
    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }
    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        Debug.Log(gravityVelocity.y);
    }
}