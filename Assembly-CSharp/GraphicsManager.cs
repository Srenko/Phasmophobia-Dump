using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x0200017F RID: 383
public class GraphicsManager : MonoBehaviour
{
	// Token: 0x06000A16 RID: 2582 RVA: 0x0003E460 File Offset: 0x0003C660
	private void Awake()
	{
		QualitySettings.SetQualityLevel(0, true);
		if (PlayerPrefs.GetInt("GraphicsSet") == 0)
		{
			PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
			PlayerPrefs.SetInt("fullscreenType", 1);
			PlayerPrefs.SetInt("taaValue", 1);
			PlayerPrefs.SetInt("shadowType", 1);
			PlayerPrefs.SetInt("shadowRes", 2);
			PlayerPrefs.SetInt("textureRes", 0);
			PlayerPrefs.SetInt("anisotropic", 1);
			PlayerPrefs.SetInt("ambientOcclusion", 1);
			PlayerPrefs.SetInt("bloom", 0);
			PlayerPrefs.SetInt("GraphicsSet", 1);
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0003E4F6 File Offset: 0x0003C6F6
	private void Start()
	{
		this.LoadValues();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0003E500 File Offset: 0x0003C700
	private void LoadValues()
	{
		this.resolutionValue = PlayerPrefs.GetInt("resolutionValue");
		this.fullscreenType = PlayerPrefs.GetInt("fullscreenType");
		this.taaValue = PlayerPrefs.GetInt("taaValue");
		this.shadowType = PlayerPrefs.GetInt("shadowType");
		this.shadowRes = PlayerPrefs.GetInt("shadowRes");
		this.textureRes = PlayerPrefs.GetInt("textureRes");
		this.anisotropic = PlayerPrefs.GetInt("anisotropic");
		this.ambientOcclusion = PlayerPrefs.GetInt("ambientOcclusion");
		this.bloom = PlayerPrefs.GetInt("bloom");
		this.brightnessValue = PlayerPrefs.GetFloat("brightnessValue");
		this.brightnessSlider.value = this.brightnessValue;
		if (this.resolutionValue > Screen.resolutions.Length - 1 || this.resolutionValue < 0)
		{
			this.resolutionValue = Screen.resolutions.Length - 1;
			PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
		}
		QualitySettings.shadows = this.GetShadowQuality();
		QualitySettings.shadowResolution = this.GetShadowResolution();
		QualitySettings.masterTextureLimit = this.textureRes;
		QualitySettings.anisotropicFiltering = this.GetAnisotropic();
		AmbientOcclusion ambientOcclusion = null;
		this.sceneCamVolume.profile.TryGetSettings<AmbientOcclusion>(out ambientOcclusion);
		ambientOcclusion.enabled.value = (PlayerPrefs.GetInt("ambientOcclusion") == 1);
		Bloom bloom = null;
		this.sceneCamVolume.profile.TryGetSettings<Bloom>(out bloom);
		bloom.enabled.value = (PlayerPrefs.GetInt("bloom") == 0);
		if (PlayerPrefs.GetInt("taaValue") == 0)
		{
			this.sceneCamLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
		}
		else
		{
			this.sceneCamLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
		}
		ColorGrading colorGrading = null;
		this.sceneCamVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
		colorGrading.postExposure.value = PlayerPrefs.GetFloat("brightnessValue");
		this.UpdateUIValues();
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
	private void SetValues()
	{
		PlayerPrefs.SetInt("resolutionValue", this.resolutionValue);
		PlayerPrefs.SetInt("fullscreenType", this.fullscreenType);
		PlayerPrefs.SetInt("taaValue", this.taaValue);
		PlayerPrefs.SetInt("shadowType", this.shadowType);
		PlayerPrefs.SetInt("shadowRes", this.shadowRes);
		PlayerPrefs.SetInt("textureRes", this.textureRes);
		PlayerPrefs.SetInt("anisotropic", this.anisotropic);
		PlayerPrefs.SetInt("ambientOcclusion", this.ambientOcclusion);
		PlayerPrefs.SetInt("bloom", this.bloom);
		QualitySettings.shadows = this.GetShadowQuality();
		QualitySettings.shadowResolution = this.GetShadowResolution();
		QualitySettings.masterTextureLimit = this.textureRes;
		QualitySettings.anisotropicFiltering = this.GetAnisotropic();
		if (this.resolutionValue > Screen.resolutions.Length - 1 || this.resolutionValue < 0)
		{
			this.resolutionValue = Screen.resolutions.Length - 1;
			PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
		}
		Screen.SetResolution(Screen.resolutions[this.resolutionValue].width, Screen.resolutions[this.resolutionValue].height, this.fullscreenType == 1);
		if (!XRDevice.isPresent)
		{
			AmbientOcclusion ambientOcclusion = null;
			MainManager.instance.localPlayer.postProcessingVolume.profile.TryGetSettings<AmbientOcclusion>(out ambientOcclusion);
			ambientOcclusion.enabled.value = (PlayerPrefs.GetInt("ambientOcclusion") == 1);
			this.sceneCamVolume.profile.TryGetSettings<AmbientOcclusion>(out ambientOcclusion);
			ambientOcclusion.enabled.value = (PlayerPrefs.GetInt("ambientOcclusion") == 1);
			if (PlayerPrefs.GetInt("taaValue") == 0)
			{
				MainManager.instance.localPlayer.postProcessingLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
				this.sceneCamLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
			}
			else
			{
				MainManager.instance.localPlayer.postProcessingLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
				this.sceneCamLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
			}
		}
		PlayerPrefs.SetFloat("brightnessValue", this.brightnessValue);
		ColorGrading colorGrading = null;
		MainManager.instance.localPlayer.postProcessingVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
		colorGrading.postExposure.value = PlayerPrefs.GetFloat("brightnessValue");
		this.sceneCamVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
		colorGrading.postExposure.value = PlayerPrefs.GetFloat("brightnessValue");
		Bloom bloom = null;
		MainManager.instance.localPlayer.postProcessingVolume.profile.TryGetSettings<Bloom>(out bloom);
		bloom.enabled.value = (PlayerPrefs.GetInt("bloom") == 0);
		this.sceneCamVolume.profile.TryGetSettings<Bloom>(out bloom);
		bloom.enabled.value = (PlayerPrefs.GetInt("bloom") == 0);
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0003E99C File Offset: 0x0003CB9C
	public void ApplyButton()
	{
		this.SetValues();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
	private void UpdateUIValues()
	{
		this.resolutionValueText.text = this.GetResolutionText();
		this.fullscreenTypeValueText.text = this.GetFullscreenText();
		this.taaValueText.text = this.GetTaaText();
		this.shadowTypeValueText.text = this.GetShadowTypeText();
		this.shadowResValueText.text = this.GetShadowResolutionText();
		this.textureResValueText.text = this.GetTextureResolutionText();
		this.anisotropicValueText.text = this.GetAnisotropicText();
		this.ambientOcclusionValueText.text = this.GetAmbientOcclusionText();
		this.bloomValueText.text = this.GetBloomText();
		this.brightnessValueText.text = this.brightnessValue.ToString("0.0");
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0003EA68 File Offset: 0x0003CC68
	public void ResolutionChangeValue(int value)
	{
		this.resolutionValue += value;
		if (this.resolutionValue > Screen.resolutions.Length - 1)
		{
			this.resolutionValue = Screen.resolutions.Length - 1;
		}
		else if (this.resolutionValue < 0)
		{
			this.resolutionValue = 0;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0003EABB File Offset: 0x0003CCBB
	public void FullscreenTypeChangeValue(int value)
	{
		this.fullscreenType += value;
		if (this.fullscreenType < 0)
		{
			this.fullscreenType = 0;
		}
		else if (this.fullscreenType > 0)
		{
			this.fullscreenType = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0003EAF3 File Offset: 0x0003CCF3
	public void TAAChangeValue(int value)
	{
		this.taaValue += value;
		if (this.taaValue < 0)
		{
			this.taaValue = 0;
		}
		else if (this.taaValue > 1)
		{
			this.taaValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0003EB2B File Offset: 0x0003CD2B
	public void ShadowTypeChangeValue(int value)
	{
		this.shadowType += value;
		if (this.shadowType < 0)
		{
			this.shadowType = 0;
		}
		else if (this.shadowType > 0)
		{
			this.shadowType = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0003EB63 File Offset: 0x0003CD63
	public void ShadowResolutionChangeValue(int value)
	{
		this.shadowRes += value;
		if (this.shadowRes < 0)
		{
			this.shadowRes = 0;
		}
		else if (this.shadowRes > 3)
		{
			this.shadowRes = 3;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0003EB9B File Offset: 0x0003CD9B
	public void TextureResolutionChangeValue(int value)
	{
		this.textureRes += value;
		if (this.textureRes < 0)
		{
			this.textureRes = 0;
		}
		else if (this.textureRes > 3)
		{
			this.textureRes = 3;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0003EBD3 File Offset: 0x0003CDD3
	public void AnisotropicChangeValue(int value)
	{
		this.anisotropic += value;
		if (this.anisotropic < 0)
		{
			this.anisotropic = 0;
		}
		else if (this.anisotropic > 0)
		{
			this.anisotropic = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0003EC0B File Offset: 0x0003CE0B
	public void AmbientOcclusionChangeValue(int value)
	{
		this.ambientOcclusion += value;
		if (this.ambientOcclusion < 0)
		{
			this.ambientOcclusion = 0;
		}
		else if (this.ambientOcclusion > 0)
		{
			this.ambientOcclusion = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0003EC43 File Offset: 0x0003CE43
	public void BloomChangeValue(int value)
	{
		this.bloom += value;
		if (this.bloom < 0)
		{
			this.bloom = 0;
		}
		else if (this.bloom > 0)
		{
			this.bloom = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003EC7B File Offset: 0x0003CE7B
	public void BrightnessChangeValue()
	{
		this.brightnessValue = this.brightnessSlider.value;
		this.UpdateUIValues();
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0003EC94 File Offset: 0x0003CE94
	private string GetResolutionText()
	{
		if (this.resolutionValue > Screen.resolutions.Length - 1 || this.resolutionValue < 0)
		{
			this.resolutionValue = Screen.resolutions.Length - 1;
			PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
		}
		return string.Concat(new object[]
		{
			Screen.resolutions[this.resolutionValue].width.ToString(),
			"x",
			Screen.resolutions[this.resolutionValue].height.ToString(),
			"@",
			Screen.resolutions[this.resolutionValue].refreshRate,
			"hz"
		});
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0003ED5D File Offset: 0x0003CF5D
	private string GetFullscreenText()
	{
		if (this.fullscreenType != 0)
		{
			return LocalisationSystem.GetLocalisedValue("PC_Fullscreen");
		}
		return LocalisationSystem.GetLocalisedValue("PC_Windowed");
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0003ED7C File Offset: 0x0003CF7C
	private string GetTaaText()
	{
		string result = string.Empty;
		if (this.taaValue == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return result;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0003EDB0 File Offset: 0x0003CFB0
	private string GetShadowTypeText()
	{
		string result = string.Empty;
		if (this.shadowType == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Hard");
		}
		else if (this.shadowType == 1)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Soft");
		}
		return result;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0003EDF0 File Offset: 0x0003CFF0
	private string GetShadowResolutionText()
	{
		string result = string.Empty;
		if (this.shadowRes == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Low");
		}
		else if (this.shadowRes == 1)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Medium");
		}
		else if (this.shadowRes == 2)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_High");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_VeryHigh");
		}
		return result;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0003EE50 File Offset: 0x0003D050
	private string GetTextureResolutionText()
	{
		string result = string.Empty;
		if (this.textureRes == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Full");
		}
		else if (this.textureRes == 1)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Half");
		}
		else if (this.textureRes == 2)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Quarter");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Eighth");
		}
		return result;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0003EEB0 File Offset: 0x0003D0B0
	private string GetAnisotropicText()
	{
		string result = string.Empty;
		if (this.anisotropic == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return result;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
	private string GetAmbientOcclusionText()
	{
		string result = string.Empty;
		if (this.ambientOcclusion == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return result;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0003EF18 File Offset: 0x0003D118
	private string GetBloomText()
	{
		string result = string.Empty;
		if (this.bloom == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		else
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		return result;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003EF4C File Offset: 0x0003D14C
	private ShadowQuality GetShadowQuality()
	{
		if (this.shadowType == 0)
		{
			return ShadowQuality.HardOnly;
		}
		return ShadowQuality.All;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0003EF59 File Offset: 0x0003D159
	private ShadowResolution GetShadowResolution()
	{
		if (this.shadowRes == 0)
		{
			return ShadowResolution.Low;
		}
		if (this.shadowRes == 1)
		{
			return ShadowResolution.Medium;
		}
		if (this.shadowRes == 2)
		{
			return ShadowResolution.High;
		}
		return ShadowResolution.VeryHigh;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003EF7C File Offset: 0x0003D17C
	private AnisotropicFiltering GetAnisotropic()
	{
		if (this.anisotropic == 0)
		{
			return AnisotropicFiltering.Disable;
		}
		return AnisotropicFiltering.ForceEnable;
	}

	// Token: 0x04000A3F RID: 2623
	private int resolutionValue;

	// Token: 0x04000A40 RID: 2624
	private int fullscreenType;

	// Token: 0x04000A41 RID: 2625
	private int taaValue;

	// Token: 0x04000A42 RID: 2626
	private int shadowType;

	// Token: 0x04000A43 RID: 2627
	private int shadowRes;

	// Token: 0x04000A44 RID: 2628
	private int textureRes;

	// Token: 0x04000A45 RID: 2629
	private int anisotropic;

	// Token: 0x04000A46 RID: 2630
	private int ambientOcclusion;

	// Token: 0x04000A47 RID: 2631
	private float brightnessValue;

	// Token: 0x04000A48 RID: 2632
	private int bloom;

	// Token: 0x04000A49 RID: 2633
	[SerializeField]
	private Text resolutionValueText;

	// Token: 0x04000A4A RID: 2634
	[SerializeField]
	private Text fullscreenTypeValueText;

	// Token: 0x04000A4B RID: 2635
	[SerializeField]
	private Text taaValueText;

	// Token: 0x04000A4C RID: 2636
	[SerializeField]
	private Text shadowTypeValueText;

	// Token: 0x04000A4D RID: 2637
	[SerializeField]
	private Text shadowResValueText;

	// Token: 0x04000A4E RID: 2638
	[SerializeField]
	private Text textureResValueText;

	// Token: 0x04000A4F RID: 2639
	[SerializeField]
	private Text anisotropicValueText;

	// Token: 0x04000A50 RID: 2640
	[SerializeField]
	private Text ambientOcclusionValueText;

	// Token: 0x04000A51 RID: 2641
	[SerializeField]
	private Text bloomValueText;

	// Token: 0x04000A52 RID: 2642
	[SerializeField]
	private Slider brightnessSlider;

	// Token: 0x04000A53 RID: 2643
	[SerializeField]
	private Text brightnessValueText;

	// Token: 0x04000A54 RID: 2644
	[SerializeField]
	private PostProcessVolume sceneCamVolume;

	// Token: 0x04000A55 RID: 2645
	[SerializeField]
	private PostProcessLayer sceneCamLayer;
}
