Shader "Custom/2_Surface_Texture"
{

	Properties
	{

		_Color ("ColorWa", Color) = (1, 1, 1, 1)
		_MainTex ("TextureWa", 2D) = "white" {} // for texture
		_TexRange ("Texture Range", Range(0,5)) = 0.5

	}

		SubShader
		{
			CGPROGRAM

			#pragma surface surf Lambert

			struct Input
			{
				float2 uv_MainTex;
			};

			fixed4 _Color;
			sampler2D _MainTex;
			float _TexRange;

			void surf(Input IN, inout SurfaceOutput o)
			{
				o.Albedo = (tex2D(_MainTex, IN.uv_MainTex) * _TexRange * _Color).rgb;
			}

			ENDCG

		}


	Fallback "Diffuse"
}
