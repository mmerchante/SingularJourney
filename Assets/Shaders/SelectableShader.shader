Shader "vrjam/Selectable" {
	Properties 
	{
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Displacement ("Displacement", Range(0.0, 1.0)) = 0.0
		_Velocity ("Velocity", Range(0.0, 1.0)) = .5
	}
	
	SubShader 
	{
		Tags { "RenderType" = "Transparent" }
	
		Pass 
		{
			Blend One One
			Cull Front Lighting Off Fog { Mode Off }
	//		ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			half _Displacement;
			half _Velocity;
			half4 _Color;

			struct v2f {
			    half4 pos : SV_POSITION;
			};
					 
			v2f vert (appdata_full v) 
			{
				half t = _Velocity * _Time.a * 4.0;

				half3 displ = half3(cos(v.vertex.y * 15.0 + t), sin(v.vertex.x * 15.0 + t) * .2, cos(v.vertex.z * 15.0 + t));
				displ += v.vertex.rgb * 2.0;
				v.vertex += half4(displ, 0.0) * _Displacement * .05;


			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);	
				return o;
			}
			
			half4 frag (v2f i) : COLOR 
			{
				return _Color;
			}
			
			ENDCG
		} 
	} 
	FallBack "Particles/Additive"
}
