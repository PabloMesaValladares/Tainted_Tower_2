using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]GameObject[] Slots;
    [SerializeField] Sprite[] itemImage;
    [SerializeField] string[] itemName;
    [SerializeField] string[] desc;
    [SerializeField] Dictionary<string, Item> items;
    Dictionary<string, Sprite> itemsImageSearch;

    private static InventoryManager _instance;
    public static InventoryManager instance => _instance;

    public GameObject itemNotice;
    public TextMeshProUGUI noticeText;
    public TextMeshProUGUI itemText;
    public bool itemSelected;
    public string itemSelectedString;
    bool itemAdded;

    public PlayerInput playerControls;
    InputAction firstSlot, secondSlot, thirdSlot, fourthSlot;
    InventorySelector selector;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        items = new Dictionary<string, Item>();
        itemsImageSearch = new Dictionary<string, Sprite>();
        for (int i = 0; i < itemName.Length; i++)
        {
            Item item = new Item();
            item.itemName = itemName[i];
            items.Add(itemName[i], item);
            itemsImageSearch.Add(itemName[i], itemImage[i]);
        }
        //itemNotice.SetActive(false);
        itemAdded = false;

        firstSlot = playerControls.actions["First"];
        secondSlot = playerControls.actions["Second"];
        thirdSlot = playerControls.actions["Third"];
        fourthSlot = playerControls.actions["Fourth"];
        selector = GetComponent<InventorySelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstSlot.triggered)
            selector.ChangeColor(Slots[0]);
        if (secondSlot.triggered)
            selector.ChangeColor(Slots[1]);
        if (thirdSlot.triggered)
            selector.ChangeColor(Slots[2]);
        if (fourthSlot.triggered)
            selector.ChangeColor(Slots[3]);
    }

    public void UpdateSlot(string name)
    {
        GameObject slot = null;
        if (!itemAdded)
             slot = getFreeSlot();

        if (slot != null)
        {
           
            //noticeText.text = TextsManager.instance.GetText("Got_Item");
            //itemText.text = TextsManager.instance.GetText(name);
            itemNotice.SetActive(true);
            GameObject parent = slot.GetComponentInParent<Image>().gameObject;
            parent.name = name;
            parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
            slot.GetComponent<Item>().itemName = items[name].itemName;
            slot.GetComponent<Image>().sprite = itemsImageSearch[name];
            slot.SetActive(true); 
            //GameManager.instance.NotificationOn();
        }
    }

    public GameObject getFreeSlot()
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].activeSelf == false)
            {
                Debug.Log(Slots[i].name); 
                itemAdded = true;
                return Slots[i];
            }
        }

        return null;
    }

    public Item GetItemByName(string n)
    {
        return items[n];
    }

    public void ClearSlot(int i)
    {
        Slots[i].GetComponentInParent<Image>().gameObject.name = "Slot";
        Slots[i].SetActive(false);
    }

    public void ClearSlotByName(string n)
    {
        for(int i =0; i < Slots.Length; i++)
        {
            if(Slots[i].GetComponentInParent<Item>().itemName == n)
            {
                ClearSlot(i);
                return;
            }
        }
    }

    public void noticeOff()
    {
        itemNotice.SetActive(false);
        //GameManager.instance.OnScriptStop();
        itemAdded = false;
    }

}
