Shader "Unlit/Test"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _CompressAmount("Compress Amount", Float) = 0.1 // 압축 강도
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

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
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _CompressAmount; // 압축 강도

                v2f vert(appdata v)
                {
                    v2f o;

                    // 스프라이트의 하단 Y좌표 (고정값)
                    float bottomY = -0.5; // 스프라이트의 하단 Y좌표를 -0.5로 설정 (정확히 하단에 위치)

                    // Y축에 따라 압축 정도 계산
                    float compress = (v.vertex.y - bottomY) * _CompressAmount;

                    // Y축을 압축 정도만큼 아래로 이동
                    v.vertex.y -= compress;

                    // 정점을 클립 공간으로 변환
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 텍스처의 색상과 알파 값을 함께 처리
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    return texColor;
                }
                ENDCG
            }
        }
            FallBack "Transparent/Cutout/VertexLit"
}
