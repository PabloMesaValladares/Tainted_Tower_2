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
    bool run;
    bool dash;
    Rigidbody rb;
    Vector3 currentVelocity;
    Vector3 cVelocity; 

    GameObject enemy;
    Transform orientation;
    float dashForce;
    float dashUpwardForce;
    Vector3 moveDirection;

    public AttackState(PlayerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)//Iniciar el estado
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()//Iniciar las variables
    {
        base.Enter();

        attack = false;
        character.animator.ResetTrigger("move");
        moveDirection = character.transform.forward;
        currentVelocity = Vector3.zero;
        dash = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("attack");
        character.animator.SetFloat("speed", 0f);
        orientation = character.transform; 
        dashForce = 5f;
        dashUpwardForce = 0f;
        rb = character.rb;

        rb.useGravity = true;

        character.Sheathedweapon.GetComponent<Timer>().enabled = false;
        character.Sheathedweapon.GetComponent<WeaponDisappearEffect>().StartDissapear();
        character.weapon.GetComponent<WeaponDisappearEffect>().StartAppear();
        character.weapon.GetComponent<DamageDealer>().StartDealDamage();
        input = moveAction.ReadValue<Vector2>();//detecta el movimiento desde input

        velocity = new Vector3(input.x, 0, input.y);
        moveDirection = character.cameraTransform.forward.normalized * velocity.z + character.cameraTransform.right.normalized * velocity.x;
        moveDirection.y = 0;
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z)), character.attackRotationDampTime);

    }

    public override void HandleInput()//Detectar el input, comprobando si un botón ha sido pulsado
    {
        base.HandleInput();

        attack = attackAction.IsPressed();
        run = sprintAction.IsPressed();
        if (dashAction.triggered)
            dash = character.dashController.checkIfDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;

        if (character.animator.GetCurrentAnimatorClipInfo(1).Length > 0)
            clipLenght = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;

        if (timePassed >= clipLenght / clipSpeed)
        { 
            if(attack)
            stateMachine.ChangeState(character.attacking);
       
            else
            {
                if (run)
                    stateMachine.ChangeState(character.sprinting);
                else
                    stateMachine.ChangeState(character.standing);
                character.animator.SetTrigger("move");
            }
        }

        if(dash)
        {
            stateMachine.ChangeState(character.dashing); 
            character.dashController.startCooldown();
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
            }

        }

    }

    public override void Exit()
    {
        base.Exit();
        character.weapon.GetComponent<WeaponDisappearEffect>().StartDissapear();
        character.animator.ResetTrigger("attack");
        character.Sheathedweapon.GetComponent<SheathedWeapon>().StartTimer();
        character.Sheathedweapon.GetComponent<WeaponDisappearEffect>().StartAppear();
        character.animator.applyRootMotion = false;
        character.weapon.GetComponent<DamageDealer>().EndDealDamage();
    }

}
