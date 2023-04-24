using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GrappleState : State
{

    float playerSpeed;
    bool dash;

    float counter;
    public GrappleState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        dash = false;
        counter = 0;
        playerSpeed = character.sprintSpeed;
        character.torsoAnimator.SetTrigger("grapple"); 
    }
    public override void HandleInput()
    {
        if (dashAction.triggered)
        {
            dash = character.dashController.checkIfDash();
        }
    }
    public override void LogicUpdate()
    {
        if (dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.dashController.previousSpeed = playerSpeed;
            character.dashController.startCooldown();
        }

    }
    public override void PhysicsUpdate()
    {
        if (counter > 0.2)
        {
            character.changeState(character.grapplemoving);
        }
        else
            counter += Time.deltaTime;
    }
    public override void Exit()
    {

    }
}
