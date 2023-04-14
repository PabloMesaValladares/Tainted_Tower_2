using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAdd : MonoBehaviour
{
    public InventoryManager inventory;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetObject(int id)
    {
        inventory.UpdateQuickSlot(item, id);
    }

    public void SetSpawnPoint(GameObject pos)
    {
        if(pos.GetComponentInChildren<Slot>().item != null)
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<RectTransform>().position = pos.GetComponent<RectTransform>().position;
            item = pos.GetComponentInChildren<Slot>().item;
        }
    }

}
