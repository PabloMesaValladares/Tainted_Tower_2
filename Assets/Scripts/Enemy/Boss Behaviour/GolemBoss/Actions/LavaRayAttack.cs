using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/LavaRayAttack")]


public class LavaRayAttack : Action
{
    Rigidbody rb;
    LineRenderer lr;
    GameObject lava;
    public float lavaOffset;
    public int secToStop;
    float count;
    Animator an;
    Vector3 lavaStartPos;

    RaycastHit hit;

    bool started = false;

    public override void Act(Controller controller)
    {
        if(!started)
        {
            controller.GetComponentInChildren<Animator>().SetTrigger("StartLaser");
            rb = controller.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            an = controller.GetComponentInChildren<Animator>();
            lr = controller.GetComponent<LineRenderer>();
            lr.enabled = true;
            lavaStartPos = new Vector3(controller.transform.position.x, controller.transform.position.y + lavaOffset, controller.transform.position.z);
            lr.SetPosition(0, lavaStartPos); 
            GameObject lavaRay = PoolingManager.Instance.GetPooledObject("LavaRay");

            lavaRay.SetActive(true);
            lavaRay.GetComponent<ParticleSystem>().Play();
            //lavaRay.GetComponent<LavaPosition>().followDistance = secToStop;
            lavaRay.GetComponent<LavaPosition>().Move(controller.player);
            lava = lavaRay;
            started = true;
        }

        controller.gameObject.transform.LookAt(new Vector3(controller.player.transform.position.x, controller.gameObject.transform.position.y, controller.player.transform.position.z));
        if (lava != null)
            controller.GetComponent<LineRenderer>().SetPosition(1, lava.transform.position);
    }

    public override void RestartVariables()
    {
        count = 0;
        an.GetComponent<Animator>().SetTrigger("StopLaser");
        started = false;
        if (lr != null)
            lr.enabled = false;
        if (lava != null)
            lava.SetActive(false);
        if (rb != null)
            rb.isKinematic = false;

    }

}
