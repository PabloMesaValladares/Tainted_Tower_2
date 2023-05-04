using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadedSpawn : MonoBehaviour
{
    GameObject player;

    public void SetSpawnPos()
    {
        Debug.Log("AJIA");
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStatsSet>().SpawnPos();
    }
}
