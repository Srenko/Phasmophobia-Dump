using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000178 RID: 376
public class BrightnessManager : MonoBehaviour
{
	// Token: 0x060009EF RID: 2543 RVA: 0x0003D439 File Offset: 0x0003B639
	private void Start()
	{
		this.volume.profile.TryGetSettings<ColorGrading>(out this.colorGradingLayer);
		this.SetScreenResolution();
		this.SetDefaultBrightness();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0003D460 File Offset: 0x0003B660
	private void SetScreenResolution()
	{
		if (!XRDevice.isPresent)
		{
			if (PlayerPrefs.GetInt("resolutionValue") == 0)
			{
				PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
				Screen.SetResolution(Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].width, Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].height, true);
				return;
			}
			Screen.SetResolution(Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].width, Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].height, PlayerPrefs.GetInt("fullscreenType") == 1);
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0003D513 File Offset: 0x0003B713
	private void SetDefaultBrightness()
	{
		if (!XRDevice.isPresent)
		{
			this.colorGradingLayer.postExposure.value = this.slider.value;
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0003D537 File Offset: 0x0003B737
	public void SliderValueChanged()
	{
		this.colorGradingLayer.postExposure.value = this.slider.value;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0003D554 File Offset: 0x0003B754
	public void Confirm()
	{
		PlayerPrefs.SetFloat("brightnessValue", this.slider.value);
		SceneManager.LoadScene("Menu_New");
	}

	// Token: 0x04000A10 RID: 2576
	[SerializeField]
	private Slider slider;

	// Token: 0x04000A11 RID: 2577
	[SerializeField]
	private PostProcessVolume volume;

	// Token: 0x04000A12 RID: 2578
	private ColorGrading colorGradingLayer;
}
