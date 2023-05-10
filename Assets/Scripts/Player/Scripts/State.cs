using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State 
{
    public PlayerController character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputAction sprintAction;
    public InputAction dashAction;
    public InputAction attackAction;
    public InputAction grappleAction;
    public InputAction testAction;

    public State(PlayerController _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        moveAction = character.playerInput.actions["Move"];
        jumpAction = character.playerInput.actions["Jump"];
        crouchAction = character.playerInput.actions["Crouch"];
        sprintAction = character.playerInput.actions["Sprint"];
        dashAction = character.playerInput.actions["Dash"];
        attackAction = character.playerInput.actions["Attack"];
        grappleAction = character.playerInput.actions["Grapple"];
        testAction = character.playerInput.actions["Test"];
    }

    public virtual void Enter()
    {

    }
    public virtual void HandleInput()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit()
    {

    }

    public virtual void ChangeAttributes(float s)
    {

    }

    public virtual void ReverseControls()
    {

    }

    public virtual void ReEnter()
    {

    }
}
