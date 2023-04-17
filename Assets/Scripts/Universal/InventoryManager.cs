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

    public GameObject Menu;
    public TextMeshProUGUI noticeText;
    public TextMeshProUGUI itemText;
    public bool itemSelected;
    public string itemSelectedString;
    bool itemAdded;

    public PlayerInput playerControls;
    InputAction firstSlot, secondSlot, thirdSlot, fourthSlot, Use, pause;
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
    private void OnDisable()
    {
        Item.itemUsed -= ClearSlot;
    }


    void Start()
    {
        itemSelected = false;
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
            Slots[i].GetComponent<Item>().ind = i;
            if (Slots[i].GetComponent<Item>().itemName != null)
            {
                Slots[i].GetComponent<Image>().sprite = itemsImageSearch[Slots[i].GetComponent<Item>().itemName];

                Slots[i].GetComponent<Item>().SetUse(sendItemUse(Slots[i].GetComponent<Item>().itemName));
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
        Use = playerControls.actions["Interact"];
        selector = GetComponent<InventorySelector>();
    }

    // Update is called once per frame
    void Update()
    {
        bool controllers = Gamepad.all.Count <= 0;
        if (controllers && !Menu.activeInHierarchy)
        {
            if (firstSlot.triggered && QuickSlots[0].activeInHierarchy)
            {
                if (!itemSelected)
                {
                    selectedItem = QuickSlots[0].GetComponent<Item>();
                    itemSelected = true;
                }
                else
                {
                    itemSelected = false;
                    selectedItem = null;
                }
                selector.ChangeColor(QuickSlots[0]);
            }
            if (secondSlot.triggered && QuickSlots[1].activeInHierarchy)
            {

                if (!itemSelected)
                {
                    selectedItem = QuickSlots[1].GetComponent<Item>();
                    itemSelected = true;
                }
                else
                {
                    itemSelected = false;
                    selectedItem = null;
                }
                selector.ChangeColor(QuickSlots[1]);
            }
            if (thirdSlot.triggered && QuickSlots[2].activeInHierarchy)
            {
                if (!itemSelected)
                {
                    selectedItem = QuickSlots[2].GetComponent<Item>();
                    itemSelected = true;
                }
                else
                {
                    itemSelected = false;
                    selectedItem = null;
                }
                selector.ChangeColor(QuickSlots[2]);
            }
            if (fourthSlot.triggered && QuickSlots[3].activeInHierarchy)
            {
                if (!itemSelected)
                {
                    selectedItem = QuickSlots[3].GetComponent<Item>();
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
                selectedItem.Use();
            }
        }

    }

    public ItemUses sendItemUse(string name)
    {
        return itemsUseSearch[name];
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
        slot.GetComponent<Item>().ind = itemGot.ind;
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

        for(int j = 0; j < QuickSlots.Length; j++)
        {
            if (QuickSlots[j].GetComponent<Item>().ind == i)
                QuickSlots[j].SetActive(false);
        }
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


}
