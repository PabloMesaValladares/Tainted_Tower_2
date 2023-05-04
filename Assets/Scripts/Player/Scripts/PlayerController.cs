using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject playerObj;

    [Header("Movement")]
    public Transform orientation;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float startOfFall;
    public int minimumFall;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;

    [Header("Grapple")]
    public float grabSpeed;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    public float downForce;

    [Header("Change Stats")]
    public float reverseTimer;

    [HideInInspector]
    public Rigidbody rb;

    internal void Enable()
    {
        throw new NotImplementedException();
    }

    internal void Disable()
    {
        throw new NotImplementedException();
    }

    public PlayerMovementBehaviour playerMovement;

    [Header("Damage")]
    public GameObject weapon;
    public GameObject Sheathedweapon;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float attackRotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;

    [HideInInspector]
    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    public SprintJumpState sprintjumping;
    public DashState dashing;
    public DashJumpingState dashjumping;
    public FallState falling;
    public AttackState attacking;
    public GrappleState grappling;
    public GrabMoveState grapplemoving;

    public float gravityValue = -9.81f;
    [HideInInspector]
    public UnityEngine.InputSystem.PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;

    [HideInInspector]
    public DashController dashController;

    [HideInInspector]
    public GroundCheck ground;

    [Header("Effects")]
    public ParticleSystem Trail;
    public RageEffects Rage;

    public bool stopInput;

    public object Controls { get; internal set; }

    // Start is called before the first frame update
    private void Start()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        crouching = new CrouchingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        sprintjumping = new SprintJumpState(this, movementSM);
        dashing = new DashState(this, movementSM);
        dashjumping = new DashJumpingState(this, movementSM);
        falling = new FallState(this, movementSM);
        attacking = new AttackState(this, movementSM);
        grappling = new GrappleState(this, movementSM);
        grapplemoving = new GrabMoveState(this, movementSM);

        dashController = GetComponent<DashController>();
        ground = GetComponent<GroundCheck>();

        startYScale = transform.localScale.y;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerMovement = GetComponent<PlayerMovementBehaviour>();

        weapon.SetActive(false);
        Sheathedweapon.SetActive(false);

        movementSM.Initialize(standing);
        //Screen.lockCursor = true;
        //normalColliderHeight = controller.height;
        //gravityValue *= gravityMultiplier;

        Trail.Stop();
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (!stopInput)
            movementSM.currentState.PhysicsUpdate();
    }
    
    public void changeState(State state)
    {
        movementSM.ChangeState(state);
    }

    public void AttackEnded()
    {
        weapon.GetComponent<DamageDealer>().EndDealDamage();
    }
    public void StartAttack()
    {
        weapon.GetComponent<DamageDealer>().StartDealDamage();
    }
}
