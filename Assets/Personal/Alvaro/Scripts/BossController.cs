using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction test;
    PatternBehaviour _pb;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        test = playerInput.actions["Test"];
        _pb = GetComponent<PatternBehaviour>();
    }

    private void Update()
    {
        if(test.triggered)
        {
            _pb.Pattern();
        }
    }
}
