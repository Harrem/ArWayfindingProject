// Shader "Custom/Unlit_Transparent_Distance" {
//     Properties {
//         _MainTex ("Texture", 2D) = "white" {}
//         _Color ("Color", Color) = (1,1,1,1)
//         _MinDistance ("Minimum Distance", Range(0, 100)) = 3
//         _MaxDistance ("Maximum Distance", Range(0, 100)) = 100
        
//     }
 
//     SubShader {
//         Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
//         LOD 100
//         // ZWrite Off
//         // ZTest Less
//         // Cull Back
//         Pass {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"
 
//             struct appdata {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };
 
//             struct v2f {
//                 float2 uv : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//                 float3 worldPos : TEXCOORD1;
//             };
 
//             sampler2D _MainTex;
//             float4 _Color;
//             float _MinDistance;
//             float _MaxDistance;
 
//             v2f vert (appdata v) {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;
//                 o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
//                 return o;
//             }
//             // fixed4 frag (v2f i) : SV_Target {
//             //     fixed4 tex = tex2D(_MainTex, i.uv);
//             //     fixed4 col = i.color * tex;
//             //     col.a *= tex.a;
//             //     return col;
//             // }
 
//             fixed4 frag (v2f i) : SV_Target {
//                 // Calculate distance from camera
//                 fixed4 tex = tex2D(_MainTex, i.uv);
//                 float alpha = 0;
//                 float dist = distance(i.worldPos, _WorldSpaceCameraPos);
//                 // Calculate alpha based on distance
//                 if (dist <= _MaxDistance) {
//                     dist = _MaxDistance - dist; // Reverse the distance
//                     float f = _MaxDistance - _MinDistance;
//                     float p = dist / f;
//                     alpha *= lerp(0, 1, p);
//                 } else {
//                     alpha = 0;
//                 }
//                 // Set the output color and alpha
//                 fixed4 col = tex * _Color;
//                 // alpha *= step(0.3, col.r + col.g + col.b);                
//                 col.a = tex.a * alpha;
                
//                 return col;
//             }
//             ENDCG
//         }
//     }
 
//     FallBack "Unlit/Texture"
// }


// Shader "Unlit/Transparent" {
//     Properties {
//         _MainTex ("Texture", 2D) = "white" {}
//         _Color ("Color", Color) = (1,1,1,1)
//     }
 
//     SubShader {
//         Tags {"Queue"="Transparent" "RenderType"="Opaque"}
//         LOD 100
 
//         Pass {
//             Blend SrcAlpha OneMinusSrcAlpha
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"
 
//             struct appdata {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };
 
//             struct v2f {
//                 float2 uv : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//                 float4 color : COLOR;
//             };
 
//             sampler2D _MainTex;
//             float4 _Color;
 
//             v2f vert (appdata v) {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;
//                 o.color = _Color;
//                 return o;
//             }
 
//             fixed4 frag (v2f i) : SV_Target {
//                 fixed4 tex = tex2D(_MainTex, i.uv);
//                 fixed4 col = i.color * tex;
//                 col.a *= tex.a;
//                 return col;
//             }
//             ENDCG
//         }
//     }
 
//     FallBack "Diffuse"
// }


Shader "Custom/SoftClipReversed" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _MinDistance ("Minimum Distance", Range(0, 100)) = 3
        _MaxDistance ("Maximum Distance", Range(0, 100)) = 100
        _FlipX ("Flip X", Float) = 0
        _FlipY ("Flip Y", Float) = 0
        _Speed ("Speed", Range(0, 10)) = 1
        _Color ("Albedo", Color) = (1,1,1)

    }
 
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        ZWrite Off
        ZTest Less
        Cull Back
 
        CGPROGRAM
        #pragma surface surf Lambert alpha
        #include "UnityCG.cginc"
 
        sampler2D _MainTex;
        float _MinDistance;
        float _MaxDistance;
        float _FlipX;
        float _FlipY;
        float _Speed;
        float3 _Color;

 
        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            float2 uv = IN.uv_MainTex;
            if (_FlipX > 0) uv.x = 1 - uv.x;
            if (_FlipY > 0) uv.y = 1 - uv.y;
 
            float t = _Time.y * _Speed;
            uv += float2(-t, 0);
 
            o.Albedo = _Color.rgb;
            half4 c = tex2D(_MainTex, uv);
            o.Albedo = c.rgb;
            float alpha = c.a;
            float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
            if (dist < _MaxDistance) {
                dist = _MaxDistance - dist; // Reverse the distance
                float f = _MaxDistance - _MinDistance;
                float p = dist / f;
                alpha *= lerp(0, 1, p);
            } else {
                alpha = 0;
            }
            alpha *= step(0.1, c.r + c.g + c.b); // Make black parts transparent
            o.Alpha = alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
