using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthBehaviour>().currentHP = GameManager.instance.currentHP; //Seteamos la vida actual
        GetComponent<LifeManager>().lifeSlider.value = GameManager.instance.currentHP;
        GetComponent<StatController>().totalMana = GameManager.instance.totalMana;
        GetComponent<StatController>().strength = GameManager.instance.strength;
        GetComponent<StatController>().inteligence = GameManager.instance.inteligence;
        GetComponent<StatController>().stamina = GameManager.instance.staminaStat;
        GetComponent<StatController>().defense = GameManager.instance.defense;
        GetComponent<StaminaController>().SetStamina(GameManager.instance.stamina);
        GetComponent<RespawnPoint>().RespawnPosition = GameManager.instance.RespawnPosition;
        GetComponent<RespawnPoint>().Respawn();
        GetComponent<Grappling>().enabled = GameManager.instance.grapple;
    }

}
