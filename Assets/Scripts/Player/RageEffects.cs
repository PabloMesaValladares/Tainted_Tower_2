using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageEffects : MonoBehaviour
{
    public List<ParticleSystem> effects;

   public void Play()
    {
        foreach(ParticleSystem particle in effects)
        {
            particle.Play();
        }
    }


    public void Stop()
    {
        foreach (ParticleSystem particle in effects)
        {
            particle.Stop();
        }
    }
}
