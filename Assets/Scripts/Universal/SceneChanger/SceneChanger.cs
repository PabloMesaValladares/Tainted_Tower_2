using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void changeMusic(string musicName)
    {
        SceneController.instance.SetSongName(musicName);
    }
    public void changeScene(string lvlname)
    {
        SceneController.instance.StartLevel(lvlname);
    }

    public void ResetGame()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.reset = true;
        }
    }

    public void ExitGame()
    {
        SceneController.instance.Exit();
    }

}
