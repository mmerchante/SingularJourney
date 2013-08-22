Shader "vrjam/Wind Shader (Overlay)" {
	
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_Speckles ("Speckles", 2D) = "black" {}
		_Velocity ("Velocity", Range(0.0, 2.0)) = 1.0
		_Intensity ("Intensity", Range(0.0, 2.0)) = 1.0
	}	

	SubShader 
	{
		Tags { "Queue" = "Transparent" }
	
		Pass 
		{
		//	Blend One One
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off Lighting Off Fog { Mode Off }
			ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			sampler2D _Speckles;
			
			half4 _MainTex_ST;

			half _Velocity;
			half _Intensity;

	  		uniform half4 unity_FogColor;

			struct v2f {
			    half4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};
					 
			v2f vert (appdata_full v) 
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);		
				return o;
			}
			
			half4 frag (v2f i) : COLOR 
			{
				half2 uv = i.uv + half2(_Time.y * _Velocity + cos(i.uv.x) * .5, i.uv.y + cos(i.uv.x * 4.0 + _Time.a * _Velocity) * .1 + .5);

				half speckles = tex2D(_Speckles, uv).x;

				uv += tex2D(_NoiseTex, i.uv * .5 + uv ) * .25;

				half4 c;
				c.rgb = tex2D(_NoiseTex, uv).x * unity_FogColor + speckles;
				c.a = tex2D(_MainTex, i.uv).r * c.r;	
				return  c * _Intensity;
			}
			
			ENDCG
		} 
	} 

	FallBack "Particles/Additive"
}
