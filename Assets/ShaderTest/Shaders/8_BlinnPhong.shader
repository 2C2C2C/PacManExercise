Shader "Custom/8_Buffers"
{
	Properties
	{
		_Color("ColorWa", Color) = (1, 1, 1, 1)
		_SpecColor("Specular Color", Color) = (1, 1, 1, 1)
		_SpecularRange("Specular Range", Range(0,1)) = 0.0
		_SpecularIntensity("Specular Intensity", Range(0,1)) = 0.0

	}


	SubShader
	{
		Tags { "Queue" = "Geometry" }

		CGPROGRAM

		// shader code here
		#pragma surface surf BlinnPhong

		struct Input
		{
			float3 uv_MainTex;
		};

		float4 _Color;
		half _SpecularRange;
		fixed _SpecularIntensity;

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _Color.rgb;
			o.Specular = _SpecularRange;
			o.Gloss = _Color.rgb;
		}


		ENDCG
	}

		Fallback "Diffuse"
}
