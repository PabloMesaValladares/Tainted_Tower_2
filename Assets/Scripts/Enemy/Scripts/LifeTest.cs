using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTest : MonoBehaviour
{
    public float maxLife;
    public float Life;
    [Header("Debug")]
    [Range(0, 100)]
    public float LifePerc;
    public bool test;
    Enemy enemyScript;
    private void Start()
    {
        maxLife = GetComponent<StatController>().health;
        //Life = maxLife;
        enemyScript = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (test)
        {
            Debug.Log("El porcentaje de vida es " + LifePerc * 100 / maxLife);
            enemyScript.health = LifePerc * maxLife / 100;
        }
    }
}
