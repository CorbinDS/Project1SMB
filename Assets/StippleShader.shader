Shader "Custom/StippleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StippleTex ("Stipple Pattern", 2D) = "white" {}
        _NoiseTex("Noise Pattern", 2D) = "white" {}
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex, _CameraDepthTexture;
        sampler2D _NoiseTex;
        sampler2D _StippleTex;
        float4 _NoiseTex_TexelSize;
        float4 _MainTex_TexelSize;
        float _DarkRegionThreshold = 0.2;
        float4 _DarkColor;
        float4 _LightColor;

        struct VertexData {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
            float4 screenPosition : TEXCOORD1;
        };

        v2f vp(VertexData v) {
            v2f f;
            f.vertex = UnityObjectToClipPos(v.vertex);
            f.uv = v.uv;
            f.screenPosition = ComputeScreenPos(f.vertex);
            
            return f;
        }
    ENDCG


        Pass{
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment frag

            fixed4 frag(v2f i) : SV_Target {
                return LinearRgbToLuminance(tex2D(_MainTex, i.uv));
            }

            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment frag

          


    
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
                
                return float4(1-Mag, 1-Mag, 1-Mag, 1);
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment frag


            float4 frag (v2f i) : SV_Target
            {
                fixed4 mainTexColor = tex2D(_MainTex, i.uv);
                float luminance = tex2D(_MainTex, i.uv).r;

                float2 scaledUV = i.uv * 1;
                fixed noiseValue= tex2D(_NoiseTex, scaledUV).r;
                if (luminance < _DarkRegionThreshold){
                    if(luminance > 10*noiseValue){
                        return 1.0f;
                    } else {
                        return 0.0f;
                    }
                } else if(luminance > .6*noiseValue){
                    return 1.0f;
                } else {
                    return 0.0f;
                }

            }
            ENDCG
        }
        // Combination Pass
        Pass {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment frag


            float4 frag(v2f i) : SV_Target {
                float edge = tex2D(_MainTex, i.uv).r;
                float4 stipple = tex2D(_StippleTex, i.uv);
                float depth = 1 - Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                depth = min(1.0f, max(0.0f, depth));

                
                float finalEdge =edge;

            
    
                return float4(finalEdge, finalEdge, finalEdge, 1)* stipple;
            }

            ENDCG
        }
        //Color Pass
        Pass {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment frag

            float4 frag(v2f i) : SV_Target {
                bool black = tex2D(_MainTex, i.uv).r > 0.0f;
                return !black ? _DarkColor : _LightColor;
                
            }
            ENDCG
        }
    }
}