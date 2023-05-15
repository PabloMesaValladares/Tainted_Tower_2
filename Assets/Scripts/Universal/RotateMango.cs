using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMango : MonoBehaviour
{
    public float degreesPerSecond;


    private void Update()
    {
        Vector3 position = GetComponent<Renderer>().bounds.center;

        transform.RotateAround(position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<HealthBehaviour>(out HealthBehaviour healt))
        {
            healt.AddHealthPercent(50);
        }
    }
}
