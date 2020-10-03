using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;

// Token: 0x02000135 RID: 309
public class Brightness : MonoBehaviour
{
	// Token: 0x06000827 RID: 2087 RVA: 0x00030F90 File Offset: 0x0002F190
	private void Start()
	{
		this.ApplyBrightnessSetting();
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00030F98 File Offset: 0x0002F198
	public void ApplyBrightnessSetting()
	{
		if (!XRDevice.isPresent)
		{
			this.pcProfile.TryGetSettings<ColorGrading>(out this.colorGradingLayer);
			this.colorGradingLayer.postExposure.value = PlayerPrefs.GetFloat("brightnessValue");
			return;
		}
		this.vrProfile.TryGetSettings<ColorGrading>(out this.colorGradingLayer);
		this.colorGradingLayer.postExposure.value = ((PlayerPrefs.GetFloat("brightnessValue") == 0f) ? 0.8f : PlayerPrefs.GetFloat("brightnessValue"));
	}

	// Token: 0x0400083B RID: 2107
	[SerializeField]
	private PostProcessProfile pcProfile;

	// Token: 0x0400083C RID: 2108
	[SerializeField]
	private PostProcessProfile vrProfile;

	// Token: 0x0400083D RID: 2109
	private ColorGrading colorGradingLayer;
}
