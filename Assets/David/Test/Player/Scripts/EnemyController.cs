using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Enemies;

    public float DistanceToCheck;
    public float maxUpDownDist;

    [Header("Debug")]
    [SerializeField] float distPlayerEnemy;
    [SerializeField] float distPlayerClosest;
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
            distPlayerEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position);
            distPlayerClosest = Vector3.Distance(transform.position, closest.transform.position);
           if (distPlayerEnemy < DistanceToCheck)
            {
                if (distPlayerEnemy < distPlayerClosest)
                {
                    closest = Enemies[i];
                }
            }
        }
        if(closest.transform.position.y > transform.position.y + maxUpDownDist || closest.transform.position.y < transform.position.y - maxUpDownDist)
        {
            return null;
        }
        return closest;
    }
}
