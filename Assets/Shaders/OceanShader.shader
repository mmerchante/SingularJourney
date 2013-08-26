Shader "vrjam/Ocean (Fog)" {
	
	Properties {
		_Scale ("Ocean scale", Range (.01, 1.0)) = .25
		_Target ("Target", Vector) = (0,0,0,0) 
	}	

	SubShader { 
		Tags { "RenderType"="Opaque" }
		 
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient vertex:myvert 

	    uniform half4 unity_FogColor;

		struct Input {
			float2 uv_MainTex;
			half height;
			half fog;
		};

		float _Scale;
		float4 _Target;

	    void myvert (inout appdata_full v, out Input data) 
	    {
	    	data.height = v.vertex.y;

	    	float t = _Time.a * .5;

	    	float displ;

	    	displ = sin(length(_Target - v.vertex) - t) * _Scale;

			v.vertex.y += displ;

			UNITY_INITIALIZE_OUTPUT(Input,data);
			
			float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
			data.fog = saturate((20.0 - pos) * .04);
	    }

		inline half4 LightingPseudoBRDF (SurfaceOutput s, float3 lightDir, float3 viewDir, half atten)
		{
			half4 c = dot (s.Normal, lightDir) * .5 + .5;			
			return lerp(c, c * s.Albedo.x, s.Specular) * atten * _LightColor0;
		}


		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = IN.height * 1.5;
			o.Specular = IN.fog;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
