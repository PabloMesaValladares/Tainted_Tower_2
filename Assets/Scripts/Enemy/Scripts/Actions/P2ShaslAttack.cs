using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Action/P2ShaslAttack")]

public class P2ShaslAttack : Action
{
    public float TimeWaitSlashes;
    public float SlashSpeed;
    public float offset;
    float counter = 0;
    public override void Act(Controller controller)
    {
       if(counter > TimeWaitSlashes)
        {
            controller.transform.LookAt(controller.player.transform.position);
            GameObject attack = PoolingManager.Instance.GetPooledObject("Slash");
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().speed = SlashSpeed;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position);
            attack.SetActive(true);

            attack = PoolingManager.Instance.GetPooledObject("Slash");
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().speed = SlashSpeed;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position + controller.gameObject.transform.right * offset);
            attack.SetActive(true);

            attack = PoolingManager.Instance.GetPooledObject("Slash");
            attack.transform.position = controller.gameObject.transform.position;
            attack.GetComponent<SlashMovement>().speed = SlashSpeed;
            attack.GetComponent<SlashMovement>().MoveDirection(controller.player.transform.position + (-controller.gameObject.transform.right * offset));
            attack.SetActive(true);
            counter = 0;
        }
       else
        {
            counter += Time.deltaTime;
        }
    }

    public override void RestartVariables()
    {
        counter = 0;
    }
}

