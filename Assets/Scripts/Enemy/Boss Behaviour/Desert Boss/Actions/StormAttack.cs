using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/DBStormAttack")]
public class StormAttack : Action
{
    public float stormOffset;
    float counter;
    Animator an;
    GameObject ske;
    bool start = false;
    public override void Act(Controller controller)
    {
        if(!start)
        {
            an = controller.GetComponentInChildren<Animator>();
            an.SetTrigger("StartStorm");
            ske = controller.gameObject;
            ske.GetComponent<BoxCollider>().enabled = false;
            ske.GetComponent<Rigidbody>().useGravity = false;
            start = true;
        }
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
        start = false;
        counter = 0;
        if (ske != null)
        {
            ske.GetComponent<BoxCollider>().enabled = false;
            ske.GetComponent<Rigidbody>().useGravity = false;
        }
        if (an != null)
            an.SetTrigger("StopStorm");
    }
}
