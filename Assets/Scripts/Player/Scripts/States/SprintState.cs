using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    float playerSpeed;
    bool grounded;
    bool sprint;
    bool dash;
    bool jump;
    Vector3 cVelocity;
    float dashVelocity;

    Rigidbody rb;
    Vector3 moveDirection;
    Transform orientation;
    GameObject playerObj;

    RaycastHit slopeHit;
    float playerHeight;
    float maxSlopeAngle;
    float groundDrag;
    float moveSpeed;
    float slopeAngle;

    bool attack;
    StaminaController stamController;

    public SprintState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = true;
        jump = false;
        dash = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        character.animator.SetTrigger("move");
        grounded = character.ground.returnCheck();
        gravityValue = character.gravityValue;

        dashVelocity = character.dashController.dashForce;

        rb = character.rb;
        orientation = character.orientation;
        moveSpeed = character.sprintSpeed;
        gravityValue = character.gravityValue;
        playerHeight = character.ground.normalColliderHeight;
        maxSlopeAngle = character.maxSlopeAngle;
        playerObj = character.playerObj;
        groundDrag = character.groundDrag;

        jumpForce = character.jumpForce;
        stamController = character.GetComponent<StaminaController>();
        stamController.Reduce(true);
        //character.dashController.keepMomentum = true;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);

        grounded = character.ground.returnCheck(); 
        SpeedControl();


        if (jumpAction.triggered)
        {
            if (OnSlope())
                Jump(jumpForce + (slopeAngle / maxSlopeAngle));
            else
                jump = true;
        }
        if (sprintAction.IsPressed() && velocity.sqrMagnitude > 0f)
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        } 
        if (stamController.ReturnStamina() < 0)
        {
            sprint = false;
        }
        if (attackAction.triggered)
            attack = true;
        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (sprint)
        {
            if (!character.dashController.keepMomentum)
                character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
            else
            {
                character.animator.SetFloat("speed", 1.5f);
            }
        }
        if (!sprint)
        {
            character.dashController.keepMomentum = false;
            stateMachine.ChangeState(character.standing);
        }

        if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }

        if (attack)
            stateMachine.ChangeState(character.attacking);
        if (!grounded)
            stateMachine.ChangeState(character.falling);


        rb.drag = groundDrag;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        moveDirection = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        moveDirection.y = 0;
        stamController.ReduceStamina();

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 15f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //on ground
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);


        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), character.rotationDampTime);

        rb.useGravity = !OnSlope();
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(character.transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            slopeAngle = angle;
            if (angle > maxSlopeAngle)
                slopeAngle = maxSlopeAngle;
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    float jumpForce;

    void Jump(float jumpF)
    {
        rb.drag = 0;
        character.animator.SetTrigger("jump");

        SpeedControl();
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(character.transform.up * jumpF, ForceMode.Impulse);

        character.changeState(character.falling);
    }

    public override void Exit()
    {
        base.Exit();
        stamController.Reduce(false);
    }

}
