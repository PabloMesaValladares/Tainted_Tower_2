using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Timer timer;
    [SerializeField] float spawnCD;
    [SerializeField] string[] pName;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField]
    private List<GameObject> enemies;
    private void Start()
    {
        timer = GetComponent<Timer>();
        enemies = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.TryGetComponent(out PlayerController player))
        {
            timer.StartTimer(spawnCD);

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GameObject enem = PoolingManager.Instance.GetPooledObject(pName[i]);
                enem.transform.position = spawnPoints[i].position;
                enem.GetComponentInChildren<Enemy>().SetSpawnPoint(spawnPoints[i]);  
                enem.SetActive(true);

                enemies.Add(enem);
            }
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public bool CheckAliveEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].activeSelf)
                return true;
        }

        ClearList();
        return false;
    }

    public void ActivateCollider()
    {
        if(!CheckAliveEnemies())
            gameObject.GetComponent<SphereCollider>().enabled = true;
        else
            timer.StartTimer(spawnCD);
    }

    void ClearList()
    {
        enemies.Clear();
    }
}
