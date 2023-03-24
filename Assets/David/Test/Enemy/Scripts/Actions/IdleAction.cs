using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/IdleAction")]

public class IdleAction : Action
{
    public override void Act(Controller controller)
    {
        Vector3 playerPos = new Vector3(controller.player.transform.position.x, controller.transform.position.y, controller.player.transform.position.z);
        controller.transform.LookAt(playerPos);
    }

}

