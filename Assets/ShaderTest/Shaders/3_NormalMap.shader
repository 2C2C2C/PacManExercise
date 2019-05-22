Shader "Custom/3_NormalMap"
{
	Properties
	{
		//_Emission ("EmissionWa", Color) = (0, 0, 0, 0)
		//_Color ("ColorWa", Color) = (1, 1, 1, 1) // ("Name", Type) show on panel = default value
		_MainTex ("TextureWa", 2D) = "white" {}
		_NormalTex ("NormalWa", 2D) = "bump" {}
		_NormalIntensity ("Normal Intensity", Range(-5,5)) = 1

	}

		SubShader
		{
			CGPROGRAM

			// shader code here
			#pragma surface surf Lambert
			//
			// surf -> func name
			// Lambert lighting model

			struct Input
			{
				float2 uv_MainTex;
				float2 uv_NormalTex;
			};

			sampler2D _MainTex;
			sampler2D _NormalTex;
			half _NormalIntensity;

			void surf(Input IN, inout SurfaceOutput o)
			{
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				//o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
				o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
				o.Normal *= float3(_NormalIntensity, _NormalIntensity, 1);
			}

			ENDCG
		}

			Fallback "Diffuse"
}
