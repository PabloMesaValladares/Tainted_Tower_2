using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Timer timer;
    [SerializeField] float spawnCD;
    [SerializeField] string[] pName;
    [SerializeField] Transform[] spawnPoints;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out TestController player))
        {
            timer.StartTimer(spawnCD);
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GameObject enem = PoolingManager.Instance.GetPooledObject(pName[i]);
                enem.transform.position = spawnPoints[i].position;
                enem.SetActive(true);
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ActivateCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
