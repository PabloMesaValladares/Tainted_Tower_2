using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GrappleState : State
{

    public GrappleState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        character.animator.SetTrigger("grapple");
    }
    public override void HandleInput()
    {

    }
    public override void LogicUpdate()
    {

    }
    public override void PhysicsUpdate()
    {

    }
    public override void Exit()
    {

    }
}
