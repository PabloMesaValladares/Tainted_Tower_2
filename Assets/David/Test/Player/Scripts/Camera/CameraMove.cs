using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

public class CameraMove : MonoBehaviour
{
    [Header("Move")]
    public Vector3 cameraOffset;
    public float sensitivity;
    protected Vector3 CamPos;



    [Header("Rotate")]
    UnityEngine.InputSystem.PlayerInput controller;
    [HideInInspector]
    public InputAction lookAction;

    float CameraX;
    float CameraY;
    [SerializeField] float MaxAngleY;
    [SerializeField] float MinAngleY;

    public GameObject Player;

    GameObject lookPosition;

    private void Start()
    {
        controller = Player.GetComponent<UnityEngine.InputSystem.PlayerInput>();
        lookAction = controller.actions["Look"];
        CamPos = transform.position - Player.transform.position;
        lookPosition = Player;
    }

    // Update is called once per frame
    void Update()
    {
        CameraX += lookAction.ReadValue<Vector2>().x * sensitivity;
        CameraY += lookAction.ReadValue<Vector2>().y * sensitivity;

        CameraY = Mathf.Clamp(CameraY, MinAngleY, MaxAngleY);
    }

    private void LateUpdate()
    {
        Vector3 dir = cameraOffset + CamPos;
        Vector3 actualCameraPos;
        Quaternion rotation = Quaternion.Euler(CameraY, CameraX, 0);
        actualCameraPos = Player.transform.position + rotation * dir;

        transform.position = actualCameraPos;
        transform.LookAt(lookPosition.transform);
    }

    public void ChangeLookAtObjective(GameObject l)
    {
        lookPosition = l;
    }

}
