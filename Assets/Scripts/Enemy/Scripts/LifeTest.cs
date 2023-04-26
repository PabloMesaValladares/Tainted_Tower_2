using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTest : MonoBehaviour
{
    public float maxLife;
    [Range(0, 1000)]
    public float Life;

    private void Start()
    {
        maxLife = GetComponent<StatController>().health;
        Life = maxLife;
    }
}
