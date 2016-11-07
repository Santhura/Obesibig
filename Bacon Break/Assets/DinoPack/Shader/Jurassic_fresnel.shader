Shader "Jurassic/Diffuse-Fresnel-Normal" {
	Properties{
		_Color("Base Color", Color) = (1,1,1,1)
		_MyColor("Fresnel Color", Color) = (1,1,1,1)
		_Shininess("Fresnel Amount", Range(0.01, 3)) = 1
		_MainTex("Base (RGB)", 2D) = "white" {}

	//_Bump("Bump", 2D) = "bump" {}   //+Normal map

	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200


		CGPROGRAM
#pragma surface surf Lambert

    sampler2D _MainTex;
	//sampler2D _Bump;  //+Normal map
	fixed _Shininess;
	fixed4 _MyColor;
	fixed4 _Color;

	struct Input {
		fixed2 uv_MainTex;
	//	fixed2 uv_Bump;   //+Normal map
		fixed3 viewDir;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		//o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump)); //+Normal map
		fixed factor = dot(normalize(IN.viewDir),o.Normal);
		o.Albedo = c.rgb + _MyColor*(_Shininess - factor*_Shininess);
		o.Emission.rgb = _MyColor*(_Shininess - factor*_Shininess);
     	o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}