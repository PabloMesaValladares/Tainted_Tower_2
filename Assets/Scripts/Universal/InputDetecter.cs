using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputDetecter : MonoBehaviour
{
    private static InputDetecter _instance;
    public static InputDetecter Instance         // => _instance;
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
        //buttonImage = GetComponent<Image>();
        //updateButtonImage(input.currentControlScheme);
        input = GetComponent<PlayerInput>();
        Debug.Log(input.currentControlScheme);
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
        // assuming you have only 2 schemes: keyboard and gamepad
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
