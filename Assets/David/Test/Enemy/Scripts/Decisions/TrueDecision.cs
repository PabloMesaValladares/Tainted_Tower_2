using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[CreateAssetMenu(menuName = "FSM/Carnation/Decision/TrueDecision")]// No puede ir en un abstract class

public class TrueDecision : Decision
{
    public override bool Decide(Controller controller)
    {
        return true;
    }
    public override void RestartVariables()
    {
       
    }
}