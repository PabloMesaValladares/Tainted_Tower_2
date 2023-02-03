using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Enemies;

    public void AddEnemy(GameObject enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }

    public GameObject GetCloseEnemy()
    {
        GameObject closest;
        if (Enemies.Count > 0)
            closest = Enemies[0];
        else
            return null;

        for(int i = 0; i < Enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = Enemies[i];
            }
        }

        return closest;
    }
}
