using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "AttackPatterns/BasicAttack")]
public class BasicAttack : AttackSO
{
    public override void Execute()
    {
        Debug.Log("Basic Attack");
    }
}
