Shader "Transparent/Holographic Sight" {
	Properties {
		_reticleTex ("Reticle (RGB)", 2D) = "white" {}
		_reticleColour ("Reticle Colour", Color) = (1,1,1,1)
		_reticleBright ("Reticle Brightness", Range(0,1)) = 1
		_glassColour ("Glass Colour", Color) = (1,1,1,1)
		_glassTrans ("Glass Transparency", Range(0,1)) = 0.1
		_uvScale ("Reticle Scale", float) = 1
		_distance ("Reticle Distance", float) = 50
	}
	SubShader {
		Tags {"Queue"="transparent" "RenderType"="transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
		#include "UnityCG.cginc"

		sampler2D _reticleTex;
		float _uvScale, _glassTrans, _reticleBright, _distance;
		float4 _reticleColour, _glassColour;

		struct Input {
			float3 worldRefl;
			float3 worldPos;
			float3 worldNormal;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {		
			//project (camera - point) vector onto normal of surface to find shortest distance from camera to surface		
			//add distance to reticle to get yComponent of right triangle with reticle and camera connected by hypotenuse
			float yComponent = dot (_WorldSpaceCameraPos - IN.worldPos,IN.worldNormal) + _distance;
			
			float3 normalizedViewVector = normalize(IN.worldPos - _WorldSpaceCameraPos);
			//find length of hypotenuse
			float hypotenuse = yComponent/dot(normalize(-IN.worldNormal), normalizedViewVector);
			
			//extend point - camera along hypotenuse to plane on which reticle lies
			float3 offsetVector = _WorldSpaceCameraPos + normalizedViewVector * hypotenuse;
			float4 offsetPoint;
			offsetPoint.xyz = offsetVector.xyz;
			offsetPoint.w = 1;
			
			//find center of sight window (assume at 0,0 in object space) and offset by distance along -normal
			//take offset point and center of reticle to object space and get difference between them in xy plane
			//multiply by unit scale factor to get uv delta
			float2 uv_Delta = (mul(_World2Object,offsetPoint) - float4(0,0,_distance,1)).xy * _uvScale;
			
			//sample texture using uv delta
			half4 col = tex2D(_reticleTex,(0.5f, 0.5f) + uv_Delta);
			
			o.Emission = col.a * _reticleColour.rgb * _reticleBright;
			o.Albedo = max(col.a * _reticleColour.rgb, _glassTrans * _glassColour.rgb);
			o.Alpha = max(col.a, _glassTrans);
		}
		
		ENDCG
	}
}
