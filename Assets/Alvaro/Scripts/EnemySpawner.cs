using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] string[] pName;
    [SerializeField] Transform[] spawnPoints;

    GameObject pPrefab;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent<PlayerController>(out PlayerController player))
        {
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                pPrefab = PoolingManager.Instance.GetPooledObject(pName[i]);
                pPrefab.transform.position = spawnPoints[i].position;
            }
        }
    }
}
