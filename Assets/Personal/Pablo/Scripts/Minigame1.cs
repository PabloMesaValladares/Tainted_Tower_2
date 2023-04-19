using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame1 : MonoBehaviour
{
    public UnityEvent plateCheck;
    public BoxCollider boxcollider;

    private void OnEnable()
    {
        Minigame1Administrator.reset += EnableCollider;
    }

    private void OnDisable()
    {
        Minigame1Administrator.reset -= EnableCollider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        plateCheck.Invoke();
        Debug.Log("Emblem Engageeeeeeeeeeeeeeeeeeeeeeeeeee");
        boxcollider.enabled = false;
    }

    public void EnableCollider()
    {
        boxcollider.enabled = true;
    }
}
