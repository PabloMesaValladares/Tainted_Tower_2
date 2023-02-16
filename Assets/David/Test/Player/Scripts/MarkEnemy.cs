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
    [SerializeField]
    CinemachineVirtualCamera camera;
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
        move = camera.GetComponent<CameraMove>();
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

        if (enemy != null)
        {
            camera.LookAt = enemy.transform;
            Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(posToLookAt);
        }
        else
        {
            Vector3 posToLookAt = new Vector3(markPos.transform.position.x, transform.position.y, markPos.transform.position.z);
            transform.LookAt(markPos.transform);
        }
        camera.Priority = 15;
    }

    void CheckUpDown()
    {
        float distPlayerEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //Debug.Log("Distancia es " + distPlayerEnemy);

        if (distPlayerEnemy > DistanceToCheck)
        {
            ResetCamera();
        }
        
        else
        {
            if (enemy != null)
                camera.LookAt = enemy.transform;
            Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            camera.Priority += 10;
            transform.LookAt(posToLookAt);
        }
    }
    public GameObject returnEnemy()
    {
        return enemy;
    }

    void ResetCamera()
    {
        enemy = null;
        camera.Priority = 5;
        camera.LookAt = markPos.transform;
    }
}
