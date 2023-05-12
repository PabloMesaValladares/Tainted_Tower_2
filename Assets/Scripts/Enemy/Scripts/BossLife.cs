using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    HealthBehaviour health;
    StatController stats;

    public Slider Life;
    public Vector3 StartPos;

    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<HealthBehaviour>();
        stats = GetComponent<StatController>();

        health.maxHP = stats.health;
        health.currentHP = stats.health;
        Life.maxValue = health.maxHP;
        Life.value = health.maxHP;
        StartPos = transform.position;
    }

    public void changeHealthSlider()
    {
        StartCoroutine(changeSlider(health.currentHP));
    }
    public IEnumerator changeSlider(float targetlife)
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            Life.value = Mathf.Lerp(Life.value, targetlife, currentTime);
            yield return null;
        }
        yield break;
    }

    public void ResetPos()
    {
        transform.position = StartPos;
    }
}
