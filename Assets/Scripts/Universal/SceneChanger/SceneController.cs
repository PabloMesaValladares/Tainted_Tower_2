using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private static SceneController _instance;
    public static SceneController instance => _instance; //Singleton, para cuando quiero que algo sea estatico pero sus propiedades no

    public string levelName;
    public string songname;
    private void Awake()
    {

        if (instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }

    public void SetSongName(string name)
    {
        SoundManager.instance.StopSounds();
        songname = name;
    }

    public void StartLevel(string ln)
    {
        levelName = ln;
        SceneManager.LoadScene("LoadingScreen");
    }


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
#else
                 Application.Quit();
#endif
    }
}
