using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTest : MonoBehaviour
{
    private MeshRenderer _mr;

    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
    }

    private void OnBecameVisible()
    {
        print("is visible");
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        print("is not visible");
        enabled = false;
    }
}
