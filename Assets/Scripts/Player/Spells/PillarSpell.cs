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

        NameToLayer(markable);
        
        previewB = false;
    }

    void Update()
    {
        if (!mark.marking)
        {
            previewB = false;
            return;
        }

        if (!previewB)
            PreviewState(previewB);
        else
        {
            RaycastInfo();
            preview.transform.position = raycastPoint.position;
            previewB = true;
            PreviewState(previewB);
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
            pillar.transform.position = transform.forward + new Vector3(0, 0, distance);

            if (Physics.Raycast(transform.forward + new Vector3(0, 0, distance), -pillar.transform.up, out RaycastHit hit, rayDistance, NameToLayer(8)))
            {
                pillar.transform.position = hit.point;
                pillar.SetActive(true);
            }
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

    int NameToLayer(LayerMask _lm)
    {
        var rawValue = _lm.value;
        var layerValue = Mathf.Log(rawValue, 2);
        markedLayer = layerValue;

        return (int)markedLayer;
    }
}