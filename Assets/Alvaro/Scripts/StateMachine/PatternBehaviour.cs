using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBehaviour : MonoBehaviour
{
    [SerializeField] private AttackSO[] attack;

    public int index = 0;
    public bool random;

    public void Pattern()
    {
        UpdateIndex();
    }

    void GetNextAttack()
    {
        index = Random.Range(0, attack.Length); 
    }

    public IEnumerator CallNextAttack(float d)
    {
        yield return new WaitForSeconds(d);

        Pattern();
    }

    private void UpdateIndex()
    {
        if (random)
        {
            GetNextAttack();
            attack[index].Execute();
            StartCoroutine(CallNextAttack(attack[index].delay));
        }
        else
        {
            if (index < attack.Length)
            {
                attack[index].Execute();
                StartCoroutine(CallNextAttack(attack[index].delay));
                index++;
            }
            else
            {
                index = 0;
                StartCoroutine(CallNextAttack(attack[attack.Length - 1].delay));
            }
        }
    }
}
