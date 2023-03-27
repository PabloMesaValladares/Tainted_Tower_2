using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class StandingState : State
{
    float gravityValue;
    bool grounded;

    Vector3 cVelocity;
    float dashVelocity;

    float moveSpeed;

    bool sprint;
    bool jump;
    bool crouch;
    bool dash;
    bool attack;
    bool grapple;

    Rigidbody rb;
    float playerSpeed;
    Vector3 moveDirection;
    Transform orientation;
    GameObject playerObj;

    RaycastHit slopeHit;
    float playerHeight;
    float maxSlopeAngle;
    float groundDrag;

    bool reverse;
    float counterReverse;
    float timerReverse;

    MarkEnemy mark;

    float jumpForce;
    public StandingState(PlayerController _character, StateMachine _stateMachine):base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();
        //character.animator.ResetTrigger("land");
        //character.animator.ResetTrigger("dash");
        //character.animator.ResetTrigger("jump");
        //character.animator.ResetTrigger("crouch");
        //character.animator.ResetTrigger("fall");
        //character.animator.SetTrigger("move");
        dash = false;
        jump = false;
        crouch = false;
        attack = false;
        sprint = false;
        grapple = false;

        grounded = character.ground.returnCheck();
        input = Vector2.zero;
        velocity = Vector3.zero;
        moveDirection = Vector3.zero;
        gravityVelocity.y = 0;


        dashVelocity = character.dashController.dashForce;

        rb = character.rb;
        orientation = character.orientation;
        moveSpeed = character.walkSpeed;
        gravityValue = character.gravityValue;
        playerHeight = character.ground.normalColliderHeight;
        maxSlopeAngle = character.maxSlopeAngle;
        playerObj = character.playerObj;
        groundDrag = character.groundDrag;
        rb.drag = groundDrag;

        counterReverse = 0;

        mark = character.GetComponent<MarkEnemy>();

        timerReverse = character.reverseTimer;


        jumpForce = character.jumpForce;
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        if (jumpAction.triggered)
        {
            if (OnSlope())
                Jump(jumpForce * 1.25f);
            else
                jump = true;
        }
        if (crouchAction.triggered)
        {
            crouch = true;
        }
        if (sprintAction.triggered)
        {
            sprint = true;
        }
        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }
        //if (attackAction.triggered)
        //    attack = true;


        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);

        if(reverse)
        {
            velocity = new Vector3(-input.x, 0, -input.y);

            counterReverse += Time.deltaTime;

            if (counterReverse > timerReverse)
                reverse = false;
        }

        //if (testAction.triggered)
        //    ReverseControls();

        grounded = character.ground.returnCheck();
        //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        SpeedControl();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);//Cambiamos la velocidad del animator para cambiar su animacion

        if (sprint)//cambia al estado dependiendo de la variable
            stateMachine.ChangeState(character.sprinting);
        else if (jump)
            Jump(jumpForce);
        else if (crouch)
            stateMachine.ChangeState(character.crouching);
        else if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }
        if (!grounded)
            stateMachine.ChangeState(character.falling);
        if (attack)
            stateMachine.ChangeState(character.attacking);


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        moveDirection = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        moveDirection.y = 0;
        
        if(mark.marking)
        {
            moveDirection = character.transform.forward.normalized * velocity.z + character.transform.right.normalized * velocity.x;
            moveDirection.y = 0;
        }


        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 15f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //on ground
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);


        if(mark.marking)
        {
            Quaternion targetRotation = Quaternion.Euler(0, character.cameraTransform.eulerAngles.y, 0);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, character.rotationDampTime);
        }
        else
        {
            if (velocity.sqrMagnitude > 0)
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), character.rotationDampTime);
        }
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(character.transform.position, Vector3.down, out slopeHit, playerHeight * 1.5f + 0.3f))
        {
            Debug.DrawRay(character.transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f), Color.yellow);
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f; 
    }

    public override void ReverseControls()
    {
        base.ReverseControls();

        reverse = true;
    }

    public override void ChangeAttributes(float s)
    {
        base.ChangeAttributes(s);

        moveSpeed += s;

    }


    void Jump(float jumpF)
    {
        rb.drag = 0;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(character.transform.up * jumpF, ForceMode.Impulse);

        character.changeState(character.falling);
    }
}
