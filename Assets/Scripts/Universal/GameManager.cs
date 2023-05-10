using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance => _instance; //Singleton, para cuando quiero que algo sea estatico pero sus propiedades no

    [Serializable]
    public struct SpawnPoints
    {
        public string SceneName;
        public Vector3 position;
    }

    [Header("Player scripts")]
    [HideInInspector]public GameObject player;
    //[HideInInspector]public HealthBehaviour playerHealth;
    //[HideInInspector]public StatController playerStats;
    //[HideInInspector]public StaminaController playerStamina;
    //[HideInInspector]public RespawnPoint playerRespawn;

    [Header("Player")]
    public int maxHP;
    public int currentHP;
    public int totalMana;
    public int strength;
    public int inteligence;
    public int defense;
    public int staminaStat;
    public float stamina;
    public Vector3 Spawns;
    public string currentBGM;

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
    public bool reset = false;
    //Meter scripts skills para guardar cooldown

    [Header("CheckPoint")]
    public bool Checkpoint;
    public int CheckPointHP;
    public int CheckPointMana;
    public int CheckPointStrength;
    public int CheckPointInteligence;
    public int CheckPointDefense;
    public int CheckPointStaminaStat;
    public float CheckPointStamina;
    public Vector3 CheckPointSpawns;
    public List<ItemSave> CheckPointInventoryItems;
    public bool Cgrapple, Cpilar, Cdrugs, Cfireball;
    public string CcurrentBGM;

    private void Awake()
    {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        if (instance == null)
        {
            _instance = this;

            SetScripts();
            maxHP = player.GetComponent<StatController>().health;
            currentHP = player.GetComponent<StatController>().health;
            stamina = staminaStat;
            player.GetComponent<StaminaController>().SetStamina(staminaStat);

            if (SceneManager.GetActiveScene().name == "Game")
                player.GetComponent<RespawnPoint>().RespawnPosition = Spawns;

            CheckPoint();

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Game" && reset)
        {
            SetScripts();
            maxHP = player.GetComponent<StatController>().health;
            currentHP = player.GetComponent<StatController>().health;
            stamina = staminaStat;
            player.GetComponent<StaminaController>().SetStamina(staminaStat);

            if (SceneManager.GetActiveScene().name == "Game")
                player.GetComponent<RespawnPoint>().RespawnPosition = Spawns;

            CheckPoint();
            reset = false;
        }
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
        inventoryItems = new List<ItemSave>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = player.GetComponent<HealthBehaviour>().currentHP; //Seteamos la vida actual
        totalMana = player.GetComponent<StatController>().totalMana;
        strength = player.GetComponent<StatController>().strength;
        inteligence = player.GetComponent<StatController>().inteligence;
        staminaStat = player.GetComponent<StatController>().stamina;
        defense = player.GetComponent<StatController>().defense;
        stamina = player.GetComponent<StaminaController>().ReturnStamina();

        if (SceneManager.GetActiveScene().name == "Game" && !reset)
            Spawns = player.GetComponent<RespawnPoint>().RespawnPosition;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        //currentBGM = SoundManager.instance.currentBGM;

        for (int i = 0; i < inventory.GetComponent<InventoryManager>().getSlotLenght(); i++)
        {
            ItemSave item = new ItemSave();
            //item.name = i.ToString();
            item.itemName = inventory.GetComponent<InventoryManager>().getInventoryItems(i).itemName;
            if (inventory.GetComponent<InventoryManager>().getInventoryItems(i).itemName == null)
                item.itemName = "Null";
            item.Num = inventory.GetComponent<InventoryManager>().getInventoryItems(i).Num;
            item.ind = i;
            //item.SetUse(inventory.GetComponent<InventoryManager>().getItemUse(item.itemName));
            inventoryItems.Add(item);

        }

        //grapple = player.GetComponent<Grappling>().enabled;
        //drugs = player.GetComponent<DrugsMode>().enabled;
        //fireball = player.GetComponent<PlayerMagicSystem>().enabled;
    }

    public void CheckPoint()
    {
        CheckPointInventoryItems = new List<ItemSave>();
        player = GameObject.FindGameObjectWithTag("Player");
        CheckPointHP = player.GetComponent<HealthBehaviour>().currentHP; //Seteamos la vida actual
        CheckPointMana = player.GetComponent<StatController>().totalMana;
        CheckPointStrength = player.GetComponent<StatController>().strength;
        CheckPointInteligence = player.GetComponent<StatController>().inteligence;
        CheckPointStaminaStat = player.GetComponent<StatController>().stamina;
        CheckPointDefense = player.GetComponent<StatController>().defense;
        CheckPointStamina = player.GetComponent<StaminaController>().ReturnStamina();

        if (SceneManager.GetActiveScene().name == "Game" && !reset)
            CheckPointSpawns = player.GetComponent<RespawnPoint>().RespawnPosition;

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        //CcurrentBGM = SoundManager.instance.currentBGM;

        for (int i = 0; i < inventory.GetComponent<InventoryManager>().getSlotLenght(); i++)
        {
            ItemSave item = new ItemSave();
            //item.name = i.ToString();
            item.itemName = inventory.GetComponent<InventoryManager>().getInventoryItems(i).itemName;
            if (inventory.GetComponent<InventoryManager>().getInventoryItems(i).itemName == null)
                item.itemName = "Null";
            item.Num = inventory.GetComponent<InventoryManager>().getInventoryItems(i).Num;
            item.ind = i;
            //item.SetUse(inventory.GetComponent<InventoryManager>().getItemUse(item.itemName));
            CheckPointInventoryItems.Add(item);

        }
        Cgrapple = player.GetComponent<Grappling>().enabled;
        Cdrugs = player.GetComponent<DrugsMode>().enabled;
        Cfireball = player.GetComponent<PlayerMagicSystem>().enabled;
    }
}
