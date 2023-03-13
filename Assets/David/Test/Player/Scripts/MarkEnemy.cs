using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Cinemachine;
using UnityEngine.Events;
public class MarkEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    public GameObject markPos;
    public bool marking;

    public GameObject pointerCanvas;

    [SerializeField]
    CinemachineVirtualCamera normalCamera;
    [SerializeField]
    CinemachineVirtualCamera AimCamera;
    [SerializeField]
    GameObject Player;
    EnemyController enemyController;

    [HideInInspector]
    public InputAction markAction;
    [HideInInspector]
    public InputAction lookAction;
    CameraMove move;

    public LayerMask markable;

    Vector3 markPosResetPos;

    public float DistanceToCheck;
    public float moveSpeed;
    public GameObject markedObject;


    public UnityEvent<Transform> sendMarked;


    // Start is called before the first frame update
    void Start()
    {
        markAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Mark"];
        lookAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Look"];
        enemyController = GetComponent<EnemyController>();

        enemy = null;

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        markPosResetPos = transform.position + (transform.forward * DistanceToCheck);
        marking = markAction.IsPressed();

        if(marking)
        {
            Mark();
        }
        else
        {
            ResetCamera();
        }
    }

    void Mark()
    {
        AimCamera.gameObject.SetActive(true);
        normalCamera.gameObject.SetActive(false);
        pointerCanvas.SetActive(true);
        RaycastHit hit; 
        MarkCameraMovement();

        Vector3 posToGrab = markPos.transform.position;
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, DistanceToCheck, markable))
        {
            sendMarked.Invoke(hit.collider.gameObject.transform);
            Debug.DrawRay(transform.position, hit.point, Color.green);
        }
        else
        {
            sendMarked.Invoke(markPos.transform);
            Debug.DrawRay(transform.position, posToGrab, Color.black);
        }
    }

    void markEnemy()
    {
        enemy = enemyController.GetCloseEnemy();
        float dist = 0; 
        //normalCamera.gameObject.SetActive(false);
        if (enemy != null)
        {
            float distPlayerEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distPlayerEnemy <= DistanceToCheck)
            {
                Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                transform.LookAt(posToLookAt);
            }
            else
            {
                MarkCameraMovement();
            }
        }
        else
        {
            MarkCameraMovement();
        }
    }

    public GameObject returnEnemy()
    {
        return enemy;
    }

    void MarkCameraMovement()
    {
        pointerCanvas.SetActive(true);

    }

    void ResetCamera()
    {
        AimCamera.gameObject.SetActive(false);
        normalCamera.gameObject.SetActive(true);
        enemy = null;
        pointerCanvas.SetActive(false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DistanceToCheck);
    }
}
