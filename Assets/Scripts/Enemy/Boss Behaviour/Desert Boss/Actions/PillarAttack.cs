using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/DBPillarAttack")]

public class PillarAttack : Action
{
    public override void Act(Controller controller)
    {
        GameObject storm = PoolingManager.Instance.GetPooledObject("Pillar");
        storm.transform.position = controller.player.transform.position;
        storm.SetActive(true);
    }

    public override void RestartVariables()
    {

    }
}