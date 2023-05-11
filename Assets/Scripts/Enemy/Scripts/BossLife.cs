using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    HealthBehaviour health;
    StatController stats;

    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<HealthBehaviour>();
        stats = GetComponent<StatController>();

        health.maxHP = stats.health;
        health.currentHP = stats.health;
    }

  
}
