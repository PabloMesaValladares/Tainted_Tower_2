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
    GameObject player;
    [SerializeField]
    GameObject camera;
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
            enemy = null;
            move.ChangeLookAtObjective(Player);
        }
    }

    void markEnemy()
    {
        enemy = enemyController.GetCloseEnemy();

        if (enemy != null)
        {
            CheckUpDown();
        }
        else move.ChangeLookAtObjective(Player);
    }

    void CheckUpDown()
    {
        float distPlayerEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        Debug.Log("Distancia es " + distPlayerEnemy);

        if (distPlayerEnemy > DistanceToCheck)
        {
            enemy = null;
            move.ChangeLookAtObjective(Player);
        }
        else if (enemy.transform.position.y > transform.position.y + maxUpDownDist || enemy.transform.position.y < transform.position.y - maxUpDownDist)
        {
            enemy = null; 
            move.ChangeLookAtObjective(Player);
        }
        
        else
        {
            move.ChangeLookAtObjective(enemy);
            Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(posToLookAt);
        }
    }
    public GameObject returnEnemy()
    {
        return enemy;
    }
}
