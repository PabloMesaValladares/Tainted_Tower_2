using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/AttackBeeAction")]

public class AttackBeeAction : Action
{
    public float attackVel;
    [SerializeField]
    private float distAttack, maxdistAttack;
    //private GameObject bullet;
    public override void Act(Controller controller)
    {
        /*
        distAttack = Vector3.Distance(new Vector3(controller.player.transform.position.x, controller.enemy.gameObject.transform.position.y, controller.player.transform.position.z), controller.enemy.gameObject.transform.position);

        if (distAttack >= maxdistAttack)
        {
            controller.enemy.GetComponent<MovementBehavior>().MoveVector(new Vector3(controller.player.transform.position.x, controller.enemy.gameObject.transform.position.y, controller.player.transform.position.z), attackVel);
        }
        if(distAttack <= maxdistAttack)
        {
            GameObject attack = PoolingManager.Instance.GetPooledObject("bullet");
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position);
            attack.SetActive(true);
        }
        */
        GameObject attack = PoolingManager.Instance.GetPooledObject("bullet");
        attack.transform.position = controller.gameObject.transform.position;
        attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position);
        attack.SetActive(true);
    }

    public override void RestartVariables()
    {

    }

}
