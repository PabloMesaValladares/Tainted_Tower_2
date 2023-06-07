using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisappearEffect : MonoBehaviour
{
    public ParticleSystem appearEffect;
    public ParticleSystem trailEffect;
    public Material swordMaterial;
    public float duration;
    public bool active = false;
    public GameObject swordObj;

    public void StartDissapear()
    {
        appearEffect.Play();
        StartCoroutine(nameof(StartFadeOut));
    }

    public void StartAppear()
    {
        swordObj.SetActive(true);
        appearEffect.Play();
        StartCoroutine(nameof(StartFadeIn));
    }

    public IEnumerator StartFadeIn()
    {
        float currentTime = 0;
        float start = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            swordMaterial.SetFloat("_Amount", Mathf.Lerp(start, 0, currentTime / duration));
            yield return null;
        }
        active = true;
        yield break;
    }
    public IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        float start = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            swordMaterial.SetFloat("_Amount", Mathf.Lerp(start, 1, currentTime / duration));
            yield return null;
        }
        active = false;
        swordObj.SetActive(false);
        yield break;
    }
}
