using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsSet : MonoBehaviour
{
    // Start is called before the first frame update

    //private void OnLevelWasLoaded(int level)
    //{
    //    int scene = sceneIndexFromName("OpenWorld");

    //    if (level == scene)
    //    {
    //        if (GameManager.instance.Checkpoint)
    //        {
    //            transform.position = GameManager.instance.CheckPointSpawns;
    //            //GameManager.instance.Checkpoint = false;
    //        }

    //        else
    //            transform.position = GameManager.instance.Spawns;

    //    }
           
    //}

    private int sceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string testedScreen = NameFromIndex(i);
            if (testedScreen == sceneName)
            {
                return i;
            }
        }
        return -1;
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }


    void Awake()
    {

        SetStats();
    }

    public void SetStats()
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

            if (SceneManager.GetActiveScene().name == "OpenWorld")
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

            if (SceneManager.GetActiveScene().name == "OpenWorld")
                transform.position = GameManager.instance.CheckPointSpawns;
            //GetComponent<RespawnPoint>().Respawn();
            GetComponent<Grappling>().enabled = GameManager.instance.Cgrapple;
            GetComponent<DrugsMode>().enabled = GameManager.instance.Cdrugs;
            GetComponent<PlayerMagicSystem>().enabled = GameManager.instance.Cfireball;
            GetComponent<PillarSpell>().enabled = GameManager.instance.Cpilar;
            //GameManager.instance.Checkpoint = false;
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
                if (SceneManager.GetActiveScene().name == "OpenWorld")
                    transform.position = GameManager.instance.CheckPointSpawns;
            }
        }
    }
}
