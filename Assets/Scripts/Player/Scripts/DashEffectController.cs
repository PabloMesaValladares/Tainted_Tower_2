using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectController : MonoBehaviour
{

    public GameObject Fire;
    public ParticleSystem startFire;
    public GameObject character;
    

    public void StartEffect()
    {
        Fire.SetActive(true);
        startFire.Play();
        character.SetActive(false);
    }

    public void StopEffect()
    {
        Fire.SetActive(false);
        startFire.Stop();
        character.SetActive(true);
    }
}
