using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    Animator anim;
    [SerializeField] float scrollSpeed;

    public SceneController scene;
    [SerializeField] string scName, soName;

    PlayerInput _config;

    InputAction esc;
    InputAction space;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.speed = scrollSpeed;
        _config = GetComponent<PlayerInput>();

        esc = _config.actions["Pause"];
        space = _config.actions["Jump"];
    }

    void Update()
    {
        if(esc.triggered || space.triggered)
        {
            SceneController.instance.StartLevel(scName);
            SceneController.instance.SetSongName(soName);
        }
    }
}
