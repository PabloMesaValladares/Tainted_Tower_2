using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{

    public UnityEvent deathStart;
    public UnityEvent deathFinal;


    public void DeathStart()
    {
        deathStart.Invoke();
    }

    public void DeathEnded()
    {
        deathFinal.Invoke();
    }
}
