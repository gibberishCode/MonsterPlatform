// шейдер с наложением нескольких текстур и искажением uv координат.
// Базовые трансформации uv координат - Tiling, Scroll, Offset, Skew
// Настраиваемый способ наложения. Типовые комбинации: 
//    Normal Blend:   Source - SrcAlpha, Destination - OneMinusSrcAlpha
//    Add Blend:      Source - SrcAlpha, Destination - One


Shader "VFX/Base/TexBlend_Color"
{
    Properties
    {

        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendFactor ("Source", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlendFactor ("Destination", Float) = 10
        [Space(3)]
		[Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Culling Mode", Float) = 0

        [Space(15)]
        [Header(Color ###########################################)]
        [Space(16)]
        _Color ("Color", Color) = (1,1,1,1)

        _Col_Mult ("Mult", Range(0, 7)) = 1

        [Header(Alpha ###########################################)]
        [Space(16)]
        _CutoutThr ("Cutout Threshold", Range(-1, 2)) = 0
        _CutoutSm ("Cutout Smooth", Range(0.001, 1)) = 1

        
        [Space(15)]
        [Header(Tex01 ###########################################)]
        [Space(16)]
        [NoScaleOffset]
        _Tex01 ("Texture", 2D) = "white" {}
        [Space(8)]
        _Tex01_TilingScroll  ("Tiling Scroll ", Vector) = (1,1,0,0)
        _Tex01_OffsetSkew ("Offset Skew ", Vector) = (0,0,0,0)

        
        [Space(15)]
        [Header(Tex02 ###########################################)]
        [Space(16)]
        [Toggle] _Tex02_Use ("Use", Float) = 0
        [Space(10)]
        [NoScaleOffset]
        _Tex02 ("Texture", 2D) = "white" {}
        [Space(8)]
        _Tex02_TilingScroll  ("Tiling Scroll ", Vector) = (1,1,0,0)
        _Tex02_OffsetSkew ("Offset Skew ", Vector) = (0,0,0,0)
        [KeywordEnum(Mult, Add)] _Tex02_BlendMode ("Blend Mode", Float) = 0

        [Space(15)]
        [Header(Mask tex ###########################################)]
        [Space(16)]
        [Toggle] _TexMask_Use ("Use", Float) = 0
        [Space(10)]
        [NoScaleOffset]
        _TexMask ("Texture", 2D) = "white" {}
        [Space(8)]
        _TexMask_TilingScroll  ("Tiling Scroll ", Vector) = (1,1,0,0)
        _TexMask_OffsetSkew ("Offset Skew ", Vector) = (0,0,0,0)


        [Space(10)]
        [Header(UV Distort  ##################################################)]
        [Space(15)]
        [Toggle] _TexDistort_Use ("Use", Float) = 0
        [Space(15)]
        [NoScaleOffset]
        _TexDistort (" ", 2D) = "white" {}
        _TexDistort_TilingScroll  ("Tiling Scroll ", Vector) = (1,1,0,0)
        _TexDistort_OffsetSkew ("Offset Skew ", Vector) = (0,0,0,0)
        [Toggle] _TexDistort_Invert_R ("Invert R", Float) = 0
        [Toggle] _TexDistort_Invert_G ("Invert G", Float) = 0

        [Space(10)]
        [Header(Tex Distort)]
        _Tex_Distort_U ("U", Range(0, 3)) = 0
        _Tex_Distort_V ("V", Range(0, 3)) = 0
        [Space(10)]
        [Header(Mask Distort)]
        _Mask_Distort_U ("U", Range(0, 3)) = 0
        _Mask_Distort_V ("V", Range(0, 3)) = 0
    }
    SubShader
    {
        Tags
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}

        Blend [_SrcBlendFactor] [_DstBlendFactor]
		ZWrite Off
		Cull [_CullMode]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _TEX02_USE_ON
            #pragma shader_feature _TEXMASK_USE_ON
            #pragma shader_feature _TEXDISTORT_USE_ON


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 uv  : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _Tex01, _Tex02, _TexMask, _TexDistort;
            fixed _Tex01_Invert, _Tex02_Invert, _Tex01_Mode, _Tex02_BlendMode;
            float _Col_Mult,  _Tex02_Level;
            float4 _Tex01_TilingScroll, _Tex01_OffsetSkew;
            float4 _Tex02_TilingScroll, _Tex02_OffsetSkew;
            float4 _TexMask_TilingScroll, _TexMask_OffsetSkew;

            float4 _TexDistort_TilingScroll, _TexDistort_OffsetSkew;
            float _Tex_Distort_U, _Tex_Distort_V, _Mask_Distort_U, _Mask_Distort_V;
            fixed _TexDistort_Invert_R, _TexDistort_Invert_G;

            float _CutoutThr, _CutoutSm;

            fixed4 _Color;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv.xy  = (v.uv.xy + v.uv.yx * _Tex01_OffsetSkew.zw) * _Tex01_TilingScroll.xy + _Tex01_OffsetSkew.xy + frac(_Tex01_TilingScroll.zw * _Time.x);
                o.uv.zw  = (v.uv.xy + v.uv.yx * _Tex02_OffsetSkew.zw) * _Tex02_TilingScroll.xy + _Tex02_OffsetSkew.xy + frac(_Tex02_TilingScroll.zw * _Time.x);
                o.uv1.xy = (v.uv.xy + v.uv.yx * _TexMask_OffsetSkew.zw) * _TexMask_TilingScroll.xy + _TexMask_OffsetSkew.xy + frac(_TexMask_TilingScroll.zw * _Time.x);
                o.uv1.zw = (v.uv.xy + v.uv.yx * _TexDistort_OffsetSkew.zw) * _TexDistort_TilingScroll.xy + _TexDistort_OffsetSkew.xy + frac(_TexDistort_TilingScroll.zw * _Time.x);
                o.color = v.color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;

                #if _TEXDISTORT_USE_ON
                fixed2 dist = tex2D(_TexDistort, i.uv1.zw).rg ;
                dist = lerp(dist, 1-dist, fixed2(_TexDistort_Invert_R, _TexDistort_Invert_G)) - 0.5; // выборочная поканальная инверсия
                i.uv.xy += dist * float2(_Tex_Distort_U, _Tex_Distort_V);
                i.uv.zw += dist * float2(_Tex_Distort_U, _Tex_Distort_V);
                i.uv1.xy += dist * float2(_Mask_Distort_U, _Mask_Distort_V);
                #endif
                
                fixed4 tex01 = tex2D(_Tex01, i.uv.xy);
                tex01 = lerp(tex01, fixed4(1,1,1,tex01.r),  _Tex01_Mode );
                tex01.a = lerp(tex01.a, 1-tex01.a, _Tex01_Invert);


                #if _TEX02_USE_ON
                fixed4 tex02 = tex2D(_Tex02, i.uv.zw);
                tex02.a = lerp(tex02.a, 1-tex02.a, _Tex02_Invert);
                tex01 = lerp(tex01 * tex02 * 2, (tex01 + tex02) * 0.5, _Tex02_BlendMode);
                #endif

                #if _TEXMASK_USE_ON
                fixed tex_mask = tex2D(_TexMask, i.uv1.xy).r;
                tex01.a *= tex_mask;
                #endif

                col = tex01; 
                col.rgb *= _Col_Mult;
                col *= _Color * i.color;

                col.a = saturate((col.a - _CutoutThr)/_CutoutSm);

                return col;
            }
            ENDCG
        }
    }
}
