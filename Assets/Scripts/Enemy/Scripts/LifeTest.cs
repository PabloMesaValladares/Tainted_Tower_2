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
    private void Start()
    {
        maxLife = GetComponent<StatController>().health;
        Life = maxLife;
    }

    private void Update()
    {
        if (test)
            Life = LifePerc * maxLife / 100;
    }
}
