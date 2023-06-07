using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PillarSpell : MonoBehaviour
{
    [SerializeField] float rayDistance, distance, coolDown;
    [SerializeField] bool previewB, cdBool;
    [SerializeField] LayerMask floor;

    InputAction interact;
    [SerializeField] PlayerInput _config;


    Timer timer;
    public Slider skillSlider;

    void Start()
    {
        timer = GetComponent<Timer>();
        _config = GetComponent<PlayerInput>();
        interact = _config.actions["Pilar"];

        cdBool = true;
        skillSlider.maxValue = coolDown;
    }

    void Update()
    {
        if (interact.triggered && cdBool)
        {
            SummonPilar();
        }
        else
        {
            skillSlider.value = coolDown - timer.ActualTime();
        }
    }

    void SummonPilar()
    {
        GameObject pillar = PoolingManager.Instance.GetPooledObject("Pillar");

        GetComponent<PlayerController>().animator.SetTrigger("columna");
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