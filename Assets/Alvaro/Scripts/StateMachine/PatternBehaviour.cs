using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBehaviour : MonoBehaviour
{
    [SerializeField] private AttackSO[] attack;

    public int index = 0;
    public float time;
    public bool random;

    private void Start()
    {
        InvokeRepeating("Pattern", 2.0f, 2.0f);
    }


    void Pattern()
    {
        if (random)
        {
            attack[GetNextAttack()].Execute();
            return;
        }
        else
        {
            if (index < attack.Length)
            {
                attack[index].Execute();
                index++;
            }
            else
                index = 0;
        }
    }

    int GetNextAttack()
    {
        int rand = Random.Range(0, attack.Length); 
        
        return rand;
    }
}
