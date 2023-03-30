using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenusController : MonoBehaviour
{
    public void ReturndedMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LevelSelect(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void Controls()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("HandBook");
    }
    public void Instructions2()
    {
        SceneManager.LoadScene("HandBook1");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
