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

    public GameObject Cam;

    GameObject lookPosition;

    public float detectDistance;
    float normalZ;
    Vector3 hitPos;
    public LayerMask layersToReact;
    private void Start()
    {
        controller = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        lookAction = controller.actions["Look"];
        Cam = Camera.main.gameObject;
        CamPos = Cam.transform.position - transform.position;
        lookPosition = this.gameObject;
        normalZ = cameraOffset.z;
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
        Quaternion rotation = Quaternion.Euler(CameraY,CameraX, 0);
        actualCameraPos = transform.position + rotation * dir;
        Cam.transform.position = ReturnNextCamPos(actualCameraPos);
        Cam.transform.LookAt(lookPosition.transform.position);



    }

    public void ChangeLookAtObjective(GameObject l)
    {
        lookPosition = l;
    }

    Vector3 ReturnNextCamPos(Vector3 actualPos)
    {
        Vector3 pos = actualPos;

        if(CheckCollisionOverlap(actualPos, actualPos + Vector3.right * detectDistance))
        {
            pos = actualPos - hitPos;
        }
        else if (CheckCollisionOverlap(actualPos, actualPos + Vector3.left * detectDistance))
        {
            pos = actualPos - hitPos;
        }

        if (CheckCollisionOverlap(actualPos, actualPos + Vector3.up * detectDistance))
        {
            pos = actualPos - hitPos;
        }
        else if (CheckCollisionOverlap(actualPos, actualPos + Vector3.down * detectDistance))
        {
            pos = actualPos - hitPos;
        }

        if(CheckCollisionOverlap(actualPos, actualPos + Vector3.forward * detectDistance))
        {
            Debug.Log("Algo esta en medio");
        }

        return pos;
    }

    public bool CheckCollisionOverlap(Vector3 position, Vector3 targetPositon)
    {
        RaycastHit hit;

        Vector3 direction = targetPositon - position;
        if (Physics.Raycast(position, direction, out hit, detectDistance, layersToReact))
        {
            Debug.DrawRay(position, direction * detectDistance, Color.yellow);
            hitPos = hit.collider.gameObject.transform.position;
            return true;
        }
        else
        {
            Debug.DrawRay(position, direction * detectDistance, Color.white);
            hitPos = Vector3.zero;
            return false;
        }
    }
    public bool CheckCollisionOverlap(Vector3 position, Vector3 targetPositon, float distance)
    {
        RaycastHit hit;

        Vector3 direction = targetPositon - position;
        if (Physics.Raycast(position, direction, out hit, distance, layersToReact))
        {
            Debug.DrawRay(position, direction * distance, Color.yellow);
            hitPos = hit.collider.gameObject.transform.position;
            return true;
        }
        else
        {
            Debug.DrawRay(position, direction * distance, Color.white);
            hitPos = Vector3.zero;
            return false;
        }
    }
}
