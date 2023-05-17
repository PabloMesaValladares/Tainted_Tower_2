using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSpawnSet : MonoBehaviour
{
    public GameObject position;
    public void setSpawnPos(GameObject other)
    {
        other.GetComponent<RespawnPoint>().SetRespawn(position.transform.position);
        GameManager.instance.SetScripts();
        GameManager.instance.CheckPoint();
        GameManager.instance.CheckPointSpawns = position.transform.position;
        GameManager.instance.Spawns = position.transform.position;

        //GameManager.instance.Checkpoint = true;
    }
}
