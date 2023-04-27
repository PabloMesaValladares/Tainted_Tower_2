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
    public UnityEvent deathEvent;

    public float dur, quantity;
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
        if (currentHP <= 0)
        {
            currentHP = 0;
            deathEvent.Invoke();
        }
        else
            damagedEvent.Invoke();
        //print(currentHP);
    }

    public void startRoutine(GameObject c)
    {
        StartCoroutine(changeSlider(c, dur));
    }

    public IEnumerator changeSlider(GameObject cam, float duration)
    {
        float max = cam.GetComponent<ShaderController>().maxDistance;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            cam.GetComponent<ShaderController>().maxDistance = Mathf.Lerp(max, quantity, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
