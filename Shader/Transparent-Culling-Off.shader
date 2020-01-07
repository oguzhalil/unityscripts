// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/Color-Culling-Off" {
Properties {
		_Color("Color Tint", Color) = (1,1,1,1)
		_Alpha("Alpha",Float) = 1.0
		_MainTex("Base (RGB)", 2D) = "white" {}

}
SubShader{
	Tags { "Queue" = "Transparent" }
	Cull Off
	LOD 0

CGPROGRAM
#pragma surface surf Lambert alpha:fade noforwardadd interpolateview halfasview noshadowmask noshadow novertexlights nolppv nometa nofog nodirlightmap nodynlightmap nolightmap 

float _Alpha;
float4 _Color;
sampler2D _MainTex;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    //float4 c = tex2D(_MainTex, IN.uv_MainTex);
	float4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb * _Color.rgb;
	o.Alpha = _Alpha;
}
ENDCG
}

	Fallback "Mobile/VertexLit"
}
