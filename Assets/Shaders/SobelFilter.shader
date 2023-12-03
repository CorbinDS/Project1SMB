Shader "Unlit/SobelFilter"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                int3x3 Kx = int3x3(1, 0, -1, 2, 0, -2, 1, 0, -1);
                int3x3 Ky = int3x3(1, 2, 1, 0, 0, 0, -1, -2, -1);

                float Gx = 0.0;
                float Gy = 0.0;

                for(int x = -1; x <= 1; x++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        float2 uv = i.uv + _MainTex_TexelSize.xy * float2(x, y);
                        float l = tex2D(_MainTex, uv).r; // Assuming grayscale; change to .a if alpha channel is used
                        Gx += Kx[x+1][y+1] * l;
                        Gy += Ky[x+1][y+1] * l;
                    }
                }

                float Mag = sqrt(Gx * Gx + Gy * Gy);
                
                return float4(Mag, Mag, Mag, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
