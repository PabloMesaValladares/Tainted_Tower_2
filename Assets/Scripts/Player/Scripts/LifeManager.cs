using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour
{
    StatController stats;
    HealthBehaviour health;
    public Slider lifeSlider;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<StatController>();
        health = GetComponent<HealthBehaviour>();
        health.maxHP = stats.health;
        health.currentHP = stats.health;
        lifeSlider.maxValue = stats.health;
        lifeSlider.value = stats.health;
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
            lifeSlider.value = Mathf.Lerp(lifeSlider.value, targetlife, currentTime);
            yield return null;
        }
        yield break;
    }
}
