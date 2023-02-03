using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class StandingState : State
{
    float gravityValue;
    Vector3 currentVelocity;
    bool grounded;
    float playerSpeed;

    Vector3 cVelocity;
    float dashVelocity;

    float moveSpeed;

    bool sprint;
    bool jump;
    bool crouch;
    bool dash;
    bool attack;
    public StandingState(PlayerController _character, StateMachine _stateMachine):base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();
        dash = false;
        jump = false;
        crouch = false;
        attack = false;
        sprint = false;
        grounded = character.ground.returnCheck();
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        dashVelocity = character.dashController.dashForce;

        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;
        character.animator.ResetTrigger("land");
        character.animator.ResetTrigger("dash");
        character.animator.ResetTrigger("jump");
        character.animator.ResetTrigger("crouch");
        character.animator.ResetTrigger("fall");
        character.animator.SetTrigger("move");
            
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        if(jumpAction.triggered)
        {
            jump = true;
        }
        if(crouchAction.triggered)
        {
            crouch = true;
        }
        if(sprintAction.triggered)
        {
            sprint = true;
        }
        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }
        if (attackAction.triggered)
            attack = true;

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);//Cambiamos la velocidad del animator para cambiar su animacion

        if (sprint)//cambia al estado dependiendo de la variable
            stateMachine.ChangeState(character.sprinting);
        else if (jump)
            stateMachine.ChangeState(character.jumping);
        else if (crouch)
            stateMachine.ChangeState(character.crouching);
        else if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }
        if(!grounded)
            stateMachine.ChangeState(character.falling);
        if (attack)
            stateMachine.ChangeState(character.attacking);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;


        grounded = character.ground.returnCheck();

        if (grounded && gravityVelocity.y < 0)
            gravityVelocity.y = 0f;


        if (character.dashController.keepMomentum)
        {
            playerSpeed = character.playerSpeed + dashVelocity;

            dashVelocity -= Time.deltaTime / character.velocityDampTime;
            if (dashVelocity <= 0)
            {
                character.dashController.keepMomentum = false;
                playerSpeed = character.playerSpeed;
            }
        }


        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);


        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);//Mueve el personaje a cuerdo a la velocidad

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);

    }
    

    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.LookRotation(velocity);
    }

}
