Shader "vrjam/Dust Particles" {
	
	Properties {
		_MainTex ("Main", 2D) = "white" {}
	}	

	SubShader 
	{
		Tags { "Queue" = "Transparent" }
	
		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off Lighting Off Fog { Mode Off }
			ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			
			half4 _MainTex_ST;

			struct v2f {
			    half4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				half4 color : COLOR0;
			};
					 
			v2f vert (appdata_full v) 
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				o.color = v.color;
				return o;
			}
			
			half4 frag (v2f i) : COLOR 
			{
				return i.color * tex2D(_MainTex, i.uv) * 2.0;
			}
			
			ENDCG
		} 
	} 

	FallBack "Particles/Additive"
}
