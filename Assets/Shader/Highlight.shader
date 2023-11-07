Shader "Custom/HighlightShader"
{
    Properties
    {
        // Tint color for the highlighted objects
        _TintColor("Tint Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        // Use a simple unlit shader for the highlight effect
        Tags { "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            // Set the tint color for the objects
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform fixed4 _TintColor;

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
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _TintColor;
            }
            ENDCG
        }
    }
}
