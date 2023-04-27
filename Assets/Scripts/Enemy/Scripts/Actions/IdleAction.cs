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
        Vector3 playerPos = new(controller.player.transform.position.x, controller.transform.position.y, controller.player.transform.position.z);
        controller.transform.LookAt(playerPos);

        if(sideMove)
        {
            if(randSide == 0)
                controller.transform.position += speed * Time.deltaTime * controller.transform.right;
            else
                controller.transform.position -= speed * Time.deltaTime * controller.transform.right;
        }
    }

    public override void RestartVariables()
    {
        randSide = Random.Range(0, 2);
        Debug.Log(randSide);
    }
}