using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderManager : MonoBehaviour
{
    private Slider slider;
    private AudioMixer audioMixer;
    public string mixerName;

    private void Start()
    {
        slider = GetComponent<Slider>();
        audioMixer = GetComponent<AudioMixer>();

        slider.value = PlayerPrefs.GetFloat(mixerName, 0f);
    }

}
