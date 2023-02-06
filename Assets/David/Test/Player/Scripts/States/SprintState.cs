using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    bool grounded;
    bool sprint;
    bool dash;
    float playerSpeed;
    bool sprintJump;
    Vector3 cVelocity;
    float dashVelocity;
    public SprintState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = false;
        sprintJump = false;
        dash = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.sprintSpeed;
        grounded = character.ground.returnCheck();
        gravityValue = character.gravityValue;

        dashVelocity = character.dashController.dashForce;
    }

    public override void HandleInput()
    {
        base.Enter();
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if (!sprintAction.IsPressed() || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }
        if (jumpAction.triggered)
        {
            sprintJump = true;

        }

    }

    public override void LogicUpdate()
    {
        if (sprint)
        {
            if(!character.dashController.keepMomentum)
            character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
            else
            {
                character.animator.SetFloat("speed", 1.5f);
            }
        }
        else
        {
            stateMachine.ChangeState(character.standing);
        }

        if (sprintJump)
        {
            stateMachine.ChangeState(character.jumping);
        }
        else if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }
        else if (!grounded)
            stateMachine.ChangeState(character.falling);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;

        grounded = character.ground.returnCheck();

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        if (character.dashController.keepMomentum)
        {
            playerSpeed = character.sprintSpeed + dashVelocity;

            dashVelocity -= Time.deltaTime / character.velocityDampTime;
            if (dashVelocity <= 0)
            {
                character.dashController.keepMomentum = false;
                playerSpeed = character.sprintSpeed;
            }
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);


        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

}
