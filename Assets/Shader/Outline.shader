Shader "Custom/OutlineShader"
{
    Properties
    {
        // Width of the outline in pixels
        _OutlineWidth("Outline Width", Range(0, 1)) = 0.01

        // Color of the outline
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        // Use a simple unlit shader for the outline effect
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            // Set the width and color of the outline
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.xy += _OutlineWidth * o.vertex.w * (float2(o.vertex.x, o.vertex.y) * _ScreenParams.xy);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
