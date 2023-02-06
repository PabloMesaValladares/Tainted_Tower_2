using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [Header("Move")]
    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;

    [Header("Rotate")]
    UnityEngine.InputSystem.PlayerInput controller;
    [HideInInspector]
    public InputAction lookAction;
    [HideInInspector]
    public InputAction lookActionMouse;
    [HideInInspector]
    public InputAction mousePos;
    public float velocityDampTime;
   
    private Vector2 dragOrigin;

    protected Vector3 velocity;
    protected Vector2 input;
    Vector3 currentVelocity;
    Vector3 cVelocity;

    public GameObject Cam;

    private void Start()
    {
        velocity = Vector2.zero;
        input = Vector2.zero;
        controller = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        lookAction = controller.actions["Look"];
        lookActionMouse = controller.actions["LookMouse"];
        mousePos = controller.actions["Mouse"];
        Cam = Camera.main.gameObject;
        cameraOffset = Cam.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        if (lookAction.triggered)
            RotateCameraConsole();
        else if(lookActionMouse.IsPressed())
        {
            dragOrigin = mousePos.ReadValue<Vector2>();
            RotateCameraMouse();
        }
        else
        {
            Vector3 newPosition = transform.position + cameraOffset;
            Cam.transform.position = Vector3.Slerp(Cam.transform.position, newPosition, smoothFactor);
        }
    }

    public void RotateCameraConsole()
    {
        input = lookAction.ReadValue<Vector2>();//detecta el movimiento desde input
        velocity = new Vector3(0, input.x, input.y);


        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, velocityDampTime);

        Cam.transform.Rotate(currentVelocity, Space.World);
    }

    public void RotateCameraMouse()
    {

        Vector3 pos = Camera.main.ScreenToViewportPoint(mousePos.ReadValue<Vector2>() - dragOrigin);
        Vector3 move = new Vector2(pos.x * velocityDampTime, -pos.y * velocityDampTime);

        Transform camPosition = Cam.transform;

        camPosition.RotateAround(transform.position,
                                Vector3.up,
                                move.x);

        camPosition.RotateAround(transform.position,
                                        Vector3.right,
                                        move.y);

        Cam.transform.rotation = new Quaternion(camPosition.rotation.x, camPosition.rotation.y, 0, camPosition.rotation.w);

    }
}
