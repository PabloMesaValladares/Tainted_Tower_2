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
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    public InputAction grappleAction;

    private PlayerInput playerInput;
    private bool grapple;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grappleAction = playerInput.actions["Grapple"];
    }

    private void Update()
    {
        if (grappleAction.triggered)
            StartGrapple();

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

        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {

    }

    private void StopGrapple()
    {
        grapple = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

}
