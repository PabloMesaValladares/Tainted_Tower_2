using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    private Slider slider;
    public AudioMixer audioMixer;
    public string nameController;
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(nameController, 1f);

    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat(nameController, slider.value);
    }

    private void Update()
    {
        if (slider.value != 0f)
            audioMixer.SetFloat(nameController, Mathf.Log10(slider.value) * 20);
        else
            audioMixer.SetFloat(nameController, -80);

    }
}