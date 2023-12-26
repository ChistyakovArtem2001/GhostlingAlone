Shader "Custom/UnlitRevealingLightShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _LightDirection("Light Direction", Vector) = (0,0,1,0)
		_LightPosition("Light Position", Vector) = (0,0,0,0)
		_LightAngle("Light Angle", Range(0,180)) = 45
		_StrengthFactor("Strength", Float) = 50
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
		    float4 _LightPosition;
		    float4 _LightDirection;
		    float  _LightAngle;
		    float  _StrengthFactor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                float3 Dir = normalize(_LightPosition - i.worldPos);
			    float  Scale = dot(Dir, _LightDirection);
			    float  Strength = Scale - cos(_LightAngle * (3.14 / 360.0));
    Strength = min(max(Strength * _StrengthFactor, 0), 1);
                
                return col * Strength;
            }
            ENDCG
        }
    }
    Fallback "Unlit/Transparent"
}