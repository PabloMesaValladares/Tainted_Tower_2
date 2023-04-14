using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] Slots;
    [SerializeField]GameObject[] QuickSlots;
    [SerializeField] Sprite[] itemImage;
    [SerializeField] ItemUses[] itemUses;
    [SerializeField] string[] itemName;
    [SerializeField] string[] desc;
    [SerializeField] Dictionary<string, Item> items;
    Dictionary<string, Sprite> itemsImageSearch;
    Dictionary<string, ItemUses> itemsUseSearch;

    private static InventoryManager _instance;
    public static InventoryManager instance => _instance;

    public GameObject itemNotice;
    public TextMeshProUGUI noticeText;
    public TextMeshProUGUI itemText;
    public bool itemSelected;
    public string itemSelectedString;
    bool itemAdded;

    public PlayerInput playerControls;
    InputAction firstSlot, secondSlot, thirdSlot, fourthSlot, Use;
    InventorySelector selector;

    public Item selectedItem;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        Item.itemUsed += ClearSlot;
    }

    void Start()
    {
        items = new Dictionary<string, Item>();
        itemsImageSearch = new Dictionary<string, Sprite>();
        itemsUseSearch = new Dictionary<string, ItemUses>();
        for (int i = 0; i < itemName.Length; i++)
        {
            Item item = new Item();
            item.itemName = itemName[i];
            items.Add(itemName[i], item);
            itemsImageSearch.Add(itemName[i], itemImage[i]);
            itemsUseSearch.Add(itemName[i], itemUses[i]);
        }

        for(int i = 0; i< QuickSlots.Length; i ++)
        {
            QuickSlots[i].GetComponent<Item>().itemName = null;
            QuickSlots[i].GetComponent<Item>().Num = 0;
            //QuickSlots[i].GetComponentInChildren<InventoryNum>().UpdateText();
            QuickSlots[i].SetActive(false);

        }
        for (int i = 0; i < Slots.Length; i++)
        {
            if(Slots[i].GetComponent<Item>().itemName != null)
            {
                Slots[i].GetComponentInParent<Slot>().index = i;
                Slots[i].GetComponent<Image>().sprite = itemsImageSearch[Slots[i].GetComponent<Item>().itemName];
                Slots[i].GetComponent<Item>().ind = i;
                //Slots[i].GetComponentInChildren<InventoryNum>().UpdateText();
            }
            else
            {
                Slots[i].SetActive(false);
            }

        }

        //itemNotice.SetActive(false);
        itemAdded = false;

        firstSlot = playerControls.actions["First"];
        secondSlot = playerControls.actions["Second"];
        thirdSlot = playerControls.actions["Third"];
        fourthSlot = playerControls.actions["Fourth"];
        Use = playerControls.actions["Interact"];
        selector = GetComponent<InventorySelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstSlot.triggered)
        {
            if (!itemSelected)
            {
                selectedItem = QuickSlots[0].GetComponent<Slot>().item;
                itemSelected = true;
            }
            else
            {
                itemSelected = false;
                selectedItem = null;
            }
            selector.ChangeColor(QuickSlots[0]);
        }
        if (secondSlot.triggered)
        {

            if (!itemSelected)
            {
                selectedItem = QuickSlots[1].GetComponent<Slot>().item;
                itemSelected = true;
            }
            else
            {
                itemSelected = false;
                selectedItem = null;
            }
            selector.ChangeColor(QuickSlots[1]);
        }
        if (thirdSlot.triggered)
        {
            if (!itemSelected)
            {
                selectedItem = QuickSlots[2].GetComponent<Slot>().item;
                itemSelected = true;
            }
            else
            {
                itemSelected = false;
                selectedItem = null;
            }
            selector.ChangeColor(QuickSlots[2]);
        }
        if (fourthSlot.triggered)
        {
            if (!itemSelected)
            {
                selectedItem = QuickSlots[3].GetComponent<Slot>().item;
                itemSelected = true;
            }
            else
            {
                itemSelected = false;
                selectedItem = null;
            }
            selector.ChangeColor(QuickSlots[3]);
        }
        if (Use.triggered && itemSelected)
        {
            selectedItem.Use(playerControls.gameObject);
        }
    }

    public void UpdateSlot(Item itemGot)
    {
        GameObject slot = null;
        if (!itemAdded)
             slot = getFreeSlot();

        if (slot != null)
        {
            //noticeText.text = TextsManager.instance.GetText("Got_Item");
            //itemText.text = TextsManager.instance.GetText(name);
            //itemNotice.SetActive(true);
            GameObject parent = slot.GetComponentInParent<Slot>().gameObject;
            InventoryNum child = slot.GetComponentInChildren<InventoryNum>();
            slot.GetComponent<Item>().itemName = itemGot.itemName;
            slot.GetComponent<Item>().SetUse(itemsUseSearch[itemGot.name]);
            slot.GetComponent<Item>().Num = itemGot.Num;
            slot.GetComponent<Image>().sprite = itemsImageSearch[itemGot.itemName];
            parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
            child.item = slot.GetComponent<Item>();
            //child.UpdateText();
            parent.name = itemGot.itemName;
            slot.SetActive(true); 
            //GameManager.instance.NotificationOn();
        }
    }

    public void UpdateQuickSlot(Item itemGot, int id)
    {

        GameObject slot = QuickSlots[id];

        GameObject parent = slot.GetComponentInParent<Slot>().gameObject;
        InventoryNum child = slot.GetComponentInChildren<InventoryNum>();
        slot.GetComponent<Item>().itemName = itemGot.itemName;
        slot.GetComponent<Item>().Num = itemGot.Num;
        slot.GetComponent<Image>().sprite = itemsImageSearch[itemGot.itemName];
        parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
        child.item = slot.GetComponent<Item>();
        //child.UpdateText();
        parent.name = itemGot.itemName;
        slot.SetActive(true);
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
        //Slots[i].GetComponentInParent<Image>().gameObject.name = "Slot";
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
