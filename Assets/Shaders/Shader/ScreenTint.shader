Shader "CustomPost/ScreenTint"
{
    Properties
    {
        _MainTex("InputTex", 2D) = "white" {}
    }

     SubShader
     {
         Tags {"RenderType"="Opaque" "RenderPipeline" = "UniversalPipeLine"}

        Pass
        {
            HLSLPROGRAM

#pragma vertex vert
#prag fragment frag

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.h"

            Name "ScreenTint"
            
        struct appdata

    {

    };
            

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 col = tex2D(_MainTex, uv) * _OverlayColor;
                col.rgb *= _Intensity;
                return col;
            }
           
                ENDHLSL

        }
    }
}
