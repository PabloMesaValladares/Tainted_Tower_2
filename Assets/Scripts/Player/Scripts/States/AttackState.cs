using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class AttackState : State
{
    float timePassed;
    float clipLenght;
    float clipSpeed;
    bool attack;
    bool dash;
    Vector3 currentVelocity;
    Vector3 cVelocity; 

    GameObject enemy;
    Transform orientation;
    float dashForce;
    float dashUpwardForce;

    public AttackState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();

        attack = false;
        currentVelocity = Vector3.zero;
        dash = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("attack");
        character.animator.SetFloat("speed", 0f);
        orientation = character.transform; 
        dashForce = 5f;
        dashUpwardForce = 0f;

        character.Sheathedweapon.SetActive(false);
        character.weapon.SetActive(true);
    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        if (attackAction.triggered)
            attack = true;
        if (dashAction.triggered)
            dash = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;

        if (character.animator.GetCurrentAnimatorClipInfo(1).Length > 0)
            clipLenght = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;

        if (timePassed >= clipLenght / clipSpeed && attack)
            stateMachine.ChangeState(character.attacking);
        
        else if(timePassed >= clipLenght / clipSpeed)
        {
            stateMachine.ChangeState(character.standing);
            character.animator.SetTrigger("move");
        }

        if(dash)
        {
            stateMachine.ChangeState(character.dashing);
            character.animator.SetTrigger("dash");
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (character.TryGetComponent(out MarkEnemy mark))
        {
            enemy = mark.returnEnemy();
            Vector3 forceToApply = Vector3.zero;
            if (enemy != null)
            {
                forceToApply = orientation.forward * dashForce + Vector3.up * dashUpwardForce;

                //if (Vector3.Distance(character.transform.position, enemy.transform.position) > 1.5f)
                    //character.controller.Move(forceToApply * dashForce * Time.deltaTime);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        character.weapon.SetActive(false);
        character.animator.ResetTrigger("attack");
        character.Sheathedweapon.GetComponent<SheathedWeapon>().StartTimer();
        character.Sheathedweapon.SetActive(true);
        character.animator.applyRootMotion = false;
    }

}
