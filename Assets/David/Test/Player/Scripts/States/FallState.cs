using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    float gravityValue;
    bool grounded;
    float jumpHeight;
    float playerSpeed;
    bool dash;

    Vector3 airVelocity;
    Vector3 dashVelocity;
    float dashForce;

    Rigidbody rb;

    public FallState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
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
        
        dashVelocity = character.dashController.LastDashSpeed;
        dashForce = character.dashController.dashForce;


        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("fall");
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input


        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }

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
            character.dashController.keepMomentum = false;
        }


        //if (dash)
        //{
        //    stateMachine.ChangeState(character.dashjumping);
        //    character.dashController.startCooldown();
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //if (!grounded)
        //{
        //    velocity = character.playerVelocity;
        //    airVelocity = new Vector3(input.x, 0, input.y);

        //    velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        //    velocity.y = 0f;

        //    airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
        //    airVelocity.y = 0f;

        //    if (character.dashController.keepMomentum)
        //    {
        //        velocity = dashVelocity;
        //        velocity.y = 0f;
        //        playerSpeed = dashForce;
        //        dashForce -= Time.deltaTime / character.velocityDampTime;

        //        if (dashForce <= 0)
        //        {
        //            character.dashController.keepMomentum = false;
        //            playerSpeed = character.playerSpeed;
        //        }
        //    }
        //    character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        //}
        //gravityVelocity.y += gravityValue * Time.deltaTime;

        rb.drag = 0;

        rb.AddForce(airVelocity * playerSpeed * 10f, ForceMode.Force);
        grounded = character.ground.returnCheck();
    }

    public override void Exit()
    {
        base.Exit();
    }
}