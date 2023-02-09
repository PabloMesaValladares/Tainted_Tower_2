using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class GraphicsBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown screenOptions, frameOptions, qualityOptions, resolutionOptions;

    public RenderPipelineAsset[] qualityLevels;

    [SerializeField]
    private int frameCap;
    // Start is called before the first frame update
    void Start()
    {
       qualityOptions.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScreen()
    {
        if (screenOptions.value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (screenOptions.value == 1)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else if (screenOptions.value == 2)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    public void ChangeQuality()
    {
        if (qualityOptions.value == 0)
        {
            QualitySettings.SetQualityLevel(0);
            QualitySettings.masterTextureLimit = 3;
            QualitySettings.skinWeights = SkinWeights.TwoBones;
        }
        else if (qualityOptions.value == 1)
        {
            QualitySettings.SetQualityLevel(1);
            QualitySettings.masterTextureLimit = 1;
            QualitySettings.skinWeights = SkinWeights.FourBones;
        }
        else if (qualityOptions.value == 2)
        {
            QualitySettings.SetQualityLevel(2);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.skinWeights = SkinWeights.Unlimited;
        }
    }

    public void ChangeResolution()
    {
        if (resolutionOptions.value == 0)
        {
            Screen.SetResolution(640, 480, false);
        }
        else if (resolutionOptions.value == 1)
        {
            Screen.SetResolution(800, 600, false);
        }
        else if (resolutionOptions.value == 2)
        {
            Screen.SetResolution(1024, 768, false);
        }
        else if (resolutionOptions.value == 3)
        {
            Screen.SetResolution(1128, 634, false);
        }
        else if (resolutionOptions.value == 4)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if (resolutionOptions.value == 5)
        {
            Screen.SetResolution(1280, 1024, false);
        }
        else if (resolutionOptions.value == 6)
        {
            Screen.SetResolution(1366, 768, false);
        }
        else if (resolutionOptions.value == 7)
        {
            Screen.SetResolution(1600, 900, false);
        }
        else if (resolutionOptions.value == 8)
        {
            Screen.SetResolution(1600, 1200, false);
        }
        else if (resolutionOptions.value == 9)
        {
            Screen.SetResolution(1680, 1050, false);
        }
        else if (resolutionOptions.value == 10)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (resolutionOptions.value == 11)
        {
            Screen.SetResolution(1920, 1200, false);
        }
        else if (resolutionOptions.value == 12)
        {
            Screen.SetResolution(2048, 1536, false);
        }
        else if (resolutionOptions.value == 13)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else if (resolutionOptions.value == 14)
        {
            Screen.SetResolution(3072, 1728, false);
        }
        else if (resolutionOptions.value == 15)
        {
            Screen.SetResolution(3200, 1800, false);
        }
        else if (resolutionOptions.value == 16)
        {
            Screen.SetResolution(3840, 2160, false);
        }
    }

    public void ChangeFrames()
    {
        if (frameOptions.value == 0)
        {
            frameCap = 30; 
        }
        else if (frameOptions.value == 1)
        {
            frameCap = 60;
        }
        else if (frameOptions.value == 2)
        {
            frameCap = 120;
        }
        else if (frameOptions.value == 3)
        {
            frameCap = 240;
        }
        else if (frameOptions.value == 3)
        {
            frameCap = -1; //esto quita el limite de frames.
        }

        Application.targetFrameRate = frameCap;
    }
}
