Shader "Custom/StylizedWater"
{
    Properties
    {
        [Header(Colors)]
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        [HDR]_FogColor("Fog Color", Color) = (1,1,1,1)
        [HDR]_IntersectionColor("Intersection color", Color) = (1,1,1,1)

        [Header(Thresholds)]
        _IntersectionThreshold("Intersction threshold", float) = 0
        _FogThreshold("Fog threshold", float) = 0
        _FoamThreshold("Foam threshold", float) = 0

        [Header(Normal maps)]
        [Normal]_NormalA("Normal A", 2D) = "bump" {} 
        [Normal]_NormalB("Normal B", 2D) = "bump" {}
        _NormalStrength("Normal strength", float) = 1
        _NormalPanningSpeeds("Normal panning speeds", Vector) = (0,0,0,0)

        [Header(Foam)]
        _FoamTexture("Foam texture", 2D) = "white" {} 
        _FoamTextureSpeedX("Foam texture speed X", float) = 0
        _FoamTextureSpeedY("Foam texture speed Y", float) = 0
        _FoamLinesSpeed("Foam lines speed", float) = 0
        _FoamIntensity("Foam intensity", float) = 1

        [Header(Misc)]
        _RenderTexture("Render texture", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _FresnelPower("Fresnel power", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:premul

   		#pragma target 3.0


        struct Input
        {
            float4 screenPos;
            float3 worldPos;
            float3 viewDir;
        };

        fixed4 _Color;
        fixed4 _FogColor;
        fixed4 _IntersectionColor;

        float _IntersectionThreshold;
        float _FogThreshold;
        float _FoamThreshold;

        sampler2D _NormalA;
        sampler2D _NormalB;
        float4 _NormalA_ST;
        float4 _NormalB_ST;
        float _NormalStrength;
        float4 _NormalPanningSpeeds;

        sampler2D _FoamTexture;
        float4 _FoamTexture_ST;
        float _FoamTextureSpeedX;
        float _FoamTextureSpeedY;
        float _FoamLinesSpeed;
        float _FoamIntensity;

        sampler2D _RenderTexture;
        half _Glossiness;
        float _FresnelPower;

        sampler2D _CameraDepthTexture;
        float3 _CamPosition;
        float _OrthographicCamSize;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 rtUV = IN.worldPos.xz - _CamPosition.xz;
            rtUV = rtUV/(_OrthographicCamSize *2);
            rtUV += 0.5;
            fixed4 rt = tex2D(_RenderTexture, rtUV);

            float depth = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
            depth = LinearEyeDepth(depth);

            float fogDiff = saturate((depth - IN.screenPos.w) / _FogThreshold);
            float intersectionDiff = saturate((depth - IN.screenPos.w) / _IntersectionThreshold);
            float foamDiff = saturate((depth - IN.screenPos.w) / _FoamThreshold);
            foamDiff *= (1.0 - rt.b);
            
            fixed4 c = lerp(lerp(_IntersectionColor, _Color, intersectionDiff), _FogColor, fogDiff);
            
            float foamTex = tex2D(_FoamTexture, IN.worldPos.xz * _FoamTexture_ST.xy + _Time.y * float2(_FoamTextureSpeedX, _FoamTextureSpeedY));
            float foam = step(foamDiff - (saturate(sin((foamDiff - _Time.y * _FoamLinesSpeed) * 8 * UNITY_PI)) * (1.0 - foamDiff)), foamTex);

            float fresnel = pow(1.0 - saturate(dot(o.Normal, normalize(IN.viewDir))), _FresnelPower);

            o.Albedo = c.rgb;
            float3 normalA = UnpackNormalWithScale(tex2D(_NormalA, IN.worldPos.xz * _NormalA_ST.xy + _Time.y * _NormalPanningSpeeds.xy + rt.rg), _NormalStrength);
            float3 normalB = UnpackNormalWithScale(tex2D(_NormalB, IN.worldPos.xz * _NormalB_ST.xy + _Time.y * _NormalPanningSpeeds.zw + rt.rg), _NormalStrength);
            o.Normal = normalA + normalB;
            o.Smoothness = _Glossiness;
            o.Alpha = lerp(lerp(c.a * fresnel, 1.0, foam), _FogColor.a, fogDiff);
            o.Emission = foam * _FoamIntensity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
