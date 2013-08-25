Shader "vrjam/Stairs (Fog, no anim)" {
	
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_BumpTex ("Bump", 2D) = "bump" {}
		_BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
		_Scale ("Pattern scale", Range (.01, 1.0)) = .25
	}	

	SubShader { 
		Tags { "RenderType"="Opaque" }
		 
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient vertex:myvert 
		#pragma glsl 3.0

		sampler2D _BRDFTex; 

	    uniform half4 unity_FogColor;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			half fog;
		};

	    void myvert (inout appdata_full v, out Input data) {
	      UNITY_INITIALIZE_OUTPUT(Input,data);
	      float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);

	      // float diff = unity_FogEnd.x - unity_FogStart.x;	// 500 - 100
	      // float invDiff = 1.0f / diff;

	      float invDiff = 0.04; 
	      data.fog = saturate((20.0 - pos) * invDiff);
	    }

		inline half4 LightingPseudoBRDF (SurfaceOutput s, float3 lightDir, float3 viewDir, half atten)
		{
			// N.L 
			half NdotL = dot (s.Normal, lightDir);
						
			// N.E
			half NdotE = dot (s.Normal, viewDir);
			
			half biasNdotL = NdotL * 0.5 + 0.5;
			
			half4 l = tex2D (_BRDFTex, half2(biasNdotL, NdotE));

			half4 c;
			c.rgb = l.rgb * 2.0 * s.Albedo;// + s.Specular * atten * 2.0 * unity_FogColor;
			c.a = 0;
			
			return lerp(unity_FogColor, c, s.Specular) * atten * _LightColor0;
		}

		sampler2D _MainTex;
		sampler2D _BumpTex;
		sampler2D _NoiseTex;

		float _Scale;

		void surf (Input IN, inout SurfaceOutput o) {
			half4 noise = tex2D (_NoiseTex, (IN.worldPos.xz + IN.worldPos.y) * _Scale);
			noise = tex2D(_BumpTex, noise.xy + _Time.x * .01);

			half3 bump = normalize(noise);

			half4 tex = tex2D(_MainTex, IN.uv_MainTex) * length(noise);
			o.Albedo = tex.rgb;// + IN.fog;
			o.Specular = IN.fog;
			o.Normal = bump;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
