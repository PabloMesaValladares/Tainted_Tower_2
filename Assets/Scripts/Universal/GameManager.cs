using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Header("Menu")]
    GameObject inventory;
    Item[] inventoryItems;

    [Header("Skills")]
    public bool grapple, pilar, drugs, fireball;
    public bool firstSet;
    //Meter scripts skills para guardar cooldown

    private void Awake()
    {

        if (instance == null)
        {
            _instance = this;

            SetScripts();
            firstSet = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!firstSet)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<HealthBehaviour>().currentHP = currentHP; //Seteamos la vida actual
            player.GetComponent<StatController>().totalMana = totalMana;
            player.GetComponent<StatController>().strength = strength;
            player.GetComponent<StatController>().inteligence = inteligence;
            player.GetComponent<StatController>().stamina = staminaStat;
            player.GetComponent<StatController>().defense = defense;
            player.GetComponent<StaminaController>().SetStamina(stamina);
            player.GetComponent<RespawnPoint>().RespawnPosition = RespawnPosition;
            player.GetComponent<RespawnPoint>().Respawn();
            player.GetComponent<Grappling>().enabled = grapple;
            inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<InventoryManager>().setInventoryItems(inventoryItems);
        }
        else
            firstSet = false;
    }

    public void SetScripts()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = player.GetComponent<HealthBehaviour>().maxHP; //Seteamos la vida actual
        totalMana = player.GetComponent<StatController>().totalMana;
        strength = player.GetComponent<StatController>().strength;
        inteligence = player.GetComponent<StatController>().inteligence;
        staminaStat = player.GetComponent<StatController>().stamina;
        defense = player.GetComponent<StatController>().defense;
        stamina = player.GetComponent<StaminaController>().ReturnStamina();
        RespawnPosition = player.GetComponent<RespawnPoint>().RespawnPosition;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventoryItems = inventory.GetComponent<InventoryManager>().getInventoryItems();
    }
}
