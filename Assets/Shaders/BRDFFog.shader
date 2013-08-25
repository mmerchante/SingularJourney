Shader "vrjam/BRDF (Fog)" {
	
	Properties {
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
	}	

	SubShader { 
		Tags { "RenderType"="Opaque" }
		LOD 400
		
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient approxview vertex:myvert

		sampler2D _BRDFTex;
		sampler2D _MainTex;

	    uniform half4 unity_FogColor;

		struct Input {
			float2 uv_MainTex;
			half fog;
		};

	    void myvert (inout appdata_full v, out Input data) {
	      UNITY_INITIALIZE_OUTPUT(Input,data);
	      float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
	      data.fog = saturate((100.0 - pos) * 0.01);
	    }

		inline fixed4 LightingPseudoBRDF (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
		{
			// N.L
			fixed NdotL = dot (s.Normal, lightDir);
						
			// N.E
			fixed NdotE = dot (s.Normal, viewDir);
			
			fixed biasNdotL = NdotL * 0.5 + 0.5;
			
			fixed4 l = tex2D (_BRDFTex, fixed2(biasNdotL, NdotE));

			fixed4 c;
			c.rgb = l.rgb * 2.0 * s.Albedo;
			c.a = 0;
			
			return lerp(unity_FogColor, c, s.Specular) * atten * _LightColor0;
		}


		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb;
			o.Specular = IN.fog;	
		}
		ENDCG
	}

	FallBack "Mobile/Diffuse"
}
