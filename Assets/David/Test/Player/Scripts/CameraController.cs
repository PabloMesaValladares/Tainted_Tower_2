using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
   public float PlayerCameraDistance { get; set; }
    public Transform cameraTarget;

    UnityEngine.InputSystem.PlayerInput controller;

    [HideInInspector]
    public InputAction lookAction;
    [HideInInspector]
    public InputAction lookActionMouse;
    [HideInInspector]
    public InputAction mousePos;
    [HideInInspector]
    public InputAction scroll;

    Camera playerCamera;
    float zoomSpeed = 25f;

    private void Start()
    {
        PlayerCameraDistance = 10f;
        playerCamera = GetComponent<Camera>();
        controller = cameraTarget.GetComponent<UnityEngine.InputSystem.PlayerInput>();
        lookAction = controller.actions["Look"];
        lookActionMouse = controller.actions["LookMouse"];
        mousePos = controller.actions["MousePos"];
        scroll = controller.actions["Scroll"];
    }

    private void Update()
    {


        transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y - PlayerCameraDistance, cameraTarget.position.z + PlayerCameraDistance);
    }
}
