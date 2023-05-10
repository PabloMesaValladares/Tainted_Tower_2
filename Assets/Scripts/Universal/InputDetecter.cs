using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputDetecter : MonoBehaviour
{
    private static InputDetecter _instance;
    public static InputDetecter Instance      
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputDetecter>();
            }
            return _instance;
        }
    }

    public bool gamepad;
    public PlayerInput input;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        InputUser.onChange += onInputDeviceChange;
    }

    void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            updateButtonImage(user.controlScheme.Value.name);
        }
    }

    void updateButtonImage(string schemeName)
    {
        if (schemeName.Equals("Gamepad"))
        {
            gamepad = true;
        }
        else
        {
            gamepad = false;
        }
    }

}
