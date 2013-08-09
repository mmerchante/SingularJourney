Shader "Effects/Screen Texture" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader 
	{
	
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			
			half4 _MainTex_ST;

			struct v2f {
			    half4 pos : SV_POSITION;
			    float4 screenPos : TEXCOORD1;
				half2 uv : TEXCOORD0;
			};
					 
			v2f vert (appdata_full v) 
			{
			    v2f o;

			    o.pos =  mul (UNITY_MATRIX_MVP, v.vertex);
			    o.screenPos = o.pos;
				return o;
			}
			
			half4 frag (v2f i) : COLOR 
			{
				half2 uv = (i.screenPos.xy / i.screenPos.w) * .5 + .5;

				return tex2D(_MainTex, uv);
			}
			
			ENDCG
		} 
	} 
	FallBack "Particles/Additive"
}
