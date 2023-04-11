using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/IdleBee")]

public class IdleBee : Action
{
    public float idleVel;
    public int maxRange;
    private int randomNumber;
    private Vector3 direction;
    public override void Act(Controller controller)
    {
        controller.enemy.GetComponent<MovementBehavior>().MoveGameObject(direction, idleVel);
    }

    public override void RestartVariables()
    {
        randomNumber = Random.Range(0, maxRange);
        if (randomNumber == 0)
        {
            direction = Vector3.forward;
        }
        else if (randomNumber == 1)
        {
            direction = Vector3.left;
        }
        else if (randomNumber == 2)
        {
            direction = Vector3.right;
        }
        else if (randomNumber == 3)
        {
            direction = Vector3.back;
        }
        else if (randomNumber != 0 && randomNumber != 1 && randomNumber != 2 && randomNumber != 3)
        {
            direction = Vector3.zero;
        }
    }

}



