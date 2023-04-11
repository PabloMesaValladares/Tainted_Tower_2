Shader "IFP/Shader05"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        MaxDistance("MaxDistance", float) = 0
        _Amount("Amount", Range(0.0,1.0)) = 0
        _Transparency("Transparency", Range(0.0,1.0)) = 0.25
        ColorTest("ColorTest", Color) = (0,0,0,0)

    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            AlphaTest Greater 0.1

            Pass
            {
                CGPROGRAM
                #pragma vertex VShader
                #pragma fragment FShader

                #include "UnityCG.cginc"

                struct VSInput
                {
                    float4 position : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct VSOutput
                {
                    float2 uv : TEXCOORD0;
                    float4 position : SV_POSITION;
                };

                sampler2D _MainTex;
                float distanceCP;
                float MaxDistance;
                float _Amount;
                float _Transparency;
                float4 ColorTest;


                VSOutput VShader(VSInput i)
                {
                    VSOutput o;

                    float4 positionO = i.position;

                    float4 positionW = mul(UNITY_MATRIX_M, positionO);

                    float4 positionC = mul(UNITY_MATRIX_V, positionW);

                    float4 positionS = mul(UNITY_MATRIX_P, positionC);


                    o.position = positionS;
                    o.uv = i.uv;

                    return o;
                }

                float4 FShader(VSOutput i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv);

                    fixed4 texColorHide = texColor;

                    fixed4 texColorBase = ColorTest;

                    texColorHide.a = _Transparency;

                    texColor.a = lerp(texColorBase, texColorHide, _Amount);


                    return texColor;
                }

                ENDCG
            }
        }
}