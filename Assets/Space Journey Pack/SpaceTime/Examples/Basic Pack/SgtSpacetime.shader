Shader "Hidden/SgtSpacetime"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Tile("Tile", Float) = 1
		_Power("Power", Float) = 1
		_Effect("Effect", Vector) = (0, 0, 0)
		_Well1_Pos("Well 1 Pos", Vector) = (0, 0, 0)
		_Well2_Pos("Well 2 Pos", Vector) = (0, 0, 0)
		_Well3_Pos("Well 3 Pos", Vector) = (0, 0, 0)
		_Well4_Pos("Well 4 Pos", Vector) = (0, 0, 0)
		_Well5_Pos("Well 5 Pos", Vector) = (0, 0, 0)
		_Well6_Pos("Well 6 Pos", Vector) = (0, 0, 0)
		_Well7_Pos("Well 7 Pos", Vector) = (0, 0, 0)
		_Well1_Dat("Well 1 Dat", Vector) = (0, 0, 0, 0)
		_Well2_Dat("Well 2 Dat", Vector) = (0, 0, 0, 0)
		_Well3_Dat("Well 3 Dat", Vector) = (0, 0, 0, 0)
		_Well4_Dat("Well 4 Dat", Vector) = (0, 0, 0, 0)
		_Well5_Dat("Well 5 Dat", Vector) = (0, 0, 0, 0)
		_Well6_Dat("Well 6 Dat", Vector) = (0, 0, 0, 0)
		_Well7_Dat("Well 7 Dat", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags
		{
			"Queue"           = "Transparent"
			"RenderType"      = "Transparent"
			"IgnoreProjector" = "True"
		}
		Pass
		{
			Blend One One
			Cull Off
			Lighting Off
			ZWrite Off
			
			CGPROGRAM
				#pragma vertex Vert
				#pragma fragment Frag
				// Pinch, Offset
				#pragma multi_compile DUMMY SGT_A
				// Well count + 1
				#pragma multi_compile DUMMY SGT_B
				// Well count + 2
				#pragma multi_compile DUMMY SGT_C
				// Well count + 4
				#pragma multi_compile DUMMY SGT_D
				
				#define WELL_COUNT (SGT_B * 1 + SGT_C * 2 + SGT_D * 4)
				
				sampler2D _MainTex;
				float4    _Color;
				float     _Tile;
				float     _Power;
				float3    _Offset;
				float3    _Well1_Pos; float4 _Well1_Dat; // x = radius, y = age, z = strength
				float3    _Well2_Pos; float4 _Well2_Dat;
				float4    _Well3_Pos; float4 _Well3_Dat;
				float4    _Well4_Pos; float4 _Well4_Dat;
				float4    _Well5_Pos; float4 _Well5_Dat;
				float4    _Well6_Pos; float4 _Well6_Dat;
				float4    _Well7_Pos; float4 _Well7_Dat;
				
				struct a2v
				{
					float4 vertex    : POSITION;
					float4 color     : COLOR;
					float2 texcoord0 : TEXCOORD0;
				};
				
				struct v2f
				{
					float4 vertex    : SV_POSITION;
					float4 color     : COLOR;
					float2 texcoord0 : TEXCOORD0;
				};
				
				struct f2g
				{
					float4 color : COLOR;
				};
				
				void UpdateWell(inout float4 worldPoint, float3 wellPosition, float4 wellData)
				{
					float3 vec = wellPosition.xyz - worldPoint.xyz;
					float  mag = saturate(length(vec) / wellData.x);
#if SGT_A
					worldPoint.xyz += _Offset * smoothstep(1.0f, 0.0f, mag) * wellData.x * wellData.z;
#else 
					mag = 1.0f - pow(smoothstep(0.0f, 1.0f, (mag)), _Power);
					
					worldPoint.xyz += vec * mag * wellData.z;
#endif
				}
				
				void Vert(a2v i, out v2f o)
				{
					float4 wPos = mul(_Object2World, i.vertex);
#if WELL_COUNT >= 1
					UpdateWell(wPos, _Well1_Pos, _Well1_Dat);
#endif
#if WELL_COUNT >= 2
					UpdateWell(wPos, _Well2_Pos, _Well2_Dat);
#endif
#if WELL_COUNT >= 3
					UpdateWell(wPos, _Well3_Pos, _Well3_Dat);
#endif
#if WELL_COUNT >= 4
					UpdateWell(wPos, _Well4_Pos, _Well4_Dat);
#endif
#if WELL_COUNT >= 5
					UpdateWell(wPos, _Well5_Pos, _Well5_Dat);
#endif
#if WELL_COUNT >= 6
					UpdateWell(wPos, _Well6_Pos, _Well6_Dat);
#endif
#if WELL_COUNT >= 7
					UpdateWell(wPos, _Well7_Pos, _Well7_Dat);
#endif
					o.vertex    = mul(UNITY_MATRIX_VP, wPos);
					o.color     = i.color * _Color;
					o.texcoord0 = i.texcoord0 * _Tile;
				}
				
				void Frag(v2f i, out f2g o)
				{
					o.color = tex2D(_MainTex, i.texcoord0) * i.color;
				}
			ENDCG
		} // Pass
	} // SubShader
} // Shader