using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSO : ScriptableObject
{
    public float delay;
    public abstract void Execute();
}
