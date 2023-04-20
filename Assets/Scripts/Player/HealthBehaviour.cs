using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public bool invencibility;

    public UnityEvent damagedEvent;


    public void AddHealthPercent(int h)
    {
        int perc = maxHP * h / 100;
        currentHP += perc;
        if (currentHP > maxHP)
            currentHP = maxHP;

        damagedEvent.Invoke();
    }
    public void AddHealth(int h)
    {
        currentHP += h;
        if (currentHP > maxHP)
            currentHP = maxHP;

        damagedEvent.Invoke();
    }

    public void Hurt(int dmg)
    {
        if (!invencibility)
            currentHP -= dmg;
        if (currentHP < 0)
            currentHP = 0;
        damagedEvent.Invoke();
        //print(currentHP);
    }
}
