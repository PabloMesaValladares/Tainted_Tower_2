using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargedAttack", menuName = "AttackPatterns/ChargedAttack")]
public class ChargedAttack : AttackSO
{
    public int type;

    public override void Execute()
    {
        Debug.Log(type);
    }

}
