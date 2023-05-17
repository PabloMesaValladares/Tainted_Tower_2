Shader "IFP/Pixelization"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Pixelization("Pixelization", Range(1, 100)) = 10

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
                float _Pixelization;

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
                    float2 pixelUv = i.uv * _Pixelization;
                    pixelUv = floor(pixelUv) / _Pixelization;
                    return tex2D(_MainTex, pixelUv);
                }

                ENDCG
            }
        }
}