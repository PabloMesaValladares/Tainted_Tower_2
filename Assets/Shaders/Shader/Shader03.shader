Shader "IFP/Shader03"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
		Cantidad("Cantidad", Float) = 0
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
            };

            sampler2D _MainTex;
            sampler2D _Mask;
            float Cantidad;
			float Intensidad;
            
            VSOutput VShader(VSInput i)
            {
                VSOutput o;
                
                float4 positionO = i.position;
				
				float4 positionO2 = positionO;
				positionO2.y += sin(positionO.z * Cantidad);
				
				positionO = lerp(positionO, positionO2, Intensidad);
                
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

                return texColor;
            }
            
            ENDCG
        }
    }
}