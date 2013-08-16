Shader "Simple/Color" {
	Properties 
	{
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	
	SubShader 
	{
		Pass 
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			half4 _Color;

			struct v2f {
			    half4 pos : SV_POSITION;
			};
					 
			v2f vert (appdata_full v) 
			{
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
	FallBack "Diffuse"
}
