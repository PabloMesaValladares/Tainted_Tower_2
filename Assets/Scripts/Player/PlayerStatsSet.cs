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
            for (int i = 0; i < GameManager.instance.Spawns.Length; i++)
            {
                if (GameManager.instance.Spawns[i].SceneName == SceneManager.GetSceneByBuildIndex(level).name)
                {
                    Debug.Log(GameManager.instance.Spawns[i].position);
                    transform.position = GameManager.instance.Spawns[i].position;
                }
            }
        }
        else
        {
            for (int i = 0; i < GameManager.instance.CheckPointSpawns.Length; i++)
            {
                if (GameManager.instance.CheckPointSpawns[i].SceneName == SceneManager.GetSceneByBuildIndex(level).name)
                    transform.position = GameManager.instance.CheckPointSpawns[i].position;
            }
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
            for (int i = 0; i < GameManager.instance.Spawns.Length; i++)
            {
                if (GameManager.instance.Spawns[i].SceneName == SceneManager.GetActiveScene().name)
                    transform.position = GameManager.instance.Spawns[i].position;
            }
            //GetComponent<RespawnPoint>().Respawn();
            GetComponent<Grappling>().enabled = GameManager.instance.grapple;
            GetComponent<DrugsMode>().enabled = GameManager.instance.drugs;
            GetComponent<PlayerMagicSystem>().enabled = GameManager.instance.fireball;
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
            for (int i = 0; i < GameManager.instance.CheckPointSpawns.Length; i++)
            {
                if (GameManager.instance.CheckPointSpawns[i].SceneName == SceneManager.GetActiveScene().name)
                    transform.position = GameManager.instance.CheckPointSpawns[i].position;
            }
            //GetComponent<RespawnPoint>().Respawn();
            GetComponent<Grappling>().enabled = GameManager.instance.Cgrapple;
            GetComponent<DrugsMode>().enabled = GameManager.instance.Cdrugs;
            GetComponent<PlayerMagicSystem>().enabled = GameManager.instance.Cfireball;
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
            {for (int i = 0; i < GameManager.instance.CheckPointSpawns.Length; i++)
            {
                if (GameManager.instance.CheckPointSpawns[i].SceneName == SceneManager.GetActiveScene().name)
                    transform.position = GameManager.instance.CheckPointSpawns[i].position;
            }
            }
        }
    }
}
