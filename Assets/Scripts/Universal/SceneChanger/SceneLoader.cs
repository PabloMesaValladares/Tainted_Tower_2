using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private bool loadScene = false;
    private bool sceneFound = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    public Slider Bar;
    float target;

    private void Awake()
    {
        scene = sceneIndexFromName(SceneController.instance.levelName);
        if (sceneFound)
        {
            loadScene = true;
            //LoadSceneNew(scene);
            StartCoroutine(LoadNewScene());
        }
    }

    // Updates once per frame
    void Update()
    {

        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

    }
    private int sceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string testedScreen = NameFromIndex(i);
            if (testedScreen == sceneName)
            {
                sceneFound = true;
                return i;
            }
        }
        return -1;
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public async void LoadSceneNew(int sceneInt)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneInt);

        do
        {
            await System.Threading.Tasks.Task.Delay(100);
            Bar.value = scene.progress;
        } while (scene.progress < 0.9f);

        await System.Threading.Tasks.Task.Delay(1000);

        scene.allowSceneActivation = true;
        
    }

    IEnumerator LoadNewScene() {

        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        if (SceneController.instance.songname != null)
        {
            SoundManager.instance.StopSounds();
            SoundManager.instance.FadeInFadeOut(SceneController.instance.songname, 1);
        }


        while (!async.isDone)
        {
            target = async.progress;
            Bar.value = target;
            yield return null;
        }
        yield return new WaitForSeconds(3);
    }
}
