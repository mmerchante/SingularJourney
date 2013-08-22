Shader "vrjam/Fog Environment Shader" {
	
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_Scale ("Scale", Range(0.0, 1.0)) = 1.0
	}	

	SubShader { 
		Tags { "RenderType"="Overlay" }

		ZWrite Off

		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf EnviroFog nolightmap noambient

	    uniform half4 unity_FogColor;

		inline half4 LightingEnviroFog (SurfaceOutput s, float3 lightDir, float3 viewDir, half atten)
		{
			half4 c = atten * s.Albedo.x;
			return unity_FogColor * c * _LightColor0 * 5.0;
		}

		sampler2D _MainTex;
		sampler2D _NoiseTex;

		half _Scale;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 noise = tex2D (_NoiseTex, (IN.worldPos.xz + IN.worldPos.yz) * .1 * _Scale + half2(_Time.x, _Time.y) * _Scale);
			
			noise += tex2D (_NoiseTex, (IN.worldPos.xz + IN.worldPos.yz) * .025 * _Scale) - .5;
			noise *= tex2D(_NoiseTex, (noise.xy + noise.yz) + (_CosTime.x * .5 + .5) ) * .2;	

			half4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.a * tex.a * (noise.x * 4.0 + noise.y * 2.0 + noise.z) * .2;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
