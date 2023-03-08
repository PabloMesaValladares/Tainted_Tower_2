using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    public CinemachineFreeLook[] cinemachineCam;
    public Transform cam;
    public Transform gunTip;
    public Transform markpos;
    public Vector3 offset;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;
    public float grappleFov;
    public float[] normalFov;

    private PlayerController controller;
    private GrappleState grappling;
    private GrabMoveState grapplemoving;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;
    public float grappleDuration;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [HideInInspector]
    public InputAction grappleAction;

    private PlayerInput playerInput;
    private bool grapple;
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grappleAction = playerInput.actions["Grapple"];
        player = this.gameObject;

        controller = player.GetComponent<PlayerController>();
        grappling = controller.grappling;
        grapplemoving = controller.grapplemoving;
        normalFov = new float[cinemachineCam.Length];
        for (int i = 0; i < cinemachineCam.Length; i++)
            normalFov[i] = cinemachineCam[i].m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (grappleAction.triggered)
        {
            StartGrapple();
        }


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

        Vector3 posToGrab = markpos.transform.position - gunTip.position;
        if(Physics.Raycast(gunTip.position, posToGrab, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Debug.Log("Pillado");
            ChangeToMove();
        }
        else
        {
            grapplePoint = gunTip.position.normalized + posToGrab * maxGrappleDistance;
            StopGrapple();
        }

        Debug.Log(gunTip.position);
        lr.enabled = true;

        lr.SetPosition(1, grapplePoint);
    }

    public void StopGrapple()
    {
        grapple = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;


        controller.changeState(controller.standing);
        for (int i = 0; i < cinemachineCam.Length; i++)
            cinemachineCam[i].m_Lens.FieldOfView = normalFov[i];
    }

    void ChangeToMove()
    {
        for (int i = 0; i < cinemachineCam.Length; i++)
            cinemachineCam[i].m_Lens.FieldOfView = normalFov[i];

        gameObject.transform.LookAt(new Vector3(grapplePoint.x, gameObject.transform.position.y, grapplePoint.z));
        controller.changeState(grappling);
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint; 
    }    
}
