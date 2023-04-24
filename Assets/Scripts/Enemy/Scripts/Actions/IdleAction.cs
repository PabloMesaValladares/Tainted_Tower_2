using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/IdleAction")]

public class IdleAction : Action
{
    public int randSide;
    public bool sideMove;
    public float speed;

    public override void Act(Controller controller)
    {
        Vector3 playerPos = new Vector3(controller.player.transform.position.x, controller.transform.position.y, controller.player.transform.position.z);
        controller.transform.LookAt(playerPos);

        if(sideMove)
        {
            if(randSide == 0)
                controller.transform.localPosition += new Vector3(controller.transform.right.x * Time.deltaTime * 10, 0, 0);
            else
                controller.transform.localPosition -= new Vector3(controller.transform.right.x * Time.deltaTime * 10, 0, 0);
        }
    }

    public override void RestartVariables()
    {
        randSide = Random.Range(0, 2);
        Debug.Log(randSide);
    }

}

