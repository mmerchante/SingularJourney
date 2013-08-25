Shader "vrjam/BRDF (Selectable)" {
	
	Properties {
		_BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Displacement ("Displacement", Range(0.0, 1.0)) = 0.0
		_Velocity ("Velocity", Range(0.0, 1.0)) = .5
	}	

	SubShader { 
		Tags { "RenderType"="Transparent" }


		Blend One One
		Cull Front Lighting Off Fog { Mode Off }
		
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient approxview vertex:myvert

		sampler2D _BRDFTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Displacement;
		half _Velocity;
		half4 _Color;

	    void myvert (inout appdata_full v, out Input data) {
			half t = _Velocity * _Time.a * 4.0;

			half3 displ = half3(cos(v.vertex.y * 15.0 + t), sin(v.vertex.x * 15.0 + t) * .2, cos(v.vertex.z * 15.0 + t));
			displ += v.vertex.rgb * 2.0;
			v.vertex += half4(displ, 0.0) * _Displacement * .05;

			UNITY_INITIALIZE_OUTPUT(Input,data);
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
			c.rgb = l.rgb * 2.0 * _LightColor0.rgb * s.Albedo * atten;
			c.a = 0;
			
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = _Color.rgb;
		}
		ENDCG
	}

	FallBack "Mobile/Diffuse"
}
