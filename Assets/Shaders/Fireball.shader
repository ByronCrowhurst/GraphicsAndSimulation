Shader "Custom/Fireball"
{
    Properties
    {
		_RampTex("Ramp texture", 2D) = "white"{}
		_NoiseTex("Noise texture", 2D) = "grey" {}
		_RampVal("Ramp offset", Range(-0.5, 0.5)) = 0
		_Amplitude("Amplitude factor", Range(0, 0.3)) = 0.1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows vertex:vert addshadow

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _NoiseTex;
			sampler2D _RampTex;
			fixed _RampVal;
			fixed _Amplitude;
			struct Input
			{
				float2 uv_NoiseTex;
			};

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)
		
			void vert(inout appdata_full v)
			{
				half noiseVal = tex2Dlod(_NoiseTex, float4(v.texcoord.xy, 0, 0)).rrr;
				//v.vertex.xyz *= 2;
				v.vertex.y += v.normal * sin(_Time.w + noiseVal * 100) * _Amplitude;
				//v.vertex.y += sin(v.vertex.x);
			}

			void surf (Input IN, inout SurfaceOutputStandard o)
			{
				// Albedo comes from a texture tinted by color
				half noiseVal = tex2D(_NoiseTex, IN.uv_NoiseTex).r + (sin(_Time.y)) / 15;
				half4 colour = tex2D(_RampTex, float2(saturate(_RampVal + noiseVal), 0.5));
				//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = colour.rgb;
				// Metallic and smoothness come from slider variables
				o.Emission = colour.rgb;
			}
		ENDCG
		}
    FallBack "Diffuse"
}
