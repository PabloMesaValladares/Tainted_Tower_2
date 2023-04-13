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
    public GameObject pauseMenu;

    InputAction activate;

    // Start is called before the first frame update
    void Start()
    {
        activate = playerInput.actions["Unlock"];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(activate.IsPressed())
        {
            Lock();
        }
        else if(pauseMenu.activeInHierarchy)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    void Lock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = false;
        AimCamera.GetComponent<CinemachineInputProvider>().enabled = false;
    }
    void Unlock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = AimCamera.GetComponent<CinemachineInputProvider>().enabled = true;
    }
}
