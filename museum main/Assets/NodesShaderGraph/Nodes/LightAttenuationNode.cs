#if UNITY_EDITOR
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEditor.ShaderGraph;
using System.Reflection;

[Title("Input", "Scene", "LightAttenuation")]
public class LightAttenuationNode : CodeFunctionNode
{
	public override bool hasPreview {get {return false;}}


	private static string functionBodyForReals = @"{

			float4 shadowCoord;
			#ifdef _SHADOWS_ENABLED
        	#if SHADOWS_SCREEN
				float4 clipPos = TransformWorldToHClip(WorldPos);
        		shadowCoord = ComputeShadowCoord(clipPos);
        	#else
        		shadowCoord = TransformWorldToShadowCoord(WorldPos);
        	#endif
			Attenuation = MainLightRealtimeShadowAttenuation(shadowCoord);
        	#endif
            Attenuation = MainLightRealtimeShadowAttenuation(shadowCoord);
		}";
	

	private static string functionBodyPreview = @"{
			Attenuation = 1;
		}";

	private static bool isPreview;


	private static string functionBody
	{
		get
		{
			if(isPreview)
				return functionBodyPreview;
			else
				return functionBodyForReals;
		}
	}


	public LightAttenuationNode()
	{
		name = "LightAttenuation";
	}
	
	protected override MethodInfo GetFunctionToConvert()
	{
		return GetType().GetMethod("LightAttenuationFunction", BindingFlags.Static | BindingFlags.NonPublic);
	}


	public override void GenerateNodeFunction(FunctionRegistry registry, GraphContext graphContext, GenerationMode generationMode)
	{
		isPreview = generationMode == GenerationMode.Preview;

		base.GenerateNodeFunction(registry, graphContext, generationMode);
	}


	private static string LightAttenuationFunction(
	[Slot(0, Binding.None)] out Vector1 Attenuation,
	[Slot(1, Binding.WorldSpacePosition)] Vector3 WorldPos)
	{

		return functionBody;
	}
}

#endif