using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<EnemyController>().AddEnemy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<EnemyController>().RemoveEnemy(other.gameObject);
    }
}
