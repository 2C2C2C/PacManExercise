Shader "Custom/1_Surface"
{
	Properties
	{
		_Color ("ColorWa", Color) = (1, 1, 1, 1) // ("Name", Type) show on panel = default value
		_Emission ("EmissionWa", Color) = (0, 0, 0, 0)
		//_MainTex

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
		};

		fixed4 _Color; // linked _Color in line 5
		fixed3 _Emission; // linked _Emission in line 6

		float4x4 _myMatrix;// row first

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _Color.rgb;
			//o.Albedo = _Color.brg;
			o.Emission = _Emission.rgb;
			//o.Albedo = _myMatrix._m11; // row first
			//o.Albedo = _myMatrix._m00_m01_m02 // row first
			//o.Albedo = _myMatrix[0] // ???
			//o.Alpha = _Color.a;
		}

		ENDCG
	}

	Fallback "Diffuse"
}
