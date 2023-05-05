using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/Action/LavaRayAttack")]


public class LavaRayAttack : Action
{
    public float lavaOffset;
    float count;
    

    public override void Act(Controller controller)
    {
        if (count > lavaOffset)
        {
            GameObject lavaRay = PoolingManager.Instance.GetPooledObject("LavaRay");

            lavaRay.GetComponent<ParticleSystem>().Play();
           
            lavaRay.SetActive(true);
        }

        else
        {
            count += Time.deltaTime;
        }



    }

    public override void RestartVariables()
    {
        count = 0;
    }

}
