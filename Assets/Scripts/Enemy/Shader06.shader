Shader "IFP/Shader06"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
		Limite ("Limite", Float) = 0
		Anchura ("Anchura", Float) = 0
		ColorRadar ("ColorRadar", Color) = (1,1,1,1)
		ColorFondo ("ColorFondo", Color) = (1,1,1,1)
		Intensidad ("Intensidad", Float) = 0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
				float4 radarColor : Color;
            };

            sampler2D _MainTex;
            sampler2D _Mask;
			float Limite;
			float Anchura;
			float4 ColorRadar;
			float4 ColorFondo;
			float Intensidad;
            
            VSOutput VShader(VSInput i)
            {
                VSOutput o;
                
                float4 positionO = i.position;
                
                float4 positionW = mul(UNITY_MATRIX_M, positionO);
                
                float4 positionC = mul(UNITY_MATRIX_V, positionW);
				
				float distWC = distance(i.position.xyz, positionC);
				
				o.radarColor = ColorFondo;
				
				if(distWC > Limite && distWC < Limite + Anchura)
				o.radarColor = ColorRadar;
				
                float4 positionS = mul(UNITY_MATRIX_P, positionC);
                
                o.position = positionS;
                o.uv = i.uv;
                
                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
				
				float4 c = lerp(texColor, i.radarColor, Intensidad);
				
                return c;
            }
            
            ENDCG
        }
    }
}