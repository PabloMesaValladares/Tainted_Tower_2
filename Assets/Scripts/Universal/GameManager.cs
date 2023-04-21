using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance => _instance; //Singleton, para cuando quiero que algo sea estatico pero sus propiedades no

    [Header("Player scripts")]
    [HideInInspector]public GameObject player;
    //[HideInInspector]public HealthBehaviour playerHealth;
    //[HideInInspector]public StatController playerStats;
    //[HideInInspector]public StaminaController playerStamina;
    //[HideInInspector]public RespawnPoint playerRespawn;

    [Header("Player")]
    public int currentHP;
    public int totalMana;
    public int strength;
    public int inteligence;
    public int defense;
    public int staminaStat;
    public float stamina;
    public Vector3 RespawnPosition;

    [Serializable]
    public struct ItemSave
    {
        public string itemName;
        public int Num;
        public int ind;
    }


    [Header("Menu")]
    GameObject inventory;
    public List<ItemSave> inventoryItems;

    [Header("Skills")]
    public bool grapple, pilar, drugs, fireball;
    public bool firstSet;
    //Meter scripts skills para guardar cooldown

    private void Awake()
    {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryItems = new List<ItemSave>();
        if (instance == null)
        {
            _instance = this;

            SetScripts(); 
            currentHP = player.GetComponent<HealthBehaviour>().maxHP;
            stamina = staminaStat;
            player.GetComponent<StaminaController>().SetStamina(staminaStat);
            firstSet = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        //    player = GameObject.FindGameObjectWithTag("Player");
        //    player.GetComponent<HealthBehaviour>().currentHP = currentHP; //Seteamos la vida actual
        //    player.GetComponent<LifeManager>().lifeSlider.value = currentHP;
        //    player.GetComponent<StatController>().totalMana = totalMana;
        //    player.GetComponent<StatController>().strength = strength;
        //    player.GetComponent<StatController>().inteligence = inteligence;
        //    player.GetComponent<StatController>().stamina = staminaStat;
        //    player.GetComponent<StatController>().defense = defense;
        //    player.GetComponent<StaminaController>().SetStamina(stamina);
        //    player.GetComponent<RespawnPoint>().RespawnPosition = RespawnPosition;
        //    player.GetComponent<RespawnPoint>().Respawn();
        //    player.GetComponent<Grappling>().enabled = grapple;
        //    inventory = GameObject.FindGameObjectWithTag("Inventory");
        //    inventory.GetComponent<InventoryManager>().setInventoryItems(inventoryItems);


       
    }

    public void SetScripts()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = player.GetComponent<HealthBehaviour>().currentHP; //Seteamos la vida actual
        totalMana = player.GetComponent<StatController>().totalMana;
        strength = player.GetComponent<StatController>().strength;
        inteligence = player.GetComponent<StatController>().inteligence;
        staminaStat = player.GetComponent<StatController>().stamina;
        defense = player.GetComponent<StatController>().defense;
        stamina = player.GetComponent<StaminaController>().ReturnStamina();
        RespawnPosition = player.GetComponent<RespawnPoint>().RespawnPosition; 
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        for (int i = 0; i < inventory.GetComponent<InventoryManager>().getSlotLenght(); i++)
        {
            ItemSave item = new ItemSave();
            //item.name = i.ToString();
            item.itemName = inventory.GetComponent<InventoryManager>().getInventoryItems(i).itemName;
            item.Num = inventory.GetComponent<InventoryManager>().getInventoryItems(i).Num;
            item.ind = inventory.GetComponent<InventoryManager>().getInventoryItems(i).ind;
            //item.SetUse(inventory.GetComponent<InventoryManager>().getItemUse(item.itemName));
            inventoryItems.Add(item);

        }
    }
}
