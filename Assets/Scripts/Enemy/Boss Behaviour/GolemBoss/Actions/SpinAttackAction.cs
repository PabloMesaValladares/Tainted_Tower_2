using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/SpinAttackAction")]

public class SpinAttackAction : Action
{
    bool spin = false;
    Vector3 pos;
    Vector3 dif;
    Animator an;
    public float speed = 0;
    Rigidbody rb;
    public override void Act(Controller controller)
    {
        if (!spin)
        {
            controller.GetComponentInChildren<Animator>().SetTrigger("StartSpinningAttack");
            an = controller.GetComponentInChildren<Animator>();
            pos = new Vector3(controller.player.transform.position.x, controller.transform.position.y, controller.player.transform.position.z);
            dif = pos - controller.transform.position;
            spin = true;
            rb = controller.GetComponent<Rigidbody>();
        }

        controller.GetComponent<Rigidbody>().AddForce(dif * speed, ForceMode.Force);


    }

    public override void RestartVariables()
    {
        spin = false;
        rb.velocity = Vector3.zero;
        an.GetComponent<Animator>().ResetTrigger("StartSpinningAttack");
        an.GetComponent<Animator>().SetTrigger("StopSpinningAttack");
    }

}
