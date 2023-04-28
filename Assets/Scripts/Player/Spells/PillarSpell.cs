using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpell : MonoBehaviour
{
    [SerializeField] float distance;

    public LayerMask markable;
    float markedLayer;

    void Start()
    {
        var rawValue = markable.value;
        var layerValue = Mathf.Log(rawValue, 2);
        markedLayer = layerValue;
    }

    void Update()
    {
        
    }

    public void RaycastDistance()
    {
        RaycastHit hit;
    }
}