// Shader "UI/Unlit-Transparent-Fade" {
//     Properties {
//         _MainTex ("Texture", 2D) = "white" {}
//         _DistanceFadeStart ("Fade Start Distance", Range(0, 100)) = 10
//         _DistanceFadeEnd ("Fade End Distance", Range(0, 100)) = 50
//     }

//     SubShader {
//         Tags {"Queue"="Transparent" "RenderType"="Transparent"}
//         Pass {
//             ZWrite Off
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             struct appdata_t {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct v2f {
//                 float2 uv : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//                 float4 projPos : TEXCOORD1;
//             };

//             sampler2D _MainTex;
//             float _DistanceFadeStart;
//             float _DistanceFadeEnd;

//             v2f vert (appdata_t v) {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;
//                 o.projPos = ComputeScreenPos(o.vertex);
//                 return o;
//             }

//             fixed4 frag (v2f i) : SV_Target {
//                 float dist = length(i.projPos.xyz / i.projPos.w - _ScreenParams.xy / _ScreenParams.w);
//                 float t = saturate((dist - _DistanceFadeStart) / (_DistanceFadeEnd - _DistanceFadeStart));
//                 fixed4 col = tex2D(_MainTex, i.uv);
//                 col.a *= 1 - smoothstep(0.0, 0.1, col.r + col.g + col.b);
//                 col.a *= (1 - t);
//                 return col;
//             }
//             ENDCG
//         }
//     }

//     FallBack "UI/Default"
// }


// Shader "Custom/SoftClipReversed" {
//     Properties {
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//         _MinDistance ("Minimum Distance", float) = 3
//         _MaxDistance ("Maximum Distance", float) = 100
//          _FlipX("Flip X", Range(0,1)) = 0
//         _FlipY("Flip Y", Range(0,1)) = 0
//     }
//     SubShader {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 200
//         ZWrite Off
//         ZTest Less
//         Cull Back
 
//         CGPROGRAM
//         #pragma surface surf Lambert alpha
//         #include "UnityCG.cginc" // Include the UnityCG.cginc file for texture access
 
//         sampler2D _MainTex;
//         float _MinDistance;
//         float _MaxDistance;
//         float _FlipX;
//         float _FlipY;
 
//         struct Input {
//             float2 uv_MainTex;
//             float3 worldPos;
//         };
 
//         void surf (Input IN, inout SurfaceOutput o) {
//             float2 uv = IN.uv_MainTex;
//             if (_FlipX == 1) uv.x = 1 - uv.x;
//             if (_FlipY == 1) uv.y = 1 - uv.y;
//             half4 c = tex2D(_MainTex, uv);
//             o.Albedo = c.rgb;
//             float alpha = c.a;
//             float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
//             if (dist < _MaxDistance) {
//                 dist = _MaxDistance - dist; // Reverse the distance
//                 float f = _MaxDistance - _MinDistance;
//                 float p = dist / f;
//                 alpha *= lerp(0, 1, p);
//             } else {
//                 alpha = 0;
//             }
//             alpha *= step(0.1, c.r + c.g + c.b); // Make black parts transparent
//             o.Alpha = alpha;
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }
// Shader "Custom/SoftClipWithColor" {
//     Properties {
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//         _MinDistance ("Minimum Distance", Range(0.1, 100)) = 3
//         _MaxDistance ("Maximum Distance", Range(0.1, 100)) = 100
//         _Color ("Tint Color", Color) = (1,1,1,1)
//     }
//     SubShader {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 200
//         ZWrite Off
//         ZTest Less
//         Cull Back
       
//         CGPROGRAM
//         #pragma surface surf Lambert alpha
       
//         sampler2D _MainTex;
//         float _MinDistance;
//         float _MaxDistance;
//         float4 _Color;
 
//         struct Input {
//             float2 uv_MainTex;
//             float3 worldPos;
//         };
 
//         void surf (Input IN, inout SurfaceOutput o) {
//             half4 c = tex2D (_MainTex, IN.uv_MainTex);
            
//             float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
//             if (dist <= _MaxDistance) {
//                 dist = _MaxDistance - dist; // Reverse the distance
//                 float f = _MaxDistance - _MinDistance;
//                 float p = dist / f;
//                 float cl = lerp(0, 1, p);
//                 cl = min(1, cl);
//                 cl = max(0, cl);
//                 o.Albedo = c.rgb * _Color.rgb;
//                 o.Alpha = cl;
//             }
//             if(dist > _MaxDistance) {
//                 o.Alpha = 0;
//                 o.Albedo = c.rgb * _Color.rgb;
//             }
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }
// Shader "Custom/SoftClipReversed" {
//     Properties {
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//         _Color ("Color", Color) = (1, 1, 1, 1)
//         _MinDistance ("Minimum Distance", Range(0, 100)) = 3
//         _MaxDistance ("Maximum Distance", Range(0, 100)) = 100
//     }
//     SubShader {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 200
//         ZWrite Off
//         ZTest Less
//         Cull Back
        
