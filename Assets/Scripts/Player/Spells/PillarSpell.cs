using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PillarSpell : MonoBehaviour
{
    [SerializeField] float rayDistance, distance, coolDown;
    [SerializeField] bool previewB, cdBool;
    [SerializeField] LayerMask floor;

    InputAction interact;
    [SerializeField] PlayerInput _config;


    Timer timer;

    void Start()
    {
        timer = GetComponent<Timer>();
        _config = GetComponent<PlayerInput>();
        interact = _config.actions["Pilar"];

        cdBool = true;
    }

    void Update()
    {
        if (interact.triggered && cdBool)
        {
            SummonPilar();
        }
    }

    void SummonPilar()
    {
        GameObject pillar = PoolingManager.Instance.GetPooledObject("Pillar");

        pillar.transform.position = transform.position + transform.forward * distance; 
        pillar.SetActive(true);
        CooldownBool(false);
        timer.StartTimer(coolDown);
    }

    public void CooldownBool(bool b)
    {
        cdBool = b;
    }
}