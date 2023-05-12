using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/SlashAttack")]

public class SlashAttack : Action
{
    public float NoSlashes;
    public float SlashSpeed;
    public float offset;
    float counter = 0;
    GameObject enemy;
    public override void Act(Controller controller)
    {
        if(counter < NoSlashes)
        {
            enemy = controller.gameObject;
            //enemy.GetComponent<Enemy>().animator.SetTrigger("attack");
            GameObject attack = PoolingManager.Instance.GetPooledObject("Slash");
            attack.GetComponent<SlashMovement>().speed = SlashSpeed;
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position);
            attack.SetActive(true);
        }
    }

    public override void RestartVariables()
    {
        //enemy.GetComponent<Enemy>().animator.ResetTrigger("attack");
        counter = 0;
    }
}
