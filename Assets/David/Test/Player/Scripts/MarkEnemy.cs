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

    public GameObject pointerCanvas;

    [SerializeField]
    CinemachineVirtualCamera enemyCamera;
    [SerializeField]
    CinemachineFreeLook MarkCamera;
    [SerializeField]
    GameObject Player;
    EnemyController enemyController;

    [HideInInspector]
    public InputAction markAction;
    CameraMove move;


    public float DistanceToCheck;
    public float maxUpDownDist;

    // Start is called before the first frame update
    void Start()
    {
        markAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Mark"];
        enemyController = GetComponent<EnemyController>();

        enemyCamera.gameObject.SetActive(false);
        MarkCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(markAction.IsPressed())
        {
            markEnemy();
        }
        else
        {
            ResetCamera();
        }
    }

    void markEnemy()
    {
        enemy = enemyController.GetCloseEnemy();
        float dist = 0;
        if (enemy != null)
        {
            float distPlayerEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distPlayerEnemy <= DistanceToCheck)
            {
                enemyCamera.LookAt = enemy.transform;
                Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                transform.LookAt(posToLookAt);
                enemyCamera.m_Lens.FieldOfView = 20 + dist;


                MarkCamera.gameObject.SetActive(false);
                enemyCamera.gameObject.SetActive(true);
                enemyCamera.Priority = 15;
            }
            else
            {
                enemyCamera.gameObject.SetActive(false);
                MarkCamera.gameObject.SetActive(true);
                MarkCamera.Priority = 15;
                pointerCanvas.SetActive(true);
            }
        }
        else
        {
            enemyCamera.gameObject.SetActive(false);
            MarkCamera.gameObject.SetActive(true);
            MarkCamera.Priority = 15;
            pointerCanvas.SetActive(true);
           
        }
    }

    public GameObject returnEnemy()
    {
        return enemy;
    }

    void ResetCamera()
    {
        enemyCamera.gameObject.SetActive(false);
        MarkCamera.gameObject.SetActive(false);
        enemy = null;
        enemyCamera.Priority = 5;
        MarkCamera.Priority  = 5;
        MarkCamera.LookAt = markPos.transform;
        pointerCanvas.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceToCheck);
    }
}
