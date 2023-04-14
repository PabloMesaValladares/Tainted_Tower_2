using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{

    Rigidbody rb;
    float jumpForce;

    float counter;
    public JumpingState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();


        character.animator.SetTrigger("jump");
        jumpForce = character.jumpForce;

        rb = character.rb;

        rb.drag = 0;

        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);


        Jump();
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    void Jump()
    {
        
        SpeedControl();
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(character.transform.up * jumpForce, ForceMode.Impulse);



        character.changeState(character.falling);
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > jumpForce)
        {
            Vector3 limitedVel = flatVel.normalized * jumpForce;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
