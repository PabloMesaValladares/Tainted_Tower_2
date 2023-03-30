using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/SlashAttack")]

public class SlashAttack : Action
{
    public float NoSlashes;
    public float offset;
    float counter = 0;
    public override void Act(Controller controller)
    {
        if(counter < NoSlashes)
        {
            GameObject attack = PoolingManager.Instance.GetPooledObject("Slash");
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position);
            attack.SetActive(true);
        }
    }

    public override void RestartVariables()
    {
        counter = 0;
    }
}
