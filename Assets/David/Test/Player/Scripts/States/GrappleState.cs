using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GrappleState : State
{

    float playerSpeed;
    bool dash;
    public GrappleState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        dash = false;
        playerSpeed = character.sprintSpeed;
        character.animator.SetTrigger("grapple");
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

    }
    public override void Exit()
    {

    }
}
