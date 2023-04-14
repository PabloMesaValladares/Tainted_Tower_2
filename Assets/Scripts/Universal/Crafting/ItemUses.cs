using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUses : ScriptableObject
{
    public int quantity;
    public abstract void Use(GameObject p);

}
