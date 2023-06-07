using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisappearEffect : MonoBehaviour
{
    public ParticleSystem appearEffect;
    public Material swordMaterial;
    public float duration;

    public void StartDissapear()
    {
        appearEffect.Play();
        StartCoroutine(nameof(StartFadeOut));
    }

    public void StartAppear()
    {
        appearEffect.Play();
        StartCoroutine(nameof(StartFadeIn));
    }

    public IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        float start = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            swordMaterial.SetFloat("Amount", Mathf.Lerp(start, 0, currentTime / duration));
            yield return null;
        }
        yield break;
    }
    public IEnumerator StartFadeIn()
    {
        float currentTime = 0;
        float start = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            swordMaterial.SetFloat("Amount", Mathf.Lerp(start, 1, currentTime / duration));
            yield return null;
        }
        yield break;
    }
}
