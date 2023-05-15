using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsSet : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnLevelWasLoaded(int level)
    {
        if (!GameManager.instance.Checkpoint)
        {
            if (level == SceneManager.GetSceneByName("Game").buildIndex)
                transform.position = GameManager.instance.Spawns;
        }
        else
        {
            if (level == SceneManager.GetSceneByName("Game").buildIndex)
                transform.position = GameManager.instance.CheckPointSpawns;
        }
    }
    void Start()
    {
        if (!GameManager.instance.Checkpoint)
        {
            GetComponent<HealthBehaviour>().currentHP = GameManager.instance.currentHP; //Seteamos la vida actual
            GetComponent<LifeManager>().lifeSlider.value = GameManager.instance.currentHP;
            GetComponent<StatController>().totalMana = GameManager.instance.totalMana;
            GetComponent<StatController>().strength = GameManager.instance.strength;
            GetComponent<StatController>().inteligence = GameManager.instance.inteligence;
            GetComponent<StatController>().stamina = GameManager.instance.staminaStat;
            GetComponent<StatController>().defense = GameManager.instance.defense;
            GetComponent<StaminaController>().SetStamina(GameManager.instance.stamina);

            if (SceneManager.GetActiveScene().name == "Game")
                transform.position = GameManager.instance.Spawns;
            //GetComponent<RespawnPoint>().Respawn();
            GetComponent<Grappling>().enabled = GameManager.instance.grapple;
            GetComponent<DrugsMode>().enabled = GameManager.instance.drugs;
            GetComponent<PlayerMagicSystem>().enabled = GameManager.instance.fireball;
            GetComponent<PillarSpell>().enabled = GameManager.instance.pilar;
        }
        else
        {
            GetComponent<HealthBehaviour>().currentHP = GameManager.instance.CheckPointHP; //Seteamos la vida actual
            GetComponent<LifeManager>().lifeSlider.value = GameManager.instance.CheckPointHP;
            GetComponent<StatController>().totalMana = GameManager.instance.CheckPointMana;
            GetComponent<StatController>().strength = GameManager.instance.CheckPointStrength;
            GetComponent<StatController>().inteligence = GameManager.instance.CheckPointInteligence;
            GetComponent<StatController>().stamina = GameManager.instance.CheckPointStaminaStat;
            GetComponent<StatController>().defense = GameManager.instance.CheckPointDefense;
            GetComponent<StaminaController>().SetStamina(GameManager.instance.CheckPointStamina);

            if (SceneManager.GetActiveScene().name == "Game")
                transform.position = GameManager.instance.CheckPointSpawns;
            //GetComponent<RespawnPoint>().Respawn();
            GetComponent<Grappling>().enabled = GameManager.instance.Cgrapple;
            GetComponent<DrugsMode>().enabled = GameManager.instance.Cdrugs;
            GetComponent<PlayerMagicSystem>().enabled = GameManager.instance.Cfireball;
            GetComponent<PillarSpell>().enabled = GameManager.instance.Cpilar;
        }
        
    }

    public void SpawnPos()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.Checkpoint)
            {
                transform.position = GetComponent<RespawnPoint>().RespawnPosition;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "Game")
                    transform.position = GameManager.instance.CheckPointSpawns;
            }
        }
    }
}
