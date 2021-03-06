// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/Diffuse-Cull-Off" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}

}
SubShader {
    Tags { "RenderType"="Opaque" }
	Cull Off
    LOD 0

CGPROGRAM
#pragma surface surf Lambert noforwardadd interpolateview halfasview noshadowmask noshadow novertexlights nolppv nometa nofog nodirlightmap nodynlightmap nolightmap 

sampler2D _MainTex;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    float4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb;
    o.Alpha = c.a;
}
ENDCG
}

	Fallback "Mobile/VertexLit"
}
