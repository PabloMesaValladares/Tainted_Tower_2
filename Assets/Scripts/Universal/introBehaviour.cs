using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class introBehaviour : MonoBehaviour
{
    
    [Header("Objects")]
    [SerializeField]
    private VideoPlayer introVideo;
    [SerializeField]
    private GameObject _bacground, _videoIntro;

    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    InputAction esc, spc;

    [SerializeField]
    private bool isIntro;

    [Header("Values")]
    [SerializeField]
    private int sceneNumber;

    // Start is called before the first frame update
    void Awake()
    {
        if(isIntro)
        {
            introVideo.loopPointReached += CheckOver;
        }
        else
        {
            gameObject.SetActive(false);
        }
        esc = _config.actions["Pause"];
        spc = _config.actions["Jump"];
    }

    private void CheckOver(UnityEngine.Video.VideoPlayer vid)
    {
        //SceneManager.LoadScene(sceneNumber);
        if(isIntro)
        {
            _videoIntro.SetActive(true);
        }      
        else
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(esc.triggered || spc.triggered)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}