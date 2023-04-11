using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
[CreateAssetMenu(menuName = "FSM/Carnation/Decision/TimerDecision")]// No puede ir en un abstract class

public class TimerDecision : Decision
{
    public float TimerMaxTime;
    float counter;
    public override bool Decide(Controller controller)
    {
        if (counter > TimerMaxTime)
            return true;
        else
        {
            counter += Time.deltaTime;
            return false;
        }
    }

    public override void RestartVariables()
    {
        counter = 0;
    }
}

