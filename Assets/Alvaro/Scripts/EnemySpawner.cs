using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] string[] pName;
    [SerializeField] Transform[] spawnPoints;

    GameObject[] pPrefab;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TestController player))
        {
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                pPrefab[i].SetActive(true);
                pPrefab[i].transform.position = spawnPoints[i].position;
            }
        }
    }
}