//         CGPROGRAM
//         #pragma surface surf Lambert alpha
        
//         sampler2D _MainTex;
//         float4 _Color;
//         float _MinDistance;
//         float _MaxDistance;
        
//         struct Input {
//             float2 uv_MainTex;
//             float3 worldPos;
//         };
        
//         void surf (Input IN, inout SurfaceOutput o) {
//             half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//             o.Albedo = c.rgb;
//             o.Alpha = 1;
//             float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
//             if (dist < _MaxDistance) {
//                 o.Albedo = c.rgb;
//                 dist = _MaxDistance - dist; // Reverse the distance
//                 float f = _MaxDistance - _MinDistance;
//                 float p = dist / f;
//                 float cl = lerp(0, 1, p);
//                 cl = min(1, cl);
//                 cl = max(0, cl);
//                 o.Alpha = cl;
//             }
//             if (dist > _MaxDistance) {
//                 o.Alpha = 0;
//             }
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }



// Shader "Custom/SoftClip" {
//     Properties {
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//         _MinDistance ("Minimum Distance", float) = 3
//         _MaxDistance ("Maximum Distance", float) = 100
//     }
//     SubShader {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 200
//         ZWrite Off
//         ZTest Less
//         Cull Back
       
//         CGPROGRAM
//         #pragma surface surf Lambert alpha
       
 
//         sampler2D _MainTex;
//         float _MinDistance;
//         float _MaxDistance;
 
//         struct Input {
//             float2 uv_MainTex;
//             float3 worldPos;
//         };
 
//         void surf (Input IN, inout SurfaceOutput o) {
//             half4 c = tex2D (_MainTex, IN.uv_MainTex);
//             o.Albedo = c;
//             o.Alpha = 1;
//             float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
//             if (dist < _MaxDistance) {
//                 o.Albedo = c.rgb;
//                 dist = dist - _MinDistance;
//                 float f = _MaxDistance - _MinDistance;
//                 float p = dist / f;
//                 float cl = lerp(0, 1, p);
//                 cl = min(1, cl);
//                 cl = max(0, cl);
//                 o.Alpha = cl;
//             }
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }

// Shader "Custom/SoftClipReversed" {
//     Properties {
//         _MainTex ("Base (RGB)", 2D) = "white" {}
//         _MinDistance ("Minimum Distance", float) = 3
//         _MaxDistance ("Maximum Distance", float) = 100
//     }
//     SubShader {
//         Tags { "Queue"="Transparent" "RenderType"="Transparent" }
//         LOD 200
//         ZWrite Off
//         ZTest Less
//         Cull Back
       
//         CGPROGRAM
//         #pragma surface surf Lambert alpha
       
//         sampler2D _MainTex;
//         float _MinDistance;
//         float _MaxDistance;
 
//         struct Input {
//             float2 uv_MainTex;
//             float3 worldPos;
//         };
 
//         void surf (Input IN, inout SurfaceOutput o) {
//             half4 c = tex2D (_MainTex, IN.uv_MainTex);
//             o.Albedo = c;
//             o.Alpha = 1;
//             float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
//             if (dist < _MaxDistance) {
//                 o.Albedo = c.rgb;
//                 dist = _MaxDistance - dist; // Reverse the distance
//                 float f = _MaxDistance - _MinDistance;
//                 float p = dist / f;
//                 float cl = lerp(0, 1, p);
//                 cl = min(1, cl);
//                 cl = max(0, cl);
//                 o.Alpha = cl;
//             }
//             if(dist > _MaxDistance) {
//                 o.Alpha = 0;
//             }
//         }
//         ENDCG
//     }
//     FallBack "Diffuse"
// }

