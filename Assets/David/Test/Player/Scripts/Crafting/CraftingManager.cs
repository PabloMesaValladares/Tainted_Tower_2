using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private static CraftingManager _instance;
    public static CraftingManager instance => _instance;
    [SerializeField] private Item currentItem;
    private Slot ItemPicked;
    private Item secondItem;
    public Image customCursor;

    public List<Slot> craftingSlots;

    public string[] recipes;
    public string[] recipeResults;

    public UnityEvent<GameObject> changeColor;
    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (currentItem != null)
        //    {
        //        Slot nearestSlot = null;
        //        float shortestDistance = float.MaxValue;

        //        foreach (Slot slot in craftingSlots)
        //        {
        //            if (slot.gameObject.name != "Slot" && slot.GetComponent<Slot>().index != ItemPicked.index)
        //            {
        //                float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

        //                if (dist < shortestDistance)
        //                {
        //                    shortestDistance = dist;
        //                    nearestSlot = slot;
        //                }
        //            }
        //        }
        //        if (nearestSlot != null)
        //        {
        //            secondItem = nearestSlot.item;
        //            if (secondItem != null)
        //                CheckForCreatedRecipes(nearestSlot);
        //        }
        //        customCursor.gameObject.SetActive(false);
        //        currentItem = null;
        //        changeColor.Invoke(ItemPicked.gameObject);

        //    }
        //}
    }
    void CheckForCreatedRecipes(Slot secondSlot)
    {
        string currentRecipeString = "";

        currentRecipeString += currentItem.itemName + secondItem.itemName;

        Debug.Log("La receta es " + currentRecipeString);

        for(int i= 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                InventoryManager.instance.ClearSlot(ItemPicked.index);
                changeColor.Invoke(ItemPicked.gameObject);
                InventoryManager.instance.ClearSlot(secondSlot.index);
                changeColor.Invoke(secondSlot.gameObject);
                Debug.Log(recipeResults[i]);
                InventoryManager.instance.UpdateSlot(recipeResults[i]);
                break;
            }
        }
    }
    public void ClearCurrentItem()
    {
        currentItem = null;
        customCursor.gameObject.SetActive(false);
    }
    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        slot.gameObject.SetActive(false);
    }

    public void DragItem(Slot slot)
    {
        if (currentItem == null)
        {
            ItemPicked = slot;
            currentItem = slot.item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
            ItemPicked = slot;
        }
    }
}
