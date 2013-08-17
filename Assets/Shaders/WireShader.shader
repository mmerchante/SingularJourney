Shader "WireShader" {
	
	Properties {
		_CoilMask ("Coil Mask", 2D) = "grey" {}
		_BaseBRDF ("Base NdotL NdotH", 2D) = "white" {}
		_CoilBRDF ("Coil NdotL NdotH", 2D) = "white" {}
		_PowerMask ("Power Mask", Range (0.0, 1.0)) = .75
	}	

	SubShader { 
		Tags { "RenderType"="Opaque" }
		LOD 400
		
		CGPROGRAM
		#pragma surface surf PseudoBRDF nolightmap noambient approxview

		sampler2D _BaseBRDF;
		sampler2D _CoilBRDF;

		inline fixed4 LightingPseudoBRDF (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
		{
			// N.L
			fixed NdotL = dot (s.Normal, lightDir);
						
			// N.E
			fixed NdotE = dot (s.Normal, viewDir);
			
			fixed biasNdotL = NdotL * 0.5 + 0.5;
			
			fixed4 base = tex2D (_BaseBRDF, fixed2(biasNdotL, NdotE));
			fixed4 coil = tex2D (_CoilBRDF, fixed2(biasNdotL, NdotE));

			fixed4 l = lerp(base, coil, s.Specular.r * s.Albedo);

			fixed4 c;
			c.rgb = l.rgb * 2.0 * _LightColor0.rgb * atten * saturate(s.Albedo + .25);
			c.a = 0;
			
			return c;
		}

		sampler2D _CoilMask;

		half _PowerMask;

		struct Input {
			float2 uv_CoilMask;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_CoilMask, IN.uv_CoilMask);

			fixed mask = smoothstep(_PowerMask * .75, _PowerMask * 1.25, IN.uv_CoilMask.y);

			mask = lerp(tex.r * mask, mask * (1.0 + cos(_Time.a * 2.0) * .25), mask);

			o.Albedo = mask;
			o.Specular = tex.rgb;
		}
		ENDCG
	}

	FallBack "Mobile/Diffuse"
}
