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
    public GameObject NormalpointerCanvas;
    public GameObject HitpointerCanvas;

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

    public LayerMask notIgnore;
    public LayerMask markable;
    float markedLayer;


    public float DistanceToCheck;
    public GameObject markedObject;


    Camera Cam;
    public UnityEvent<Transform> sendMarked;
    public UnityEvent ResetMarked;


    // Start is called before the first frame update
    void Start()
    {
        markAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Mark"];
        lookAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Look"];
        enemyController = GetComponent<EnemyController>();

        enemy = null;

        Player = GameObject.FindGameObjectWithTag("Player");
        Cam = Camera.main;
        HitpointerCanvas.SetActive(false);

        var rawValue = markable.value;
        var layerValue = Mathf.Log(rawValue, 2);
        markedLayer = layerValue;
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 posToGrab = Cam.transform.forward;
        //Vector3 posToGrab = Cam.ScreenToWorldPoint(new Vector3(markPos.transform.position.x, markPos.transform.position.y, Cam.nearClipPlane));

        if (Physics.Raycast(transform.position, posToGrab, out hit, DistanceToCheck, notIgnore))
        {
            if (hit.collider.gameObject.layer == markedLayer)
            {
                markedObject = hit.collider.gameObject;
                HitpointerCanvas.SetActive(true);
                NormalpointerCanvas.SetActive(false);
                Debug.DrawRay(transform.position, hit.point, Color.green);
                sendMarked.Invoke(markedObject.transform);
            }
            else
            {
                HitpointerCanvas.SetActive(false);
                NormalpointerCanvas.SetActive(true);
                markedObject = null;
                Debug.DrawRay(transform.position, posToGrab, Color.black);
                ResetMarked.Invoke();
            }
        }
        else
        {
            HitpointerCanvas.SetActive(false);
            NormalpointerCanvas.SetActive(true);
            markedObject = null;
            Debug.DrawRay(transform.position, posToGrab, Color.black);
            ResetMarked.Invoke();
        }

    }

    void markEnemy()
    {
        enemy = enemyController.GetCloseEnemy();
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
