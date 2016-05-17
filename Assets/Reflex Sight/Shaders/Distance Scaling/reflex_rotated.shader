Shader "Transparent/Reflex Sight (Rotated)" {
	Properties {
		_reticleTex ("Reticle (RGB)", 2D) = "white" {}
		_reticleColour ("Reticle Colour", Color) = (1,1,1,1)
		_reticleBright ("Reticle Brightness", Range(0,1)) = 1
		_glassColour ("Glass Colour", Color) = (1,1,1,1)
		_glassTrans ("Glass Transparency", Range(0,1)) = 0.1
		_uvScale ("Reticle Scale", float) = 1
		_angle ("Sight Angle", float) = 45
	}
	SubShader {
		Tags {"Queue"="transparent" "RenderType"="transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
		#include "UnityCG.cginc"
		
		sampler2D _reticleTex;
		float _angle, _uvScale, _glassTrans, _reticleBright;
		float4 _reticleColour, _glassColour;
		
		struct Input
		{
		  float3 worldPos;
		  float3 worldNormal;
		};
				
		void surf (Input IN, inout SurfaceOutput o) {
			float PI = 3.14159;
			float rads = _angle * (PI / 180);
			float complement = PI/2 - rads;
			
			//project (camera - point) vector onto normal of surface
			float shortestDistanceToSurface = dot (_WorldSpaceCameraPos - IN.worldPos,IN.worldNormal);
			//get point on surface closest to camera
			float3 closestPoint = _WorldSpaceCameraPos - (shortestDistanceToSurface * IN.worldNormal);
			
			//find amount to offset center by such that line between offset point and camera makes desired angle with surface
			float centreOffset = shortestDistanceToSurface/tan(rads);
			
			//take both current point and closest point to object space
			float3 objClosestPoint = mul((float3x3)_World2Object,closestPoint);
			float3 objPoint = mul((float3x3)_World2Object,IN.worldPos);
			
			//offset closest point in local Y plane
			objClosestPoint.y -= centreOffset;
			
			//rotate closest point and current (in obj space) about X axis by complement of desired angle
			float ratio = cos(complement);
			objClosestPoint.y = objClosestPoint.y*ratio;
			objPoint.y = objPoint.y*ratio;
			
			//get difference between current point and closest point in object space in xy plane
			//multiply by unit scale factor to get uv delta
			float2 uv_Delta = (objPoint - objClosestPoint).xy * _uvScale/shortestDistanceToSurface;
			
			//sample texture using uv delta
			half4 col = tex2D(_reticleTex,(0.5, 0.5) + uv_Delta);
			
			o.Emission = col.a * _reticleColour.rgb * _reticleBright;
			o.Albedo = max(col.a * _reticleColour.rgb, _glassTrans * _glassColour.rgb);
			o.Alpha = max(col.a, _glassTrans);
		}
		
		ENDCG
	}
}
