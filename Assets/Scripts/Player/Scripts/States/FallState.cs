using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    bool grounded;
    bool dash;

    Vector3 dashVelocity;
    float dashForce;

    Rigidbody rb;
    Vector3 moveDirection;
    Transform orientation;
    GameObject playerObj;

    RaycastHit slopeHit;
    float playerHeight;
    float maxSlopeAngle;
    float moveSpeed;
    float airMultiplier;
    float gravity;

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
        gravity = character.gravityValue;
        gravityVelocity.y = 0;
        
        dashVelocity = character.dashController.LastDashSpeed;
        dashForce = character.dashController.dashForce;


        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("fall");

        rb = character.rb;
        orientation = character.orientation;
        moveSpeed = character.walkSpeed;
        if (character.dashController.keepMomentum)
            moveSpeed = character.sprintSpeed;
        playerHeight = character.ground.normalColliderHeight;
        maxSlopeAngle = character.maxSlopeAngle;
        playerObj = character.playerObj;
        airMultiplier = character.airMultiplier;

        grounded = false;
        rb.drag = 0;
        rb.useGravity = true;
    }

    public override void HandleInput()//Detectar el input, comprobando si un bot�n ha sido pulsado
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input


        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);

        grounded = character.ground.returnCheck();

        SpeedControl();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
        {
            //character.animator.SetTrigger("move");
            stateMachine.ChangeState(stateMachine.previousState);
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


        moveDirection = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        moveDirection.y = 0;
        moveDirection.y = character.gravityValue + moveSpeed * Time.deltaTime;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f , ForceMode.Force);
        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), character.rotationDampTime);

    }

    public override void Exit()
    {
        base.Exit();
    }
    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }
}