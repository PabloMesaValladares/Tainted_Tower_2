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
            GameObject arr;
            arr = PoolingManager.Instance.GetPooledObject("arrow");
            arr.transform.position = arrow.transform.position;
            arr.transform.rotation = arrow.transform.rotation;
            arr.SetActive(true);
            arr.GetComponent<SlashMovement>().MoveDirection(arrow.transform.forward.normalized);
            arr.transform.rotation = arrow.transform.rotation;
            //arr.GetComponent<ArrowMovement>().Move();
        }
    }
}
