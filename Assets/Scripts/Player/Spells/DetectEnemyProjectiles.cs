using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyProjectiles : MonoBehaviour
{

    [SerializeField] LayerMask layer;
    float _layerValue;

    private void Start()
    {
        var rawValue = layer.value;
        var layerValue = Mathf.Log(rawValue, 2);
        _layerValue = layerValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _layerValue)
        {
            other.gameObject.SetActive(false);
        }
    }
}
