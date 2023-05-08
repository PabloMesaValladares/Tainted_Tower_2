using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public string Music;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlaySound(Music, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SoundManager.instance.currentBGM != Music)
        {
            SoundManager.instance.FadeInFadeOut(Music, time);
        }
    }
}
