using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSpawnSet : MonoBehaviour
{
    public void setSpawnPos(GameObject other)
    {
        Vector3 posToResp = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        other.GetComponent<RespawnPoint>().SetRespawn(posToResp);
        GameManager.instance.SetScripts();
        if (SceneManager.GetActiveScene().name == "Game")
            GameManager.instance.Spawns = transform.position;

        //GameManager.instance.Checkpoint = true;
    }
}
