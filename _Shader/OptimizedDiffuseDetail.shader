// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/OptimizedDiffuseDetail" {
Properties 
{
    _MainTex ("Base (RGB)", 2D) = "white" {}
	_DetailMap("Detailmap", 2D) = "Detail" {}
	_Strength("Ratio" , Range(0.0, 1.0)) = 0.2
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 0

CGPROGRAM
#pragma surface surf Lambert noforwardadd interpolateview halfasview noshadowmask noshadow novertexlights nolppv nometa nofog nodirlightmap nodynlightmap nolightmap 

sampler2D _MainTex;
sampler2D _DetailMap;
float _Strength;


struct Input {
    fixed2 uv_MainTex;
	fixed2 uv_DetailMap;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 z = tex2D(_DetailMap, IN.uv_DetailMap);

	fixed3 n = lerp(c.rgb, c.rgb * z.rgb, z.a * _Strength);
    o.Albedo = n;
    o.Alpha = c.a;
}
ENDCG
}

	Fallback Off
}
