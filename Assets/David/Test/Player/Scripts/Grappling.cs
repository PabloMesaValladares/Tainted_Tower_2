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
        grapplemoving = controller.grapplemoving;
        markpos = GetComponent<MarkEnemy>().markPos;
        normalFov = new float[cinemachineCam.Length];
        for (int i = 0; i < cinemachineCam.Length; i++)
            normalFov[i] = cinemachineCam[i].m_Lens.FieldOfView;
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
            gameObject.transform.LookAt(new Vector3(grapplePoint.x, gameObject.transform.position.y, grapplePoint.z));
            Debug.Log("Pillado");
            Invoke(nameof(ChangeToMove), grappleDelayTime);
        }
        else
        {
            grapplePoint = markpos.transform.position + posToGrab * maxGrappleDistance;
            gameObject.transform.LookAt(new Vector3(grapplePoint.x, gameObject.transform.position.y, grapplePoint.z));
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

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
        controller.changeState(grapplemoving);
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint; 
    }    
}
