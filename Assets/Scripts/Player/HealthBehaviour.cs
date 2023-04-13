using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public UnityEvent damagedEvent;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Hurt(int dmg)
    {
        currentHP -= dmg;
        if (currentHP < 0)
            currentHP = 0;
        damagedEvent.Invoke();
        //print(currentHP);
    }
}
