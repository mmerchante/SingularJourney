Shader "Glitch/Glitch1" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TintColor ("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_DisplVector ("Displacement Vector", Color) = (0.0, 0.0, 0.0, 0.0)
	}
	
	SubShader 
	{
		Tags { "Queue" = "Transparent" }
	
		Pass 
		{
			Blend One One
			Cull Off Lighting Off Fog { Mode Off }
			ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			
			half4 _MainTex_ST;
			half4 _TintColor;
			half4 _DisplVector;
			 
			struct v2f {
			    half4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};
					 
			v2f vert (appdata_full v) 
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex) + _DisplVector;
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				return o;
			}
			
			half4 frag (v2f i) : COLOR 
			{
				return _TintColor;
			}
			
			ENDCG
		} 
	} 
	FallBack "Particles/Additive"
}
