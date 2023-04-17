using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public string itemName;
    public int Num;
    public int ind;

    ItemUses use;

    GameObject player;

    public delegate void UseIt(int id);
    public static UseIt itemUsed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void SetUse(ItemUses u)
    {
        use = u;
    }

    public void Use()
    {
        use = InventoryManager.instance.sendItemUse(itemName);
        //Debug.Log(use);
        use.Use(player);
        Num--;
        if (Num <= 0)
        {
            itemUsed(ind);
            gameObject.SetActive(false);
        }
    }
}
