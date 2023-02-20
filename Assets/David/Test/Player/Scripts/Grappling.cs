using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    public Transform cam;
    public Transform gunTip;
    public Vector3 offset;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    private PlayerController controller;
    private GrappleState grappling;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    public InputAction grappleAction;

    private PlayerInput playerInput;
    private bool grapple;
    GameObject markpos;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grappleAction = playerInput.actions["Grapple"];
        player = this.gameObject;

        controller = player.GetComponent<PlayerController>();
        grappling = controller.grappling;
        markpos = GetComponent<MarkEnemy>().markPos;
    }

    private void Update()
    {
        if (grappleAction.triggered)
            controller.changeState(grappling);


        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (grapple)
            lr.SetPosition(0, gunTip.position);
    }
    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grapple = true;

        RaycastHit hit;

        Vector3 posToGrab = cam.forward + offset;

        if(Physics.Raycast(markpos.transform.position, posToGrab, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Debug.Log("Pillado");
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = markpos.transform.position + posToGrab * maxGrappleDistance;

            //Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    private void StopGrapple()
    {
        grapple = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;


        controller.changeState(controller.standing);
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        GetComponent<CharacterController>().Move(velocityToSet * Time.deltaTime);

        //cam.DoFov(grappleFov);
    }

    public void ResetRestrictions()
    {
        //activeGrapple = false;
        //cam.DoFov(85f);
    }
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}
