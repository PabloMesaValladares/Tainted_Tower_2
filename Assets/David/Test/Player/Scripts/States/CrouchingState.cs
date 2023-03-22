using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CrouchingState : State
{
    float playerSpeed;
    bool belowCeiling;
    bool crouchHeld;

    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;

    Rigidbody rb;
    Vector3 moveDirection;
    Transform orientation;
    GameObject playerObj;

    RaycastHit slopeHit;
    float playerHeight;
    float maxSlopeAngle;
    float groundDrag;
    float moveSpeed;
    float crouchYScale;
    float startYScale;
    public CrouchingState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        //character.animator.SetTrigger("crouch");
        belowCeiling = false;
        crouchHeld = false;
        gravityVelocity.y = 0;

        playerSpeed = character.crouchSpeed;
        gravityValue = character.gravityValue;


        rb = character.rb;
        orientation = character.orientation;
        moveSpeed = character.crouchSpeed;
        gravityValue = character.gravityValue;
        playerHeight = character.ground.normalColliderHeight;
        maxSlopeAngle = character.maxSlopeAngle;
        playerObj = character.playerObj;
        groundDrag = character.groundDrag;
        crouchYScale = character.crouchYScale;
        startYScale = character.startYScale;
        playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, crouchYScale, playerObj.transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);
        playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, startYScale, playerObj.transform.localScale.z);
        //character.animator.SetTrigger("move");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, crouchYScale, playerObj.transform.localScale.z);
        

        if (!crouchAction.IsPressed() && !belowCeiling)//Mientras no pulses el botón y no estes debajo de un techo sale del estado
        {
            crouchHeld = true;
        }


        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);

        grounded = character.ground.returnCheck();
        //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        SpeedControl();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
        }


        rb.drag = groundDrag;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * startYScale);

        moveDirection = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        moveDirection.y = 0;


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

    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        RaycastHit hit;
        Vector3 direction = targetPositon - character.transform.position;
        if (Physics.Raycast(character.transform.position, direction, out hit, startYScale, character.layersToReact))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * startYScale, Color.white);
            return false;
        }

        return false;
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
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
