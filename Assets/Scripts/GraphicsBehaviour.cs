using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsBehaviour : MonoBehaviour
{
    [SerializeField]
<<<<<<< Updated upstream:Assets/Scripts/GraphicsBehaviour.cs
    private TMP_Dropdown screenOptions, frameOptions, vsyncOptions, qualityOptions, resolutionOptions, shadowOptions, antialiasingOptions, particleOptions;
=======
    private TMP_Dropdown screenOptions, frameOptions, qualityOptions, resolutionOptions;

    public RenderPipelineAsset[] qualityLevels;

    public Camera cam;

>>>>>>> Stashed changes:Assets/Pablo/Scripts/GraphicsBehaviour.cs
    [SerializeField]
    private int frameCap, vSync, aliasing, disParticle;
    // Start is called before the first frame update
    void Awake()
    {
<<<<<<< Updated upstream:Assets/Scripts/GraphicsBehaviour.cs
        QualitySettings.vSyncCount = 0;
=======
       qualityOptions.value = QualitySettings.GetQualityLevel();
        cam = Camera.main;
>>>>>>> Stashed changes:Assets/Pablo/Scripts/GraphicsBehaviour.cs
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
<<<<<<< Updated upstream:Assets/Scripts/GraphicsBehaviour.cs
            QualitySettings.SetQualityLevel(2);
            QualitySettings.masterTextureLimit = 2;
            QualitySettings.skinWeights = SkinWeights.TwoBones;
            QualitySettings.lodBias = 0;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
=======
            Debug.Log("lOW");
            QualitySettings.SetQualityLevel(0);
            QualitySettings.masterTextureLimit = 3;
            QualitySettings.skinWeights = SkinWeights.TwoBones;
            cam.farClipPlane = 100;
>>>>>>> Stashed changes:Assets/Pablo/Scripts/GraphicsBehaviour.cs
        }
        else if (qualityOptions.value == 1)
        {
            Debug.Log("MED");
            QualitySettings.SetQualityLevel(1);
            QualitySettings.masterTextureLimit = 1;
            QualitySettings.skinWeights = SkinWeights.FourBones;
<<<<<<< Updated upstream:Assets/Scripts/GraphicsBehaviour.cs
            QualitySettings.lodBias = 1;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
        else if (qualityOptions.value == 2)
        {
            QualitySettings.SetQualityLevel(0);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.skinWeights = SkinWeights.Unlimited;
            QualitySettings.lodBias = 2;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
=======
            cam.farClipPlane = 200;
        }
        else if (qualityOptions.value == 2)
        {
            Debug.Log("HIG");
            QualitySettings.SetQualityLevel(2);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.skinWeights = SkinWeights.Unlimited;
            cam.farClipPlane = 300;
>>>>>>> Stashed changes:Assets/Pablo/Scripts/GraphicsBehaviour.cs
        }
    }

    public void ChangeResolution()
    {
        if (resolutionOptions.value == 0)
        {
            Screen.SetResolution(320, 240, false);
        }
        else if (resolutionOptions.value == 1)
        {
            Screen.SetResolution(640, 480, false);
        }
        else if (resolutionOptions.value == 2)
        {
            Screen.SetResolution(854, 480, false);
        }
        else if (resolutionOptions.value == 3)
        {
            Screen.SetResolution(800, 600, false);
        }
        else if (resolutionOptions.value == 4)
        {
            Screen.SetResolution(960, 540, false);
        }
        else if (resolutionOptions.value == 5)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if (resolutionOptions.value == 6)
        {
            Screen.SetResolution(1400, 1050, false);
        }
        else if (resolutionOptions.value == 7)
        {
            Screen.SetResolution(1600, 1200, false);
        }
        else if (resolutionOptions.value == 8)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (resolutionOptions.value == 9)
        {
            Screen.SetResolution(1920, 1200, false);
        }
        else if (resolutionOptions.value == 10)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else if (resolutionOptions.value == 11)
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

    public void ChangeVsync()
    {
        if (vsyncOptions.value == 0)
        {
            vSync = 0;
        }
        else if (vsyncOptions.value == 1)
        {
            vSync = 1;
        }
        else if (vsyncOptions.value == 2)
        {
            vSync = 2;
        }
        
        QualitySettings.vSyncCount = vSync;
    }

    public void ChangeAliasing()
    {
        if (antialiasingOptions.value == 0)
        {
            aliasing = 0;
        }
        else if (antialiasingOptions.value == 1)
        {
            aliasing = 2;
        }
        else if (antialiasingOptions.value == 2)
        {
            aliasing = 4;
        }
        else if (antialiasingOptions.value == 3)
        {
            aliasing = 8;
        }

        QualitySettings.antiAliasing = aliasing;

    }

    public void ChangeParticle()
    {
        if (particleOptions.value == 0)
        {
            disParticle = 1;
            QualitySettings.particleRaycastBudget = 0;
            QualitySettings.softParticles = false;

        }
        else if (particleOptions.value == 1)
        {
            disParticle = 0;
            QualitySettings.particleRaycastBudget = 16;
            QualitySettings.softParticles = false;
        }
        else if (particleOptions.value == 2)
        {
            disParticle = 0;
            QualitySettings.particleRaycastBudget = 256;
            QualitySettings.softParticles = true;
        }
        else if (particleOptions.value == 2)
        {
            disParticle = 0;
            QualitySettings.particleRaycastBudget = 4096;
            QualitySettings.softParticles = true;
        }

        QualitySettings.antiAliasing = aliasing;

    }

    //Sombras, Resolucion, particulas, anti aliasing, skin Weights + Lod Bias
}
