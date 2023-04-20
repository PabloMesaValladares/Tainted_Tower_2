using UnityEngine;
using System.Collections;
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

    private void Awake()
    {
        scene = sceneIndexFromName(SceneController.instance.levelName);
    }

    // Updates once per frame
    void Update() {

        if (sceneFound && !loadScene)
        {
            //SoundManager.instance.StopSounds();
            loadScene = true;
            StartCoroutine(LoadNewScene());
            StartCoroutine(changeAlpha(0));

        }

        if (loadScene == true) {
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

    IEnumerator LoadNewScene() {

        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        if (SceneController.instance.songname != null)
        {
            SoundManager.instance.StopSounds();
            SoundManager.instance.PlaySound(SceneController.instance.songname, true);
        }

        while (!async.isDone) {
            yield return null;
        }

    }
    public IEnumerator changeAlpha(float targetAlpha)
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            loadingText.alpha = Mathf.Lerp(loadingText.alpha, targetAlpha, currentTime);
            yield return null;
        }
        StartCoroutine(changeAlpha(1));
        yield break;
    }
}
