using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Cinemachine;

public class MouseController : MonoBehaviour
{
    PlayerInput playerInput;
    public CinemachineVirtualCamera NormalCamera;
    public CinemachineVirtualCamera AimCamera;
    public GameObject pauseMenu, BagMenu, deathMenu;

    public bool unlock;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        unlock = false;
    }

    private void OnEnable()
    {
        ControllerInventory.mouseUnlock += changeLockState;
        ControllerInventory.mouseLock += changeLockState;
    }
    private void OnDisable()
    {
        ControllerInventory.mouseUnlock -= changeLockState;
        ControllerInventory.mouseLock -= changeLockState;

    }

    // Update is called once per frame
    void Update()
    {
        if (unlock)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    public void changeLockState(bool b)
    {
        unlock = b;
    }


    public void Unlock()
    {
        player.GetComponent<PlayerInput>().enabled = false;
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
    public void Lock()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = AimCamera.GetComponent<CinemachineInputProvider>().enabled = true;
    }
}
