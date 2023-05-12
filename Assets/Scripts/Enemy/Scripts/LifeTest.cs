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
    HealthBehaviour health;
    private void Start()
    {
        maxLife = GetComponent<StatController>().health;
        health = GetComponent<HealthBehaviour>();
    }

    private void Update()
    {
        Life = health.currentHP * 100 / maxLife;
        if (test)
        {
            health.currentHP = (int)(LifePerc * maxLife / 100);
        }
    }
}
