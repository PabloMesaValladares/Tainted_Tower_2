using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAudio : MonoBehaviour
{
    public UnityEngine.InputSystem.PlayerInput input;
    public InputAction activate;

    // Start is called before the first frame update
    void Start()
    {
        activate = input.actions["Test"];
        SoundManager.instance.PlaySound("Mario", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(activate.triggered)
        {
            SoundManager.instance.FadeInFadeOut("Jizz", "Mario");
        }
    }
}
