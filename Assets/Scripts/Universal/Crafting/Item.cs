using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public string itemName;
    public int Num;
    public int ind;

    ItemUses use;


    public delegate void UseIt(int id);
    public static UseIt itemUsed;

    public void SetUse(ItemUses u)
    {
        use = u;
    }

    public void Use(GameObject p)
    {
        use.Use(p);
        Num--;
        if (Num < 0)
        {
            GetComponentInParent<Slot>().gameObject.SetActive(false);
            itemUsed(ind);
        }
    }
}
