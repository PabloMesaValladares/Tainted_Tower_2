using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PillarSpell : MonoBehaviour
{
    [SerializeField] float rayDistance, distance, coolDown;
    [SerializeField] bool previewB, cdBool;
    [SerializeField] LayerMask floor;
    float markedLayer;

    InputAction interact;
    [SerializeField] PlayerInput _config;

    Transform raycastPoint;

    MarkEnemy mark;
    Timer timer;

    GameObject preview;

    void Start()
    {
        mark = GetComponent<MarkEnemy>();
        timer = GetComponent<Timer>();
        _config = GetComponent<PlayerInput>();
        //interact = _config.actions[""];                    Falta tecla

        // preview = PoolingManager.Instance("PillarPrev");
        
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

        if(/*interact.triggered &&*/ cdBool)
        {
            SummonPilar();
        }
    }
    
    public void RaycastInfo()
    {
        Transform camera = Camera.main.transform;

        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, rayDistance, NameToLayer(floor)))
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

            if (Physics.Raycast(transform.forward + new Vector3(0, 0, distance), -pillar.transform.up, out RaycastHit hit, rayDistance, NameToLayer(floor)))
            {
                pillar.transform.position = hit.point;
                pillar.SetActive(true);
                CooldownBool(false);
                timer.StartTimer(coolDown);
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

    public void CooldownBool(bool b)
    {
        cdBool = b;
    }
}