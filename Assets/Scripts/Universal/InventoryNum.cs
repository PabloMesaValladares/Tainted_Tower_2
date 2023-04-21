using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryNum : MonoBehaviour
{
    public Item item;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (item.Num > 0)
            text.text = "X " + item.Num.ToString();
        else
            text.text = "";
    }    
}
