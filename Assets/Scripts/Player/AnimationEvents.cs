using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [Header("Attack Events")]
    public UnityEvent AttackStart;
    public UnityEvent AttackEnd;

    [Header("Death Events")]
    public UnityEvent deathStart;
    public UnityEvent deathFinal;

    [Header("Skill Events")]
    public UnityEvent fireBallActivate;

    public void AttackStarted()
    {
        AttackStart.Invoke();
    }

    public void AttackEnded()
    {
        AttackEnd.Invoke();
    }

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
