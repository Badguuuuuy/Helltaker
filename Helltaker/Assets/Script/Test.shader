Shader "Unlit/Test"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _CompressAmount("Compress Amount", Float) = 0.1 // ���� ����
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
                float _CompressAmount; // ���� ����

                v2f vert(appdata v)
                {
                    v2f o;

                    // ��������Ʈ�� �ϴ� Y��ǥ (������)
                    float bottomY = -0.5; // ��������Ʈ�� �ϴ� Y��ǥ�� -0.5�� ���� (��Ȯ�� �ϴܿ� ��ġ)

                    // Y�࿡ ���� ���� ���� ���
                    float compress = (v.vertex.y - bottomY) * _CompressAmount;

                    // Y���� ���� ������ŭ �Ʒ��� �̵�
                    v.vertex.y -= compress;

                    // ������ Ŭ�� �������� ��ȯ
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // �ؽ�ó�� ����� ���� ���� �Բ� ó��
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    return texColor;
                }
                ENDCG
            }
        }
            FallBack "Transparent/Cutout/VertexLit"
}
