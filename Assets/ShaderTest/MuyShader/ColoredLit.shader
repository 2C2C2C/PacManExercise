Shader "Custom/ColoredLit"
{

	Properties
	{
		_Color("Main Color",Color) = (1,0.5,0.5,1)
	}


	SubShader
	{
		// a single pass
		// wat does a pass do?
		Pass
		{
			// use fixed func per-vertes lighting????
			Material
			{
				Diffuse[_Color]
			}
			Lighting ON
		}

	}
}
