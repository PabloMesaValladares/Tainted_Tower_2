using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/LavaRayAttack")]


public class LavaRayAttack : Action
{
    LineRenderer lr;
    GameObject lava;
    public float lavaOffset;
    float count;

    bool started = false;

    public override void Act(Controller controller)
    {
        if(!started)
        {
            lr = controller.GetComponent<LineRenderer>();
            lr.enabled = true;
            lr.SetPosition(0, new Vector3(controller.transform.position.x, controller.transform.position.y + lavaOffset, controller.transform.position.z)); 
            GameObject lavaRay = PoolingManager.Instance.GetPooledObject("LavaRay");

            lavaRay.SetActive(true);
            lavaRay.GetComponent<ParticleSystem>().Play();
            lavaRay.GetComponent<LavaPosition>().Move(controller.player);
            lava = lavaRay;
            started = true;
        }


        controller.gameObject.transform.LookAt(new Vector3(controller.player.transform.position.x, controller.gameObject.transform.position.y, controller.player.transform.position.z));
        controller.GetComponent<LineRenderer>().SetPosition(1, controller.player.transform.position);
    }

    public override void RestartVariables()
    {
        count = 0;
        started = false;
        lr.enabled = false;
        lava.SetActive(false);

    }

}
