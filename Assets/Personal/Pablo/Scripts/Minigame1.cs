using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame1 : MonoBehaviour
{
    public UnityEvent plateCheck;

    private void OnCollisionEnter(Collision collision)
    {
        plateCheck.Invoke();
        Debug.Log("Emblem Engageeeeeeeeeeeeeeeeeeeeeeeeeee");
    }

}
