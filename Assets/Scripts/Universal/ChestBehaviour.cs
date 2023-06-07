using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Item;

public class ChestBehaviour : MonoBehaviour
{
    [Header("Chest Config")]
    public Item item;
    public string animationName;
    public ParticleSystem chestOpen;

    Animator chestAnimator;

    [Header("Inputs")]
    public PlayerInput input;
    InputAction Interact;

    public delegate void SendItems(Item item);
    public static SendItems sendItem;

    private void Start()
    {
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        Interact = input.actions["Interact"];
        chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Interact.triggered)
        {
            //Debug.Log("Interactuado con " + gameObject.name);
            chestAnimator.SetTrigger(animationName);
            InventoryManager.instance.UpdateSlot(item);
            GetComponent<SphereCollider>().enabled = false;
            chestOpen.Play();
            this.enabled = false;
        }
    }
}
