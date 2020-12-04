Shader "Unlit/WaterRipples"
{
    Properties
    {
        [Normal]_MainTex ("Texture", 2D) = "bump" {}
        _MaskRadiusOuter("Mask radius outer", Range(0,.5)) = 0
        _MaskRadiusInner("Mask radius inner", Range(0,.5)) = 0
        _MaskSoftnessOuter("Mask softness outer", Range(0,1)) = 0
        _MaskSoftnessInner("Mask softness inner", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Mask;
            float _MaskSoftnessOuter;
            float _MaskRadiusOuter;
            float _MaskRadiusInner;
            float _MaskSoftnessInner;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float circleSDF = distance(i.uv, float2(0.5,0.5));
                float outerMask = 1.0 - smoothstep(_MaskRadiusOuter, saturate(_MaskRadiusOuter + _MaskSoftnessOuter), circleSDF);
                float innerMask = smoothstep(_MaskRadiusInner, saturate(_MaskRadiusInner + _MaskSoftnessInner), circleSDF);
                float mask = outerMask * innerMask;
                fixed4 col = fixed4(UnpackNormal(tex2D(_MainTex, i.uv)), mask) * i.color;
                return col;
            }
            ENDCG
        }
    }
}
