Shader "Mobile/OptimizedDiffuse" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader{

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd interpolateview halfasview noshadowmask noshadow novertexlights nolppv nometa nofog nodirlightmap nodynlightmap nolightmap 

		sampler2D _MainTex;

		struct Input {
			fixed2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

		Fallback Off
}
