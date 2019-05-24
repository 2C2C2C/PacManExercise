Shader "Custom/BlendTexSimple"
{
    Properties
    { 
        _MainTex ("Main Texture", 2D)= "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "white" {}
        _LerpValue ("BlendValue",Range(0,1)) = 0.5
        [Toggle] _AutoAnim("Auto Anim", Float) = 0
    }

    SubShader
    {
        Tags {"RenderType" = "Opaque"}
        LOD 100

        // code will do
        // can has multiple pass, they exc in a order(up to down)
        Pass
        {
            CGPROGRAM
            // #pragma 'keyword' 'func_name'
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            // #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                // POSITION is the pos of this vertex in 3d world
                float4 vertex : POSITION;
                // wat u define the uv for dat vertex
                float2 uv : TEXCOORD0;
            };

            // v2f == vertex to fragment
            struct v2f
            {
                float2 uv : TEXCOORD0;
                // UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                
                
            };

            // 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            float4 __SecondaryTex_ST;
            half _LerpValue;
            float _AutoAnim;

            
            // will be call x times, x == vertex_count of your object every single frame
            // vertex shader, to get v2f, and send v2f to frag() func
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            } 


            // draw the actual pixel on tooop
            // pixel shader
            fixed4 frag (v2f i) : SV_Target
            {
                // add
                fixed4 col = tex2D(_MainTex, i.uv); // idk how to init it
 
                // sample the texture
                if(_AutoAnim>0.0)
                {
                    col = lerp(tex2D(_MainTex, i.uv),tex2D(_SecondaryTex, i.uv),abs(_CosTime.z));
                }
                else
                {
                    col = lerp(tex2D(_MainTex, i.uv),tex2D(_SecondaryTex, i.uv),_LerpValue);
                }
                
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

    }
    
    FallBack "Diffuse"
}
