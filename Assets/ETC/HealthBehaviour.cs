using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Hurt(int dmg)
    {
        currentHP -= dmg;
        print(currentHP);
    }
}
