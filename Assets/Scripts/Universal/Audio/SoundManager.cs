using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
//Singleton //Un Singleton es una clase estatica que no es estatica

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance;
    public static SoundManager instance => _instance; //Singleton, para cuando quiero que algo sea estatico pero sus propiedades no

    [Serializable]
    public class AudioLists
    {
        public string type;
        public AudioClip audioSelected;
        public AudioMixerGroup mixer;
        [Range(0,1)]
        public float volume;
    };

    [SerializeField]
    private List<AudioLists> audioTypes = new List<AudioLists>();
    [SerializeField]
    private List<AudioSource> audioControllers = new List<AudioSource>();

    [SerializeField]
    private Dictionary<string, AudioLists> _audios;

    public string currentBGM;

    private void Start()
    {
        PlaySound(currentBGM, true);
    }

    // Start is called before the first frame update
    private void Awake()
    {

        if (instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);

        _audios = new Dictionary<string, AudioLists>();
        
        for(int i = 0; i < audioTypes.Count; i++)
        {
            _audios.Add(audioTypes[i].type, audioTypes[i]);
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            audioControllers.Add(gameObject.transform.GetChild(i).GetComponent<AudioSource>());
        }

    }


    public void PlaySound(string name, bool loop)
    {
        if(_audios.ContainsKey(name))//if exist in dictionary
        {
            for(int i = 0; i < audioControllers.Count; i++)
            {
                if(audioControllers[i].clip == null || !audioControllers[i].isPlaying)
                {
                    audioControllers[i].clip = _audios[name].audioSelected;
                    audioControllers[i].volume = _audios[name].volume;
                    audioControllers[i].loop = loop;
                    audioControllers[i].outputAudioMixerGroup = _audios[name].mixer;
                    if(_audios[name].mixer.name == "Music")
                    {
                        currentBGM = name;
                    }
                    audioControllers[i].Play();
                    break;
                }
            }
        }
    }

    public void StopSound(string name)
    {
        if (_audios.ContainsKey(name))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip != null && audioControllers[i].isPlaying)
                {
                    if(audioControllers[i].clip.name == name)
                    audioControllers[i].Stop();
                }
            }
        }
    }

    public bool checkSound(string name)
    {
        if (_audios.ContainsKey(name))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip != null && audioControllers[i].isPlaying)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void FadeInFadeOut(string nameIn, float duration)
    {
        Debug.Log("Entro");
        AudioSource audioIn = audioControllers[0];
        AudioSource audioOut = audioControllers[0];

        if (_audios.ContainsKey(nameIn))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip == null || !audioControllers[i].isPlaying)
                {
                    audioControllers[i].clip = _audios[nameIn].audioSelected;
                    audioControllers[i].volume = 0;
                    audioControllers[i].loop = true;
                    audioControllers[i].outputAudioMixerGroup = _audios[nameIn].mixer;
                    audioControllers[i].Play();
                    audioIn = audioControllers[i];
                }
            }
        }

        if (_audios.ContainsKey(currentBGM))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip != null && audioControllers[i].isPlaying)
                {
                    if (audioControllers[i].clip.name == currentBGM)
                        audioOut = audioControllers[i];
                }
            }
        }

        StartCoroutine(StartFade(audioIn, audioOut, duration, audioOut.volume));
    }


    public void FadeInFadeOut(string nameIn, string nameOut, float duration)
    {
        Debug.Log("Entro");
        AudioSource audioIn = audioControllers[0];
        AudioSource audioOut = audioControllers[0];

        if (_audios.ContainsKey(nameIn))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip == null || !audioControllers[i].isPlaying)
                {
                    audioControllers[i].clip = _audios[nameIn].audioSelected;
                    audioControllers[i].volume = 0;
                    audioControllers[i].loop = true;
                    audioControllers[i].outputAudioMixerGroup = _audios[nameIn].mixer;
                    audioControllers[i].Play();
                    audioIn = audioControllers[i];
                }
            }
        }

        if (_audios.ContainsKey(nameOut))//if exist in dictionary
        {
            for (int i = 0; i < audioControllers.Count; i++)
            {
                if (audioControllers[i].clip != null && audioControllers[i].isPlaying)
                {
                    if (audioControllers[i].clip.name == nameOut)
                        audioOut = audioControllers[i];
                }
            }
        }

        StartCoroutine(StartFade(audioIn, audioOut, duration, audioOut.volume));
    }

    public static IEnumerator StartFade(AudioSource In, AudioSource Out, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = In.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            Out.volume = Mathf.Lerp(targetVolume, 0, currentTime / duration);
            In.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    void LowerVolume(AudioSource audIn, AudioSource audOut, float vol)
    {
       if(audIn.volume < _audios[audOut.clip.name].volume)
        {
            Debug.Log("Volumen es : " + vol);
            audIn.volume += vol;
            audOut.volume -= vol;
            LowerVolume(audIn, audOut, vol);
        }
       else
        {
            audOut.Stop();
            audOut.clip = null;
        }
    }

    public void StopSounds()
    {
        for (int i = 0; i < audioControllers.Count; i++)
        {
            audioControllers[i].Stop();
        }
    }
}
