using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCheck : MonoBehaviour
{

    public VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (video.isPlaying)
            gameObject.SetActive(false);
    }
}
