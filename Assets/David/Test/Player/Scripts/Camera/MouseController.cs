using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class MouseController : MonoBehaviour
{
    public PlayerInput playerInput;
    public CinemachineVirtualCamera NormalCamera;
    public CinemachineVirtualCamera AimCamera;


    InputAction Unlock;

    // Start is called before the first frame update
    void Start()
    {
        Unlock = playerInput.actions["Unlock"];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Unlock.IsPressed())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            NormalCamera.GetComponent<CinemachineInputProvider>().enabled = false;
            AimCamera.GetComponent<CinemachineInputProvider>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            NormalCamera.GetComponent<CinemachineInputProvider>().enabled = AimCamera.GetComponent<CinemachineInputProvider>().enabled = true;
        }
    }
}
