Shader "MuyShader/Dissolve"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        // 
        _DissovleTex ("Dissolve Texture", 2D) = "white" {}
        _DissolveY("current Y of the dissolve effect?",float)=0
        _DissolveSize("size of effect",float)=2 // meter
        _StartingY("starting point of effecy",float)=-10
    }
    SubShader
    {
        // tags is a dictionary
        Tags { "RenderType"="Opaque" }
        // far from 100m, it will not render?
        LOD 100

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
            sampler2D _DissovleTex;
            float _DissolveY;
            float _DissolveSize;
            float _StartingY;

 
            // will be call x times, x == vertex_count of your object every single frame
            // vertex shader, to get v2f, and send v2f to frag() func
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // add
                o.worldPos=mul(unity_ObjectToWorld,v.vertex).xyz;

                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


            // draw the actual pixel on tooop
            // pixel shader
            fixed4 frag (v2f i) : SV_Target
            {
                // func to do
                float transition = _DissolveY - i.worldPos.y;
                clip(_StartingY + (transition + (tex2D(_DissovleTex,i.uv)) * _DissolveSize));

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //
                // clip(1-(i.vertex.x%2));
                // // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
