Shader "Stairs" {
	
	Properties {
		_MainTex ("Base", 2D) = "grey" {}
		_BumpTex ("Bump", 2D) = "bump" {}
		_BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
	}	

	SubShader { 
		Tags { "RenderType"="Opaque" }
		LOD 400
		
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient approxview

		sampler2D _BRDFTex;

		inline fixed4 LightingPseudoBRDF (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
		{
			// N.L
			fixed NdotL = dot (s.Normal, lightDir);
						
			// N.E
			fixed NdotE = dot (s.Normal, viewDir);
			
			fixed biasNdotL = NdotL * 0.5 + 0.5;
			
			fixed4 l = tex2D (_BRDFTex, fixed2(biasNdotL, NdotE));

			fixed4 c;
			c.rgb = l.rgb * 2.0 * _LightColor0.rgb * s.Albedo * atten;
			c.a = 0;
			
			return c;
		}

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb;
		}
		ENDCG
	}

	FallBack "Mobile/Diffuse"
}
