Shader "IFP/Matrix Invisible"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Spacing("Spacing", Range(0,1)) = 0.25
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _MainTex_TexelSize;
                float _Spacing;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 texelUV = i.uv * _MainTex_TexelSize.zw;

                    fixed4 col = tex2D(_MainTex, i.uv);

                    float2 pixelUV = frac(texelUV) * 2.0 - 1.0;

                    float pixelCenterDist = max(abs(pixelUV.x), abs(pixelUV.y));

                    clip(1 - _Spacing - pixelCenterDist);

                    return col;
                }
                ENDCG
            }
        }
}

/*
Shader "Custom/Pixelate" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", Range(1, 100)) = 10
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _PixelSize;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float2 pixelUv = i.uv * _PixelSize;
                    pixelUv = floor(pixelUv) / _PixelSize;
                    return tex2D(_MainTex, pixelUv);
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
*/