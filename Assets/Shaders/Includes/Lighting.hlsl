//    Copyright (C) 2020 NedMakesGames

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.

// This is the Lighting.hlsl file for the end of the first video in 2020.2

#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out half DistanceAtten, out half ShadowAtten) {
#ifdef SHADERGRAPH_PREVIEW
    Direction = half3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToHClip(WorldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void AddAdditionalLights_float(float3 WorldPosition, float3 WorldNormal,
    out float Diffuse, out float3 Color) {

    half3 Direction;
    half3 MainColor;
    half MainDiffuse;

#ifdef SHADERGRAPH_PREVIEW
    Direction = half3(0.5, 0.5, 0);
    MainColor = 1;
#else
    Light mainLight = GetMainLight(0);
    Direction = mainLight.direction;
    MainColor = mainLight.color;
#endif

    MainDiffuse = saturate(dot(WorldNormal, Direction));

    Diffuse = MainDiffuse;
    Color = MainColor * MainDiffuse;

#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i) {
        Light light = GetAdditionalLight(i, WorldPosition);
        half NdotL = saturate(dot(WorldNormal, light.direction));
        half atten = light.distanceAttenuation * light.shadowAttenuation;
        half thisDiffuse = atten * NdotL;
        Diffuse += thisDiffuse;
        Color += light.color * thisDiffuse;
    }
#endif

    half total = Diffuse;
    // If no light touches this pixel, set the color to the main light's color
    Color = total <= 0 ? MainColor : Color / total;
}

void CalculateAlpha_float(float RedWeight, float GreenWeight, float BlueWeight, float3 Color,
    out float Alpha) {

    Alpha = 0;
    Alpha += RedWeight * Color.x;
    Alpha += GreenWeight * Color.y;
    Alpha += BlueWeight * Color.z;
    Alpha = clamp(Alpha, 0, 1);
}

#endif