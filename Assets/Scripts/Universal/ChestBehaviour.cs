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

    public GameObject ChestCover;
    Animator chestAnimator;

    [Header("Inputs")]
    public PlayerInput input;
    InputAction Interact;

    public delegate void SendItems(Item item);
    public static SendItems sendItem;

    private void Start()
    {
        Interact = input.actions["Interact"];
        chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Interact.triggered)
        {
            chestAnimator.SetTrigger("open");
            sendItem(item);
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
