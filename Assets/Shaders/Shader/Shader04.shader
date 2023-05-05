Shader "IFP/Shader04"
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
                
				float4 _Offset = float4(i.position.x * Cantidad, i.position.y * Cantidad, 0, 1);
				
				float4 maskValue = tex2Dlod(_Mask, float4(i.uv, 0, 0));
				
				float4 newI = (1,1,1,1);
				
				newI.xyz = i.position.xyz + maskValue.x * _Offset.xyz;
				
				i.position = lerp(i.position, newI, Intensidad);
				
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

                return texColor;
            }
            
            ENDCG
        }
    }
}