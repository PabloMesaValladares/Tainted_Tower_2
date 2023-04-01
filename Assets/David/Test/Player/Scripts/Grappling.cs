using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    public Transform cam;
    public Transform gunTip;
    public Transform markpos;
    Transform posToGo;
    public Vector3 offset;
    public LineRenderer lr;
    public float grappleFov;
    public float[] normalFov;

    private PlayerController controller;
    private GrappleState grappling;

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

    [SerializeField]
    Transform posToGrab;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grappleAction = playerInput.actions["Grapple"];
        player = this.gameObject;

        controller = player.GetComponent<PlayerController>();
        grappling = controller.grappling;

        posToGrab = null;

    }

    private void Update()
    {
        //if (grappleAction.triggered)
        //{
        //    StartGrapple();
        //}


        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (grapple)
            lr.SetPosition(0, gunTip.position);
    }
    public void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grapple = true;

        if (posToGrab !=null)
        {
            grapplePoint = posToGrab.position + transform.up * overshootYAxis;
            Debug.Log("Pillado");

            ChangeToMove();


            lr.enabled = true;

            lr.SetPosition(1, posToGrab.position);
        }
        else
        {
            grapplePoint = gunTip.position.normalized + Camera.main.transform.forward * maxGrappleDistance;
            StopGrapple();
        }
    }

    public void StopGrapple()
    {
        grapple = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
        ResetPosToGo();
    }

    public void ChangePosToGo(Transform pTG)
    {
        posToGrab = pTG;
    }

    public void ResetPosToGo()
    {
        posToGrab = null;
    }

    void ChangeToMove()
    {

        lr.enabled = false;

        gameObject.transform.LookAt(new Vector3(grapplePoint.x, gameObject.transform.position.y, grapplePoint.z));
        controller.changeState(controller.grappling);
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint; 
    }    
}
