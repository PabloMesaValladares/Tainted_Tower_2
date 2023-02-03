using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Cinemachine;

public class MarkEnemy : MonoBehaviour
{
    GameObject enemy;
    GameObject player;
    EnemyController enemyController;
    //Camara
    public CinemachineFreeLook c_VirtualCamera;

    [HideInInspector]
    public InputAction markAction;

    // Start is called before the first frame update
    void Start()
    {
        markAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions["Mark"];
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(markAction.IsPressed())
        {
            if (enemy == null)
                enemy = enemyController.GetCloseEnemy();
            if(enemy != null) 
            { 
                c_VirtualCamera.m_LookAt = enemy.transform;
                Vector3 posToLookAt = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                transform.LookAt(posToLookAt);
            }
            else c_VirtualCamera.m_LookAt = this.transform;
        }
        else
        {
            enemy = null;
            c_VirtualCamera.m_LookAt = this.transform;
        }
    }

    public GameObject returnEnemy()
    {
        return enemy;
    }
}
