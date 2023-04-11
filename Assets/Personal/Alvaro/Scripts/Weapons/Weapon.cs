using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public enum Type
    {
        physical,
        magical
    };

    public Elements weaponElement;
    public int damage;

    public Type mainStatType;
    public int mainStat;

    public int crit;
    public int critDmg;
}
