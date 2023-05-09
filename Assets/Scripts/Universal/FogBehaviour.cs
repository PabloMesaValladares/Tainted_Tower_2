using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBehaviour : MonoBehaviour
{
    ParticleSystem fog; 
    ParticleSystem.ShapeModule ps;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        fog = GetComponent<ParticleSystem>();
        ps = fog.shape;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ps.radius = cam.farClipPlane;
    }
}
