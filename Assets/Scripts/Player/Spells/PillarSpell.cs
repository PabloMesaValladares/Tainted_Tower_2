using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpell : MonoBehaviour
{
    [SerializeField] float rayDistance, distance;
    [SerializeField] bool previewB;
    public LayerMask markable;
    float markedLayer;

    Transform raycastPoint;

    MarkEnemy mark;

    GameObject preview;

    void Start()
    {
        mark = GetComponent<MarkEnemy>();

        // preview = PoolingManager.Instance("PillarPrev");

        var rawValue = markable.value;
        var layerValue = Mathf.Log(rawValue, 2);
        markedLayer = layerValue;
        previewB = false;
    }

    void Update()
    {
        if (!mark.marking)
        {
            
        }
        else
        {
            if (!previewB)
                PreviewState(previewB);
            else
            {
                RaycastInfo();
                PreviewState(true);
                preview.transform.position = raycastPoint.position;
                previewB = true;
            }
        }
    }

    public void RaycastInfo()
    {
        Transform camera = Camera.main.transform;

        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, rayDistance, (int)markedLayer))
        {
            raycastPoint.position = hit.point;
        }
    }

    void SummonPilar()
    {
        GameObject pillar = PoolingManager.Instance.GetPooledObject("Pillar");

        if (!mark.marking)
        {

        }
        else
        {
            pillar.transform.position = raycastPoint.position;
        }

    }

    void PreviewState(bool b)
    {
        preview.SetActive(b);
    }
}
