using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedNightVision
{
	// Token: 0x02000476 RID: 1142
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Advanced Night Vision")]
	public sealed class AdvancedNightVision : MonoBehaviour
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000AEF3B File Offset: 0x000AD13B
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x000AEF43 File Offset: 0x000AD143
		public AdvancedNightVision.ShaderPasses ShaderPass
		{
			get
			{
				return this.shaderPasses;
			}
			set
			{
				if (this.shaderPasses != value)
				{
					this.shaderPasses = value;
					this.LoadShader();
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000AEF5B File Offset: 0x000AD15B
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x000AEF63 File Offset: 0x000AD163
		public AdvancedNightVision.RenderTextureResolutions RenderTextureResolution
		{
			get
			{
				return this.renderTextureResolution;
			}
			set
			{
				this.renderTextureResolution = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000AEF6C File Offset: 0x000AD16C
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x000AEF74 File Offset: 0x000AD174
		[FloatProperty(0f, 10f, 2f, "Blur passes. Less passes is faster, but the quality is worse.")]
		public float BlurPasses
		{
			get
			{
				return this.blurPasses;
			}
			set
			{
				this.blurPasses = (float)((int)Mathf.Clamp(value, 0f, 10f));
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000AEF8E File Offset: 0x000AD18E
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x000AEF96 File Offset: 0x000AD196
		[FloatProperty(0f, 25f, 4f, "Glow, less is faster")]
		public float Glow
		{
			get
			{
				return this.glow;
			}
			set
			{
				this.glow = (float)((int)Mathf.Clamp(value, 0f, 25f));
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000AEFB0 File Offset: 0x000AD1B0
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x000AEFB8 File Offset: 0x000AD1B8
		public AdvancedNightVision.ColorControls ColorControl
		{
			get
			{
				return this.colorControlType;
			}
			set
			{
				this.colorControlType = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000AEFC1 File Offset: 0x000AD1C1
		// (set) Token: 0x0600239C RID: 9116 RVA: 0x000AEFC9 File Offset: 0x000AD1C9
		public Color Tint
		{
			get
			{
				return this.tint;
			}
			set
			{
				this.tint = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x000AEFD2 File Offset: 0x000AD1D2
		// (set) Token: 0x0600239E RID: 9118 RVA: 0x000AEFDA File Offset: 0x000AD1DA
		[FloatProperty(0f, 1f, 1f, "Tint color amount")]
		public float LuminanceAmount
		{
			get
			{
				return this.luminanceAmount;
			}
			set
			{
				this.luminanceAmount = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000AEFF2 File Offset: 0x000AD1F2
		// (set) Token: 0x060023A0 RID: 9120 RVA: 0x000AEFFA File Offset: 0x000AD1FA
		[FloatProperty(0f, 10f, 1f, "Exposure")]
		public float Exposure
		{
			get
			{
				return this.exposure;
			}
			set
			{
				this.exposure = Mathf.Clamp(value, 0f, 10f);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000AF012 File Offset: 0x000AD212
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x000AF01A File Offset: 0x000AD21A
		[FloatProperty(-1f, 1f, 0f, "Brightness")]
		public float Brightness
		{
			get
			{
				return this.brightness;
			}
			set
			{
				this.brightness = Mathf.Clamp(value, -1f, 1f);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000AF032 File Offset: 0x000AD232
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x000AF03A File Offset: 0x000AD23A
		[FloatProperty(-1f, 1f, 0f, "Contrast")]
		public float Contrast
		{
			get
			{
				return this.contrast;
			}
			set
			{
				this.contrast = Mathf.Clamp(value, -1f, 1f);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000AF052 File Offset: 0x000AD252
		// (set) Token: 0x060023A6 RID: 9126 RVA: 0x000AF05A File Offset: 0x000AD25A
		[FloatProperty(0f, 1f, 0.5f, "Saturation")]
		public float Saturation
		{
			get
			{
				return this.saturation;
			}
			set
			{
				this.saturation = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000AF072 File Offset: 0x000AD272
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x000AF07C File Offset: 0x000AD27C
		public Vector3 RGBOffset
		{
			get
			{
				return this.rgbOffset;
			}
			set
			{
				this.rgbOffset = new Vector3(Mathf.Clamp(value.x, -100f, 100f), Mathf.Clamp(value.y, -100f, 100f), Mathf.Clamp(value.z, -100f, 100f));
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000AF0D3 File Offset: 0x000AD2D3
		// (set) Token: 0x060023AA RID: 9130 RVA: 0x000AF0DB File Offset: 0x000AD2DB
		public AdvancedNightVision.VignetteTypes VignetteType
		{
			get
			{
				return this.vignetteType;
			}
			set
			{
				this.vignetteType = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000AF0E4 File Offset: 0x000AD2E4
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x000AF0EC File Offset: 0x000AD2EC
		[FloatProperty(0f, 100f, 10f, "Vignette scale")]
		public float VignetteScale
		{
			get
			{
				return this.vignetteScale;
			}
			set
			{
				this.vignetteScale = Mathf.Clamp(value, 0f, 100f);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000AF104 File Offset: 0x000AD304
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x000AF10C File Offset: 0x000AD30C
		[FloatProperty(0f, 10f, 0.1f, "Vignette softness")]
		public float VignetteSoftness
		{
			get
			{
				return this.vignetteSoftness;
			}
			set
			{
				this.vignetteSoftness = Mathf.Clamp(value, 0f, 10f);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000AF124 File Offset: 0x000AD324
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x000AF12C File Offset: 0x000AD32C
		public AdvancedNightVision.ChromaticAberrationTypes ChromaticAberrationType
		{
			get
			{
				return this.chromaticAberrationType;
			}
			set
			{
				this.chromaticAberrationType = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000AF135 File Offset: 0x000AD335
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x000AF13D File Offset: 0x000AD33D
		[IntProperty(3, 24, 6, "Distortions passes. Less passes is faster, but the quality is worse")]
		public int DistortionsPasses
		{
			get
			{
				return this.distortionsPasses;
			}
			set
			{
				this.distortionsPasses = AdvancedNightVisionHelper.Clamp(value, 3, 24);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x000AF14E File Offset: 0x000AD34E
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x000AF156 File Offset: 0x000AD356
		[FloatProperty(-100f, 100f, 2f, "Chromatic aberration")]
		public float ChromaticAberration
		{
			get
			{
				return this.chromaticAberration;
			}
			set
			{
				this.chromaticAberration = Mathf.Clamp(value, -100f, 100f);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000AF16E File Offset: 0x000AD36E
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x000AF176 File Offset: 0x000AD376
		[FloatProperty(-100f, 100f, 10f, "Barrel distortion")]
		public float BarrelDistortion
		{
			get
			{
				return this.barrelDistortion;
			}
			set
			{
				this.barrelDistortion = Mathf.Clamp(value, -100f, 100f);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000AF18E File Offset: 0x000AD38E
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000AF196 File Offset: 0x000AD396
		public bool AnalogTV
		{
			get
			{
				return this.analogTV;
			}
			set
			{
				this.analogTV = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000AF19F File Offset: 0x000AD39F
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x000AF1A7 File Offset: 0x000AD3A7
		[FloatProperty(0f, 10f, 3f, "Noise grain")]
		public float Noise
		{
			get
			{
				return this.noise;
			}
			set
			{
				this.noise = Mathf.Clamp(value, 0f, 10f);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000AF1BF File Offset: 0x000AD3BF
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x000AF1C7 File Offset: 0x000AD3C7
		[FloatProperty(0f, 2f, 1f, "Scanlines")]
		public float Scanline
		{
			get
			{
				return this.scanline;
			}
			set
			{
				this.scanline = Mathf.Clamp(value, 0f, 2f);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000AF1DF File Offset: 0x000AD3DF
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x000AF1E7 File Offset: 0x000AD3E7
		public bool DigitalTV
		{
			get
			{
				return this.digitalTV;
			}
			set
			{
				this.digitalTV = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000AF1F0 File Offset: 0x000AD3F0
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x000AF1F8 File Offset: 0x000AD3F8
		[FloatProperty(0f, 1f, 0.1f, "Threshold")]
		public float DigitalTVNoiseThreshold
		{
			get
			{
				return this.digitalTVNoiseThreshold;
			}
			set
			{
				this.digitalTVNoiseThreshold = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000AF206 File Offset: 0x000AD406
		// (set) Token: 0x060023C2 RID: 9154 RVA: 0x000AF20E File Offset: 0x000AD40E
		[FloatProperty(0f, 1f, 0.1f, "MaxOffset")]
		public float DigitalTVNoiseMaxOffset
		{
			get
			{
				return this.digitalTVNoiseMaxOffset;
			}
			set
			{
				this.digitalTVNoiseMaxOffset = Mathf.Clamp01(value);
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000AF21C File Offset: 0x000AD41C
		public void ResetDefaultValues()
		{
			this.blurPasses = 2f;
			this.luminanceAmount = 1f;
			this.exposure = 1f;
			this.rgbOffset = new Vector3(10f, 1f, -10f);
			this.tint = new Color(0.258f, 1f, 0.188f);
			this.brightness = 0f;
			this.contrast = 0f;
			this.saturation = 0.5f;
			this.distortionsPasses = 6;
			this.chromaticAberration = 1f;
			this.barrelDistortion = 10f;
			this.noise = 3f;
			this.scanline = 1f;
			this.vignetteScale = 10f;
			this.vignetteSoftness = 0.1f;
			this.glow = 4f;
			this.digitalTVNoiseThreshold = 0.1f;
			this.digitalTVNoiseMaxOffset = 0.1f;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000AF309 File Offset: 0x000AD509
		private void Awake()
		{
			this.LoadShader();
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000AF311 File Offset: 0x000AD511
		private void OnEnable()
		{
			if (AdvancedNightVisionHelper.CheckHardwareRequirements(true, false, false, this) && AdvancedNightVisionHelper.IsSupported(this.shader, this))
			{
				this.material = new Material(this.shader);
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000AF33D File Offset: 0x000AD53D
		private void OnDisable()
		{
			if (this.material != null)
			{
				Object.DestroyImmediate(this.material);
			}
			this.ReleaseAllTemporaryRenderTextures();
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000AF360 File Offset: 0x000AD560
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.material != null)
			{
				if (this.shaderPasses == AdvancedNightVision.ShaderPasses.MultiPass)
				{
					if (this.blurPasses > 0f)
					{
						this.material.EnableKeyword("BLUR_ENABLED");
						this.material.SetFloat("_Blur", this.blurPasses);
					}
					else
					{
						this.material.DisableKeyword("BLUR_ENABLED");
					}
					this.material.DisableKeyword("CHROMATIC_NONE");
					this.material.DisableKeyword("CHROMATIC_SIMPLE");
					this.material.DisableKeyword("CHROMATIC_ADVANCED");
					switch (this.chromaticAberrationType)
					{
					case AdvancedNightVision.ChromaticAberrationTypes.None:
						this.material.EnableKeyword("CHROMATIC_NONE");
						break;
					case AdvancedNightVision.ChromaticAberrationTypes.Simple:
						this.material.EnableKeyword("CHROMATIC_SIMPLE");
						this.material.SetFloat("_ChromaticAberration", this.chromaticAberration);
						break;
					case AdvancedNightVision.ChromaticAberrationTypes.Advanced:
						this.material.EnableKeyword("CHROMATIC_ADVANCED");
						this.material.SetInt("_DistortionsPasses", this.distortionsPasses);
						this.material.SetFloat("_ChromaticAberration", this.chromaticAberration);
						this.material.SetFloat("_BarrelDistortion", this.barrelDistortion * 0.05f);
						break;
					}
					if (this.glow > 0f)
					{
						this.material.EnableKeyword("GLOW_ENABLED");
						this.material.SetFloat("_Glow", this.glow);
					}
					else
					{
						this.material.DisableKeyword("GLOW_ENABLED");
					}
				}
				this.material.SetColor("_Tint", this.tint);
				this.material.SetFloat("_LuminanceAmount", this.luminanceAmount);
				this.material.SetFloat("_Exposure", this.exposure);
				if (this.colorControlType == AdvancedNightVision.ColorControls.Advanced)
				{
					this.material.EnableKeyword("COLORCONTROL_ADVANCED");
					this.material.SetVector("_RGBLum", this.rgbOffset);
					this.material.SetFloat("_Brightness", this.brightness);
					this.material.SetFloat("_Contrast", 1f + this.contrast);
					this.material.SetFloat("_Saturation", this.saturation);
				}
				else
				{
					this.material.DisableKeyword("COLORCONTROL_ADVANCED");
				}
				if (this.analogTV)
				{
					this.material.EnableKeyword("ANALOGTV_ENABLED");
					this.material.SetFloat("_Scanline", (this.scanline < 0.04f) ? 0.04f : this.scanline);
					this.material.SetFloat("_Noise", this.noise * 0.1f);
				}
				else
				{
					this.material.DisableKeyword("ANALOGTV_ENABLED");
				}
				if (this.digitalTV)
				{
					this.material.EnableKeyword("DIGITALTV_ENABLED");
					this.material.SetFloat("_DigitalTVNoiseThreshold", this.digitalTVNoiseThreshold);
					this.material.SetFloat("_DigitalTVNoiseMaxOffset", this.digitalTVNoiseMaxOffset);
				}
				else
				{
					this.material.DisableKeyword("DIGITALTV_ENABLED");
				}
				this.material.DisableKeyword("VIGNETTE_NONE");
				this.material.DisableKeyword("VIGNETTE_SCREEN");
				this.material.DisableKeyword("VIGNETTE_MONOCULAR");
				this.material.DisableKeyword("VIGNETTE_BINOCULAR");
				switch (this.vignetteType)
				{
				case AdvancedNightVision.VignetteTypes.None:
					this.material.EnableKeyword("VIGNETTE_NONE");
					break;
				case AdvancedNightVision.VignetteTypes.Screen:
					this.material.EnableKeyword("VIGNETTE_SCREEN");
					break;
				case AdvancedNightVision.VignetteTypes.Monocular:
					this.material.EnableKeyword("VIGNETTE_MONOCULAR");
					break;
				case AdvancedNightVision.VignetteTypes.Binocular:
					this.material.EnableKeyword("VIGNETTE_BINOCULAR");
					break;
				}
				if (this.vignetteType != AdvancedNightVision.VignetteTypes.None)
				{
					this.material.SetInt("_VignetteType", (int)this.vignetteType);
					this.material.SetFloat("_VignetteScale", (this.vignetteType == AdvancedNightVision.VignetteTypes.Screen) ? this.vignetteScale : (this.vignetteScale * 0.15f));
					this.material.SetFloat("_VignetteSoftness", this.vignetteSoftness * 0.075f);
				}
				if (source)
				{
					int width = source.width / (int)this.renderTextureResolution;
					int height = source.height / (int)this.renderTextureResolution;
					if (this.shaderPasses == AdvancedNightVision.ShaderPasses.OnePass)
					{
						if (this.renderTextureResolution == AdvancedNightVision.RenderTextureResolutions.Normal)
						{
							Graphics.Blit(source, destination, this.material);
							return;
						}
						RenderTexture temporaryRenderTexture = this.GetTemporaryRenderTexture(width, height);
						Graphics.Blit(source, temporaryRenderTexture, this.material);
						Graphics.Blit(temporaryRenderTexture, destination, this.material);
						this.ReleaseTemporaryRenderTexture(temporaryRenderTexture);
						return;
					}
					else
					{
						RenderTexture temporaryRenderTexture2 = this.GetTemporaryRenderTexture(width, height);
						Graphics.Blit(source, temporaryRenderTexture2, this.material, 0);
						if (this.glow == 0f)
						{
							Graphics.Blit(temporaryRenderTexture2, destination, this.material, 1);
						}
						else
						{
							RenderTexture temporaryRenderTexture3 = this.GetTemporaryRenderTexture(width, height);
							Graphics.Blit(temporaryRenderTexture2, temporaryRenderTexture3, this.material, 1);
							Graphics.Blit(temporaryRenderTexture3, destination, this.material, 2);
							this.ReleaseTemporaryRenderTexture(temporaryRenderTexture3);
						}
						this.ReleaseTemporaryRenderTexture(temporaryRenderTexture2);
					}
				}
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000AF880 File Offset: 0x000ADA80
		private void LoadShader()
		{
			this.shader = Resources.Load<Shader>(string.Format("Shaders/AdvancedNightVision{0}", this.shaderPasses.ToString()));
			AdvancedNightVisionHelper.IsSupported(this.shader, this);
			this.material = new Material(this.shader);
			this.ReleaseAllTemporaryRenderTextures();
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000AF8D8 File Offset: 0x000ADAD8
		private RenderTexture GetTemporaryRenderTexture(int width, int height)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, Application.isMobilePlatform ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
			this.renderTextures.Add(temporary);
			return temporary;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000AF907 File Offset: 0x000ADB07
		private void ReleaseTemporaryRenderTexture(RenderTexture renderTexture)
		{
			this.renderTextures.Remove(renderTexture);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000AF91C File Offset: 0x000ADB1C
		private void ReleaseAllTemporaryRenderTextures()
		{
			while (this.renderTextures.Count > 0)
			{
				int index = this.renderTextures.Count - 1;
				RenderTexture temp = this.renderTextures[index];
				this.renderTextures.RemoveAt(index);
				RenderTexture.ReleaseTemporary(temp);
			}
		}

		// Token: 0x040020F2 RID: 8434
		[SerializeField]
		private AdvancedNightVision.ShaderPasses shaderPasses = AdvancedNightVision.ShaderPasses.MultiPass;

		// Token: 0x040020F3 RID: 8435
		[SerializeField]
		private float blurPasses = 2f;

		// Token: 0x040020F4 RID: 8436
		[SerializeField]
		private AdvancedNightVision.RenderTextureResolutions renderTextureResolution = AdvancedNightVision.RenderTextureResolutions.Normal;

		// Token: 0x040020F5 RID: 8437
		[SerializeField]
		private float luminanceAmount = 1f;

		// Token: 0x040020F6 RID: 8438
		[SerializeField]
		private float exposure = 1f;

		// Token: 0x040020F7 RID: 8439
		[SerializeField]
		private Vector3 rgbOffset = new Vector3(10f, 1f, -10f);

		// Token: 0x040020F8 RID: 8440
		[SerializeField]
		private Color tint = new Color(0.258f, 1f, 0.188f);

		// Token: 0x040020F9 RID: 8441
		[SerializeField]
		private float brightness;

		// Token: 0x040020FA RID: 8442
		[SerializeField]
		private float contrast;

		// Token: 0x040020FB RID: 8443
		[SerializeField]
		private float saturation = 0.5f;

		// Token: 0x040020FC RID: 8444
		[SerializeField]
		private int distortionsPasses = 6;

		// Token: 0x040020FD RID: 8445
		[SerializeField]
		private float chromaticAberration = 2f;

		// Token: 0x040020FE RID: 8446
		[SerializeField]
		private float barrelDistortion = 10f;

		// Token: 0x040020FF RID: 8447
		[SerializeField]
		private bool analogTV = true;

		// Token: 0x04002100 RID: 8448
		[SerializeField]
		private float noise = 3f;

		// Token: 0x04002101 RID: 8449
		[SerializeField]
		private float scanline = 1f;

		// Token: 0x04002102 RID: 8450
		[SerializeField]
		private bool digitalTV = true;

		// Token: 0x04002103 RID: 8451
		[SerializeField]
		private AdvancedNightVision.ColorControls colorControlType = AdvancedNightVision.ColorControls.Advanced;

		// Token: 0x04002104 RID: 8452
		[SerializeField]
		private AdvancedNightVision.ChromaticAberrationTypes chromaticAberrationType = AdvancedNightVision.ChromaticAberrationTypes.Advanced;

		// Token: 0x04002105 RID: 8453
		[SerializeField]
		private AdvancedNightVision.VignetteTypes vignetteType = AdvancedNightVision.VignetteTypes.Screen;

		// Token: 0x04002106 RID: 8454
		[SerializeField]
		private float vignetteScale = 10f;

		// Token: 0x04002107 RID: 8455
		[SerializeField]
		private float vignetteSoftness = 0.1f;

		// Token: 0x04002108 RID: 8456
		[SerializeField]
		private float glow = 4f;

		// Token: 0x04002109 RID: 8457
		[SerializeField]
		private float digitalTVNoiseThreshold = 0.1f;

		// Token: 0x0400210A RID: 8458
		[SerializeField]
		private float digitalTVNoiseMaxOffset = 0.1f;

		// Token: 0x0400210B RID: 8459
		private List<RenderTexture> renderTextures = new List<RenderTexture>();

		// Token: 0x0400210C RID: 8460
		private Material material;

		// Token: 0x0400210D RID: 8461
		private Shader shader;

		// Token: 0x0400210E RID: 8462
		private const string variableBlur = "_Blur";

		// Token: 0x0400210F RID: 8463
		private const string variableLuminanceAmount = "_LuminanceAmount";

		// Token: 0x04002110 RID: 8464
		private const string variableExposure = "_Exposure";

		// Token: 0x04002111 RID: 8465
		private const string variableRGBLum = "_RGBLum";

		// Token: 0x04002112 RID: 8466
		private const string variableTint = "_Tint";

		// Token: 0x04002113 RID: 8467
		private const string variableBrightness = "_Brightness";

		// Token: 0x04002114 RID: 8468
		private const string variableContrast = "_Contrast";

		// Token: 0x04002115 RID: 8469
		private const string variableSaturation = "_Saturation";

		// Token: 0x04002116 RID: 8470
		private const string variableDistortionsPasses = "_DistortionsPasses";

		// Token: 0x04002117 RID: 8471
		private const string variableChromaticAberration = "_ChromaticAberration";

		// Token: 0x04002118 RID: 8472
		private const string variableBarrelDistortion = "_BarrelDistortion";

		// Token: 0x04002119 RID: 8473
		private const string variableNoise = "_Noise";

		// Token: 0x0400211A RID: 8474
		private const string variableScanline = "_Scanline";

		// Token: 0x0400211B RID: 8475
		private const string variableVignetteType = "_VignetteType";

		// Token: 0x0400211C RID: 8476
		private const string variableVignetteScale = "_VignetteScale";

		// Token: 0x0400211D RID: 8477
		private const string variableVignetteSoftness = "_VignetteSoftness";

		// Token: 0x0400211E RID: 8478
		private const string variableGlow = "_Glow";

		// Token: 0x0400211F RID: 8479
		private const string variableDigitalTVNoiseThreshold = "_DigitalTVNoiseThreshold";

		// Token: 0x04002120 RID: 8480
		private const string variableDigitalTVNoiseMaxOffset = "_DigitalTVNoiseMaxOffset";

		// Token: 0x04002121 RID: 8481
		private const string keywordBlur = "BLUR_ENABLED";

		// Token: 0x04002122 RID: 8482
		private const string keywordColorControlAdvanced = "COLORCONTROL_ADVANCED";

		// Token: 0x04002123 RID: 8483
		private const string keywordChromaticNone = "CHROMATIC_NONE";

		// Token: 0x04002124 RID: 8484
		private const string keywordChromaticSimple = "CHROMATIC_SIMPLE";

		// Token: 0x04002125 RID: 8485
		private const string keywordChromaticAdvanced = "CHROMATIC_ADVANCED";

		// Token: 0x04002126 RID: 8486
		private const string keywordVignetteNone = "VIGNETTE_NONE";

		// Token: 0x04002127 RID: 8487
		private const string keywordVignetteScreen = "VIGNETTE_SCREEN";

		// Token: 0x04002128 RID: 8488
		private const string keywordVignetteMonocular = "VIGNETTE_MONOCULAR";

		// Token: 0x04002129 RID: 8489
		private const string keywordVignetteBinocular = "VIGNETTE_BINOCULAR";

		// Token: 0x0400212A RID: 8490
		private const string keywordAnalogTV = "ANALOGTV_ENABLED";

		// Token: 0x0400212B RID: 8491
		private const string keywordDigitalTV = "DIGITALTV_ENABLED";

		// Token: 0x0400212C RID: 8492
		private const string keywordGlow = "GLOW_ENABLED";

		// Token: 0x02000791 RID: 1937
		public enum ShaderPasses
		{
			// Token: 0x0400298D RID: 10637
			OnePass = 1,
			// Token: 0x0400298E RID: 10638
			MultiPass = 3
		}

		// Token: 0x02000792 RID: 1938
		public enum RenderTextureResolutions
		{
			// Token: 0x04002990 RID: 10640
			Normal = 1,
			// Token: 0x04002991 RID: 10641
			Half,
			// Token: 0x04002992 RID: 10642
			Quarter = 4
		}

		// Token: 0x02000793 RID: 1939
		public enum ColorControls
		{
			// Token: 0x04002994 RID: 10644
			Simple,
			// Token: 0x04002995 RID: 10645
			Advanced
		}

		// Token: 0x02000794 RID: 1940
		public enum VignetteTypes
		{
			// Token: 0x04002997 RID: 10647
			None,
			// Token: 0x04002998 RID: 10648
			Screen,
			// Token: 0x04002999 RID: 10649
			Monocular,
			// Token: 0x0400299A RID: 10650
			Binocular
		}

		// Token: 0x02000795 RID: 1941
		public enum ChromaticAberrationTypes
		{
			// Token: 0x0400299C RID: 10652
			None,
			// Token: 0x0400299D RID: 10653
			Simple,
			// Token: 0x0400299E RID: 10654
			Advanced
		}
	}
}
