Shader "Custom/GradShader" {
	Properties{
		_ColorA("ColorA", Color) = (1,1,1,1)
		_ColorB("ColorB", Color) = (1,1,1,1)
		_ColorAStart("ColorA Start", Range(0,1)) = 0
		_ColorBStart("ColorB Start", Range(0, 1)) = 1
		_BlendMidpoint("Color Midpoint", Range(0, 1)) = 0.5
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _ColorA;
		fixed4 _ColorB;
		half _ColorAStart;
		half _ColorBStart; 
		half _BlendMidpoint; 

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float _point = IN.uv_MainTex.y; 
			float _range =  1 / (_ColorBStart - _ColorAStart); 
			float _value = clamp((_point - _ColorAStart) * _range, 0.0, 1.0); 
			fixed4 cBlend = _value * _ColorB + ((1 - _value) * _ColorA);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * cBlend;
			o.Alpha = c.a;
			o.Albedo = c; 
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
