using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] Slots;
    [SerializeField] Item[] SlotsItems;
    [SerializeField] GameObject[] QuickSlots;
    [SerializeField] Sprite[] itemImage;
    [SerializeField] ItemUses[] itemUses;
    [SerializeField] string[] itemName;
    [SerializeField] string[] desc;
    [SerializeField] Dictionary<string, Item> items;
    public Dictionary<string, Sprite> itemsImageSearch;
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

    public GameObject selectedItem;

    public GameObject AddQuickInventory;

    private void Awake()
    {
        _instance = this;
    }
    private void OnLevelWasLoaded(int level)
    {
        if(GameManager.instance != null)
        {
            for(int i = 0; i < GameManager.instance.inventoryItems.Count; i++)
            {
                UpdateSlot(GameManager.instance.inventoryItems[i], i);
            }
        }
    }


    void Start()
    {
        itemSelected = false;
        SlotsItems = new Item[Slots.Length];
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

        items.Add("Null", null);
        itemsImageSearch.Add("Null", null);
        itemsUseSearch.Add("Null", null);

        for (int i = 0; i< QuickSlots.Length; i ++)
        {
            QuickSlots[i].GetComponent<Item>().itemName = null;
            QuickSlots[i].GetComponent<Item>().Num = 0;
            //QuickSlots[i].GetComponentInChildren<InventoryNum>().UpdateText();
            QuickSlots[i].SetActive(false);

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
        if (!Menu.activeInHierarchy)
        {
            if (firstSlot.triggered && QuickSlots[0].activeInHierarchy)
            {
                if (!itemSelected)
                {
                    Debug.Log("Seleccionado");
                    selectedItem = QuickSlots[0];
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
                    selectedItem = QuickSlots[1];
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
                    selectedItem = QuickSlots[2];
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
                    selectedItem = QuickSlots[3];
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
                selectedItem.GetComponent<Item>().Use();
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

        slot = getSlotWithName(itemGot.itemName);

        if (slot == null)
        {
            Debug.Log("No encontre para stackear");
            slot = getFreeSlot();
            if (slot != null)
            {
                //noticeText.text = TextsManager.instance.GetText("Got_Item");
                //itemText.text = TextsManager.instance.GetText(name);
                //itemNotice.SetActive(true);
                GameObject parent = slot.transform.parent.GetComponent<Slot>().gameObject;
                InventoryNum child = slot.GetComponentInChildren<InventoryNum>();
                slot.GetComponent<Item>().itemName = itemGot.itemName;
                slot.GetComponent<Item>().SetUse(itemsUseSearch[itemGot.itemName]);
                slot.GetComponent<Item>().Num = itemGot.Num;
                slot.GetComponent<Image>().sprite = itemsImageSearch[itemGot.itemName];
                slot.GetComponent<Image>().color = Color.white;
                parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
                child.item = slot.GetComponent<Item>();
                //child.UpdateText();
                parent.name = itemGot.itemName;
                slot.SetActive(true);
                //GameManager.instance.NotificationOn();
            }
        }
        else
        {
            Debug.Log("Encontre para stackear");
            if (slot.GetComponent<Item>().Num + itemGot.Num <= 99)
            {
                slot.GetComponent<Item>().Num += itemGot.Num;
                foreach(GameObject quickslot in QuickSlots)
                {
                    if(quickslot.GetComponent<Item>().ind == slot.GetComponent<Item>().ind)
                    {
                        quickslot.GetComponent<Item>().Num = slot.GetComponent<Item>().Num;
                        break;
                    }
                }
            }
            else
            {
                int overflow = slot.GetComponent<Item>().Num += itemGot.Num - 99;
                slot.GetComponent<Item>().Num = 99;
                

                slot = getFreeSlot();
                if (slot != null)
                {
                    //noticeText.text = TextsManager.instance.GetText("Got_Item");
                    //itemText.text = TextsManager.instance.GetText(name);
                    //itemNotice.SetActive(true);
                    GameObject parent = slot.transform.parent.GetComponent<Slot>().gameObject;
                    InventoryNum child = slot.GetComponentInChildren<InventoryNum>();
                    slot.GetComponent<Item>().itemName = itemGot.itemName;
                    slot.GetComponent<Item>().SetUse(itemsUseSearch[itemGot.itemName]);
                    slot.GetComponent<Item>().Num = overflow;
                    slot.GetComponent<Image>().sprite = itemsImageSearch[itemGot.itemName];
                    slot.GetComponent<Image>().color = Color.white;
                    parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
                    child.item = slot.GetComponent<Item>();
                    //child.UpdateText();
                    parent.name = itemGot.itemName;
                    slot.SetActive(true);
                    //GameManager.instance.NotificationOn();
                }
            }
         
        }

        Debug.Log(slot);
    }
    
    
    public void UpdateSlot(Item itemGot, int ind)
    {
        GameObject slot = Slots[ind];


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
    public void UpdateSlot(GameManager.ItemSave itemGot, int ind)
    {
        GameObject slot = Slots[ind];

        slot.GetComponent<Item>().ind = ind;

        if (itemGot.itemName != "Null")
        {
            //noticeText.text = TextsManager.instance.GetText("Got_Item");
            //itemText.text = TextsManager.instance.GetText(name);
            //itemNotice.SetActive(true);
            GameObject parent = slot.transform.parent.gameObject;
            InventoryNum child = slot.GetComponentInChildren<InventoryNum>();
            slot.GetComponent<Item>().itemName = itemGot.itemName;
            //slot.GetComponent<Item>().SetUse(itemsUseSearch[itemGot.itemName]);
            slot.GetComponent<Item>().Num = itemGot.Num;
            slot.GetComponent<Image>().sprite = itemsImageSearch[itemGot.itemName];
            parent.GetComponent<Slot>().item = slot.GetComponent<Item>();
            child.item = slot.GetComponent<Item>();
            //child.UpdateText();
            parent.name = itemGot.itemName;
            slot.SetActive(true);
            //GameManager.instance.NotificationOn();
        }
        else
        {
            slot.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //Slots[i].GetComponent<Item>().SetUse(sendItemUse(Slots[i].GetComponent<Item>().itemName));
            SlotsItems[ind] = slot.GetComponent<Item>();
            //Slots[i].GetComponentInChildren<InventoryNum>().UpdateText();
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
            if (Slots[i].GetComponent<Item>().itemName == "Null")
            {
                Debug.Log(Slots[i].name); 
                itemAdded = true;
                return Slots[i];
            }
        }

        return null;
    }
    public GameObject getSlotWithName(string name)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<Item>().itemName == name && Slots[i].GetComponent<Item>().Num < 99)
            {
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
        Slots[i].GetComponent<Item>().itemName = null;
        Slots[i].GetComponent<Item>().Num = 0;

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

    public Item getInventoryItems(int ind)
    {
        return Slots[ind].GetComponent<Item>();
    }

    public void setInventoryItems(List<GameManager.ItemSave> items)
    {
        for(int i = 0; i < SlotsItems.Length; i++)
        {
            UpdateSlot(items[i], i);
        }
    }

    public ItemUses getItemUse(string name)
    {
        return itemsUseSearch[name];
    }

    public int getSlotLenght()
    {
        return Slots.Length;
    }

    public void SelectSlot(GameObject slot)
    {
        if(slot.GetComponentInChildren<Item>().itemName != "Null")
        {
            GetComponent<InventorySelector>().ChangeColor(slot);
            AddQuickInventory.GetComponent<QuickAdd>().SetSpawnPoint(slot);
        }
    }
}
