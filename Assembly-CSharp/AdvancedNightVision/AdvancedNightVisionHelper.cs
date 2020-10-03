using System;
using UnityEngine;

namespace AdvancedNightVision
{
	// Token: 0x02000477 RID: 1143
	public static class AdvancedNightVisionHelper
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000AFA7D File Offset: 0x000ADC7D
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				return min;
			}
			if (value <= max)
			{
				return value;
			}
			return max;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000AFA8C File Offset: 0x000ADC8C
		public static bool SupportsDX11
		{
			get
			{
				return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000AFAA0 File Offset: 0x000ADCA0
		public static bool CheckHardwareRequirements(bool needRT, bool needDepth, bool needHDR, MonoBehaviour effect)
		{
			if (!SystemInfo.supportsImageEffects)
			{
				Debug.LogErrorFormat("Hardware not support Image Effects. '{0}' disabled.", new object[]
				{
					effect.ToString()
				});
				effect.enabled = false;
			}
			else if (needRT && !SystemInfo.supportsRenderTextures)
			{
				Debug.LogErrorFormat("Hardware not support Render Textures. '{0}' disabled.", new object[]
				{
					effect.ToString()
				});
				effect.enabled = false;
			}
			else if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				Debug.LogErrorFormat("Hardware not support Depth Buffer. '{0}' disabled.", new object[]
				{
					effect.ToString()
				});
				effect.enabled = false;
			}
			else if (needHDR && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
			{
				Debug.LogErrorFormat("Hardware not support HDR. '{0}' disabled.", new object[]
				{
					effect.ToString()
				});
				effect.enabled = false;
			}
			return true;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000AFB60 File Offset: 0x000ADD60
		public static bool IsSupported(Shader shader, MonoBehaviour effect)
		{
			if (shader == null || !shader.isSupported)
			{
				Debug.LogErrorFormat("Shader not supported. '{0}' disabled.\n{1}Please contact to 'hello@ibuprogames.com'.", new object[]
				{
					effect.ToString(),
					(((AdvancedNightVision)effect).ShaderPass == AdvancedNightVision.ShaderPasses.MultiPass) ? "You can try the 'One Pass' mode.\n" : string.Empty
				});
				effect.enabled = false;
			}
			return true;
		}
	}
}
