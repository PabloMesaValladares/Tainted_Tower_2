using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.PostProcessing;

public class llamarEffect : MonoBehaviour
{
    public PixelizeFeature pixelFeature;
    public RenderPipelineGlobalSettings _hiugh2;
    public RenderPipelineAsset _high;
    public GameObject highGraphics;
    // Start is called before the first frame update

    public void DesactivateBro()
    {
        //highGraphics.GetComponent<PixelizeFeature>().SetActive(false);

        pixelFeature.SetActive(false);
        
    }
}
