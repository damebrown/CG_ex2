﻿Shader "CG/BlinnPhongGouraud"
{
    Properties
    {
        _DiffuseColor("Diffuse Color", Color) = (0.14, 0.43, 0.84, 1)
        _SpecularColor("Specular Color", Color) = (0.7, 0.7, 0.7, 1)
        _AmbientColor("Ambient Color", Color) = (0.05, 0.13, 0.25, 1)
        _Shininess("Shininess", Range(0.1, 50)) = 10
    }
        SubShader
    {
        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

        // From UnityCG
        uniform fixed4 _LightColor0;

    // Declare used properties
    uniform fixed4 _DiffuseColor;
    uniform fixed4 _SpecularColor;
    uniform fixed4 _AmbientColor;
    uniform float _Shininess;

    struct appdata
    {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        fixed4 color : COLOR;
    };


    v2f vert(appdata input)
    {
        v2f output;
        output.pos = UnityObjectToClipPos(input.vertex);
        fixed4 Cd = _DiffuseColor * max(dot(input.normal, _WorldSpaceLightPos0.xyz), 0) * _LightColor0;
        float3 v = _WorldSpaceCameraPos - input.vertex;
        float3 h = (_WorldSpaceLightPos0.xyz + v) / 2;
        h = normalize(h);
        fixed4 Cs = pow(max(dot(input.normal, h),0), _Shininess) * _SpecularColor * _LightColor0;
        fixed4 Ca = _AmbientColor * _LightColor0;
        output.color = Cd + Cs + Ca;
        return output;
    }


    fixed4 frag(v2f input) : SV_Target
    {
        //return fixed4(0, 0, 1.0, 1.0);
        return input.color;
    //return _LightColor0;

}

ENDCG
}
    }
}
