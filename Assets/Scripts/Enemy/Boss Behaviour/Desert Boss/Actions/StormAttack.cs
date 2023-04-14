using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/DBStormAttack")]
public class StormAttack : Action
{
    public float stormOffset;
    float counter;

    public override void Act(Controller controller)
    {
        if (counter > stormOffset)
        {
            GameObject storm = PoolingManager.Instance.GetPooledObject("Storm");
            storm.transform.position = controller.player.transform.position;
            storm.SetActive(true);
            counter = 0;
        }
        else
            counter += Time.deltaTime;
    }

    public override void RestartVariables()
    {
        counter = 0;
    }
}
