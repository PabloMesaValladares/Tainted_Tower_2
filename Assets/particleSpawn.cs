using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpawn : MonoBehaviour
{
    [SerializeField]

    public Transform rayCastPoint;
    public ParticleSystem particlesRay;

    private void Update()
    {
        
    }

    public void particlePlay()
    {
        particlesRay.transform.rotation = rayCastPoint.transform.rotation;
        particlesRay.transform.position = rayCastPoint.transform.position;
        particlesRay.Play();

    }
}
