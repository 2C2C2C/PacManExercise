Shader "MuyShader/MuySimpleFlip"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    SubShader {

        Tags { "RenderType" = "Opaque" }

        Cull Front // wat is this mean

        CGPROGRAM

        #pragma surface surf Lambert 
        #pragma vertex vert
        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float4 color : COLOR;
        };


        void vert(inout appdata_full v)
        {
            v.normal.xyz = v.normal * -1;
            // v.normal.xyz = v.normal * 1;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed3 result = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = result.rgb;
            o.Alpha = 1;
        }

        ENDCG

    }

    // if failed, use default_diffuse
    Fallback "Diffuse"
}
