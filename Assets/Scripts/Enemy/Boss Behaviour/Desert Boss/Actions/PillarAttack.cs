using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/DBPillarAttack")]

public class PillarAttack : Action
{
    public override void Act(Controller controller)
    {
        GameObject pilar = PoolingManager.Instance.GetPooledObject("Pillar");
        pilar.transform.position = controller.player.transform.position;
        pilar.SetActive(true);
    }

    public override void RestartVariables()
    {

    }
}