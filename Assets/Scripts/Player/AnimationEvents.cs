using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{

    public UnityEvent deathStart;
    public UnityEvent deathFinal;
    public UnityEvent fireBallActivate;

    public void DeathStart()
    {
        deathStart.Invoke();
    }

    public void DeathEnded()
    {
        deathFinal.Invoke();
    }

    public void LaunchFireball()
    { 
        fireBallActivate.Invoke();
    }
}
