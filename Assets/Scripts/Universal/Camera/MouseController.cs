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
    public GameObject pauseMenu;

    public CinemachineBrain brain;
    public float ClipPlane;

    InputAction activate;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        activate = playerInput.actions["Unlock"];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = ClipPlane;
        if (activate.IsPressed())
        {
            Unlock();
        }
        else if (pauseMenu.activeInHierarchy)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }



    void Unlock()
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
    void Lock()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NormalCamera.GetComponent<CinemachineInputProvider>().enabled = AimCamera.GetComponent<CinemachineInputProvider>().enabled = true;
    }
}
