using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Cinemachine;

public class MouseController : MonoBehaviour
{
    public PlayerInput playerInput;
    public CinemachineVirtualCamera NormalCamera;
    public CinemachineVirtualCamera AimCamera;
    public GameObject pauseMenu;

    InputAction activate;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        activate = playerInput.actions["Unlock"];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = playerInput.gameObject;
    }

    private void OnEnable()
    {
        ControllerInventory.mouseUnlock += Unlock;
        ControllerInventory.mouseLock += Lock;
    }
    private void OnDisable()
    {
        ControllerInventory.mouseUnlock -= Unlock;
        ControllerInventory.mouseLock -= Lock;
    }

    // Update is called once per frame
    void Update()
    {
        if(activate.IsPressed())
        {
            Unlock();
        }
    }



    void Unlock()
    {
        player.GetComponent<PlayerController>().enabled = false;
        if(InputDetecter.Instance.gamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = false;
        AimCamera.GetComponent<CinemachineInputProvider>().enabled = false;
    }
    void Lock()
    {
        player.GetComponent<PlayerController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = AimCamera.GetComponent<CinemachineInputProvider>().enabled = true;
    }
}
