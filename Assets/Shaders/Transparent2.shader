// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Unlit/Transparent2" {

	Properties{
		_Color("Color Tint", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_AlphaTex("Alpha mask (R)", 2D) = "white" {}
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		float2 texcoord2 : TEXCOORD1;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		half2 texcoord2 : TEXCOORD1;
	};

	sampler2D _MainTex;
	sampler2D _AlphaTex;
	fixed4 _Color;

	float4 _MainTex_ST;
	float4 _AlphaTex_ST;


	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.texcoord2 = TRANSFORM_TEX(v.texcoord, _AlphaTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col3 = _Color;
		fixed4 col = tex2D(_MainTex, i.texcoord);
	fixed4 col2 = tex2D(_AlphaTex, i.texcoord2);

	return fixed4(col.r, col.g, col.b, col2.r) *  fixed4(1, 1, 1, col3.a);
	}
		ENDCG
	}
	}


}