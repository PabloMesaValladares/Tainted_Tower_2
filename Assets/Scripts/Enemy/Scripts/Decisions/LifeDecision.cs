using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Decision/LifeDecision")]// No puede ir en un abstract class

public class LifeDecision : Decision
{
    public float LifePercentage;
    
    public override bool Decide(Controller controller)
    {
        float perc = controller.GetComponent<HealthBehaviour>().currentHP * 100 / controller.GetComponent<HealthBehaviour>().maxHP;
        Debug.Log("Porcentaje de vida es " + perc);
        
        if (perc < LifePercentage)
            return true;
        else
        {
            return false;
        }
    }

    public override void RestartVariables()
    {
        
    }
}
