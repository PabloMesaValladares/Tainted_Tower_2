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
        jumpForce = character.jumpForce;

        rb = character.rb;

        rb.drag = 0;
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

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(character.transform.up * jumpForce, ForceMode.Impulse);


        character.changeState(character.falling);
    }
}
