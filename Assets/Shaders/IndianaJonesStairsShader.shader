Shader "vrjam/Indiana Jones Stairs" {
	
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_BumpTex ("Bump", 2D) = "bump" {}
		_BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
		_Scale ("Pattern scale", Range (.01, 1.0)) = .25
		_Mask ("Mask", Range (0.0, 1.0)) = .5
	}	

	SubShader { 
		Tags { "RenderType"="Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient vertex:myvert
		#pragma glsl 3.0

		sampler2D _BRDFTex;

		half _Mask;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			half2 mask;
		};


	    void myvert (inout appdata_full v, out Input data) {
	      UNITY_INITIALIZE_OUTPUT(Input,data);
	      data.mask.x = v.vertex.x + 2.0;
	      data.mask.y = v.vertex.z;
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
			c.rgb = l.rgb * 2.0 * _LightColor0.rgb * s.Albedo * atten;
			c.a = s.Alpha;
			
			return c;
		}

		sampler2D _MainTex;
		sampler2D _BumpTex;
		sampler2D _NoiseTex;

		float _Scale;

		void surf (Input IN, inout SurfaceOutput o) {
			half4 noise = tex2D (_NoiseTex, (IN.worldPos.xz + IN.worldPos.y) * _Scale + _Time.x * .025);
			noise = tex2D(_BumpTex, noise.xy + _Time.x * .025);

			half3 bump = normalize(noise);

			half4 tex = tex2D(_MainTex, IN.uv_MainTex) * length(noise);
			o.Albedo = tex.rgb;
			o.Alpha = step(_Mask + cos(IN.mask.y * 10.0 + _Time.a) * .05 + .05, IN.mask.x);
			o.Normal = bump;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
