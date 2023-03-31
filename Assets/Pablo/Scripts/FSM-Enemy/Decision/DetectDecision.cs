using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[CreateAssetMenu(menuName = "FSM/Carnation/Decision/DetectDecision")]// No puede ir en un abstract class

public class DetectDecision : Decision
{
    public bool check;
    public override bool Decide(Controller controller)
    {
        return controller.detect == check ? true : false;
        Debug.Log(check);
    }
    public override void RestartVariables()
    {

    }
}
