using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GraphicsBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown screenOptions, frameOptions, qualityOptions, resolutionOptions;

    [SerializeField]
    private bool Full;

    public RenderPipelineAsset[] qualityLevels;

    public Camera cam;
    public CinemachineBrain vcam;

    public List<int> reswidth;
    public List<int> resheight;

    [SerializeField]
    private int frameCap;

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            reswidth[resolutions.Length - 1 -  i] = resolutions[i].width;
            resheight[resolutions.Length - 1 - i] = resolutions[i].height;
        }

        qualityOptions.value = QualitySettings.GetQualityLevel();
        cam = Camera.main;

        resolutionOptions.options.Insert(0, new TMP_Dropdown.OptionData(reswidth[0] + " x " + resheight[0], null));
        resolutionOptions.options.Insert(1, new TMP_Dropdown.OptionData(reswidth[1] + " x " + resheight[1], null));
        resolutionOptions.options.Insert(2, new TMP_Dropdown.OptionData(reswidth[2] + " x " + resheight[2], null));
        resolutionOptions.options.Insert(3, new TMP_Dropdown.OptionData(reswidth[3] + " x " + resheight[3], null));
        resolutionOptions.options.Insert(4, new TMP_Dropdown.OptionData(reswidth[4] + " x " + resheight[4], null));
        resolutionOptions.options.Insert(5, new TMP_Dropdown.OptionData(reswidth[5] + " x " + resheight[5], null));
        resolutionOptions.options.Insert(6, new TMP_Dropdown.OptionData(reswidth[6] + " x " + resheight[6], null));
        resolutionOptions.options.Insert(7, new TMP_Dropdown.OptionData(reswidth[7] + " x " + resheight[7], null));
        resolutionOptions.options.Insert(8, new TMP_Dropdown.OptionData(reswidth[8] + " x " + resheight[8], null));
        resolutionOptions.options.Insert(9, new TMP_Dropdown.OptionData(reswidth[9] + " x " + resheight[9], null));

        ChangeResolution();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScreen()
    {
        if (screenOptions.value == 0)
        {
            Full = true;
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            ChangeResolution();
        }
        else if (screenOptions.value == 1)
        {
            Full = false;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            ChangeResolution();
        }
        else if (screenOptions.value == 2)
        {
            Full = false;
            Screen.fullScreenMode = FullScreenMode.Windowed;
            ChangeResolution();
        }
    }

    public void ChangeQuality()
    {
        if (qualityOptions.value == 0)
        {
            //high
            QualitySettings.SetQualityLevel(0);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.skinWeights = SkinWeights.FourBones;

            vcam.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = 300;
        }
        else if (qualityOptions.value == 1)
        {
            //medium
            QualitySettings.SetQualityLevel(1);
            QualitySettings.masterTextureLimit = 1;
            QualitySettings.skinWeights = SkinWeights.TwoBones;
            vcam.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = 200;
        }
        else if (qualityOptions.value == 2)
        {
            //low
            QualitySettings.SetQualityLevel(2);
            QualitySettings.masterTextureLimit = 3;
            QualitySettings.skinWeights = SkinWeights.OneBone;
            vcam.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = 100;
        }
    }

    public void ChangeResolution()
    {
        if(Full)
        {
            Screen.SetResolution(reswidth[0], resheight[0], true);   
        }
        else if(Full == false)
        {
            if (resolutionOptions.value == 0)
            {
                Screen.SetResolution(reswidth[0], resheight[0], false);
            }
            else if (resolutionOptions.value == 1)
            {
                Screen.SetResolution(reswidth[1], resheight[1], false);
            }
            else if (resolutionOptions.value == 2)
            {
                Screen.SetResolution(reswidth[2], resheight[2], false);
            }
            else if (resolutionOptions.value == 3)
            {
                Screen.SetResolution(reswidth[3], resheight[3], false);
            }
            else if (resolutionOptions.value == 4)
            {
                Screen.SetResolution(reswidth[4], resheight[4], false);
            }
            else if (resolutionOptions.value == 5)
            {
                Screen.SetResolution(reswidth[5], resheight[5], false);
            }
            else if (resolutionOptions.value == 6)
            {
                Screen.SetResolution(reswidth[6], resheight[6], false);
            }
            else if (resolutionOptions.value == 7)
            {
                Screen.SetResolution(reswidth[7], resheight[7], false);
            }
            else if (resolutionOptions.value == 8)
            {
                Screen.SetResolution(reswidth[8], resheight[8], false);
            }
            else if (resolutionOptions.value == 9)
            {
                Screen.SetResolution(reswidth[9], resheight[9], false);
            }
        }
    }

    public void ChangeFrames()
    {
        if (frameOptions.value == 0)
        {
            frameCap = 60; 
        }
        else if (frameOptions.value == 1)
        {
            frameCap = 120;
        }
        else if (frameOptions.value == 2)
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
