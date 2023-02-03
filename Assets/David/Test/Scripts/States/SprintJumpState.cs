using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintJumpState : State
{
    float timePassed;
    float jumpTime;


    public SprintJumpState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();

        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("jump");

        jumpTime = 1f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed > jumpTime)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.sprinting);
        }
        timePassed += Time.deltaTime;
    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }
}