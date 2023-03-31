using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelector : MonoBehaviour
{
    public Color selectedColor;

    GameObject SelectedSlot;

    public void ChangeColor(GameObject Slot)
    {
        if (Slot.GetComponent<Image>().color != selectedColor )
        {
            if (SelectedSlot != null)
                SelectedSlot.GetComponent<Image>().color = Color.white;

            Slot.GetComponent<Image>().color = selectedColor;
            InventoryManager.instance.itemSelected = true;
            SelectedSlot = Slot;
            //if (Slot.name != "Slot")
            //{
            //    CraftingManager.instance.DragItem(Slot.GetComponent<Slot>());
            //    InventoryManager.instance.itemSelectedString = Slot.GetComponent<Slot>().item.itemName;
            //}
            
        }
        else
        {
            InventoryManager.instance.itemSelected = false;
            InventoryManager.instance.itemSelectedString = null;
            Slot.GetComponent<Image>().color = Color.white;
            CraftingManager.instance.ClearCurrentItem();
        }
    }
}
