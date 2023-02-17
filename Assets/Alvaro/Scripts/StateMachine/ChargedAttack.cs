using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargedAttack", menuName = "AttackPatterns/ChargedAttack")]
public class ChargedAttack : AttackSO
{
    public string pName;
    public int type;   
    GameObject _go;

    public override void Execute()
    {
        _go = PoolingManager.Instance.GetPooledObject(pName);
        _go.SetActive(true);
    }
}
