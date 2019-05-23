﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MuyShader/OutlineA"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 0)
        _OutlineWid("Outline width",Range(0.0, 5.0)) = 0.1
    }

    SubShader
    {
        //
        Tags { "Queue" = "Transparent+1" }

        Pass // render the outline
        {
            Zwrite Off
            // Cull Off

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            float _OutlineWid;
            float4 _OutlineColor;


            v2f vert(appdata v)
            {
                v2f o;

                //// do not do this
                // v.vertex.xyz *= _OutlineWid;

                v.vertex.xyz += v.normal * _OutlineWid;
                // v.vertex.xyz += v.color * _OutlineWid;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _OutlineColor;
                //// wtf are these
                // float3 vnormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                // float2 offset = TransformViewToProjection(vnormal.xy);
                // o.pos.xy += offset * _OutlineWid;

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return i.color;
            }

            ENDCG
        }

        // Pass // render the outline
        // {
        //     Zwrite Off
        //     // Cull Off

        //     CGPROGRAM

        //     #include "UnityCG.cginc"

        //     #pragma vertex vert
        //     #pragma fragment frag

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float4 color : COLOR;
        //         float3 normal : NORMAL;
        //     };

        //     struct v2f
        //     {
        //         float4 pos : POSITION;
        //         float3 normal : NORMAL;
        //         float4 color : COLOR;
        //     };

        //     float _OutlineWid;
        //     float4 _OutlineColor;


        //     v2f vert(appdata v)
        //     {
        //         v2f o;

        //         //// do not do this
        //         // v.vertex.xyz *= _OutlineWid;

        //         v.vertex.xyz -= v.color * _OutlineWid;
        //         o.pos = UnityObjectToClipPos(v.vertex);
        //         o.color = _OutlineColor;
        //         //// wtf are these
        //         // float3 vnormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        //         // float2 offset = TransformViewToProjection(vnormal.xy);
        //         // o.pos.xy += offset * _OutlineWid;

        //         return o;
        //     }

        //     half4 frag(v2f i) : SV_Target
        //     {
        //         return i.color;
        //     }

        //     ENDCG
        // }



        // draw default obj
        Pass
        {
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // // make fog work
            // #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                // UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

    }

    FallBack "Diffuse"
}