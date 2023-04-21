using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InventoryManager>().setInventoryItems(GameManager.instance.inventoryItems);
    }

}
