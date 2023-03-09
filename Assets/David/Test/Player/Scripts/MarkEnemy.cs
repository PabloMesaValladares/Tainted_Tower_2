using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Cinemachine;

public class MarkEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    public GameObject markPos;
    public bool marking;

    public GameObject pointerCanvas;

    [SerializeField]
    CinemachineFreeLook normalCamera;
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

    // Start is called before the first frame update
    void Start()
    {
        markAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Mark"];
        lookAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Look"];
        enemyController = GetComponent<EnemyController>();

        enemy = null;

        markPosResetPos = transform.position + (transform.forward * DistanceToCheck);

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

        Vector2 look = lookAction.ReadValue<Vector2>();

        Vector3 lookPos = new Vector3(look.y, look.x, 0);

        //markPos.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, transform.position.z));

        normalCamera.m_Lens.FieldOfView = 20;
        normalCamera.m_LookAt = markPos.transform;

        Vector3 posToGrab = markPos.transform.position - transform.position;
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, DistanceToCheck, markable))
        {
            Debug.DrawRay(transform.position, hit.point, Color.green);
        }
        else
        {
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
        normalCamera.m_Lens.FieldOfView = 40;
        normalCamera.LookAt = this.transform;
        pointerCanvas.SetActive(false);
        markPos.transform.position = markPosResetPos;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DistanceToCheck);
    }
}
