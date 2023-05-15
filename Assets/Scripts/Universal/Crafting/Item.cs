using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public int Num;
    public int ind;

    ItemUses use;

    GameObject player;

    public delegate void UseIt(int id);
    public static UseIt itemUsed;

    public delegate void Subst(int id);
    public static Subst SubstractUse;

    private void OnEnable()
    {
        SubstractUse += SubsUse;
    }
    private void OnDisable()
    {
        SubstractUse -= SubsUse;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (itemName == "Null")
            GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }


    public void SetUse(ItemUses u)
    {
        use = u;
    }

    void SubsUse(int i)
    {
        if(ind == i)
        {
            Num--;
        }
    }

    public void Use()
    {
        use = InventoryManager.instance.sendItemUse(itemName);
        
        //Debug.Log(use);
        use.Use(player);
        SubstractUse(ind);
        if (Num <= 0)
        {
            itemName = null;
            Num = 0;
            ind = 0;
            InventoryManager.instance.ClearSlot(ind);
            gameObject.SetActive(false);
        }
    }
}
