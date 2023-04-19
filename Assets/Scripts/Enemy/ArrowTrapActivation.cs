using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapActivation : MonoBehaviour
{
    public Vector3 directionToGo;

    public List<GameObject> arrows;

    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject arrow in arrows)
        {
            directionToGo = other.transform.position;
            arrow.GetComponent<ArrowMovement>().Move();
        }
    }
}
