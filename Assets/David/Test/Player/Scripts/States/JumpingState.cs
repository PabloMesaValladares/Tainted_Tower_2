using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    float gravityValue;
    bool grounded;
    float jumpHeight;
    float playerSpeed;
    bool dash;

    Vector3 airVelocity;

    Vector3 playerPrevPos;

    Rigidbody rb;
    float jumpForce;
    float airMultiplier;
    Vector3 moveDirection;
    Transform orientation;

    public JumpingState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
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
        playerSpeed = character.walkSpeed;
        gravityVelocity.y = 0;
        playerPrevPos = character.transform.position;
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");

        grounded = character.ground.returnCheck();
        Jump();
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input


        //if (dashAction.triggered)
        //{
        //    dash = character.dashController.checkIfDash();
        //}

        velocity = character.playerVelocity;
        airVelocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

        airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
        airVelocity.y = 0f;
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

    }

    public override void Exit()
    {
        base.Exit();
    }

    void Jump()
    {
        rb.AddForce(character.transform.up * jumpForce, ForceMode.Impulse);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //Debug.Log(gravityVelocity.y);
    }
}
