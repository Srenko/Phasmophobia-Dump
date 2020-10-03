using System;
using Photon;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x0200018A RID: 394
public class PCManager : Photon.MonoBehaviour
{
	// Token: 0x06000A98 RID: 2712 RVA: 0x00041F28 File Offset: 0x00040128
	private void Awake()
	{
		if (XRDevice.isPresent)
		{
			this.PCButton.interactable = false;
			this.PCButtonText.color = this.PCButton.colors.disabledColor;
			base.enabled = false;
		}
		else
		{
			if (PlayerPrefs.GetInt("fovValue") == 0)
			{
				PlayerPrefs.SetInt("fovValue", 70);
			}
			if (PlayerPrefs.GetFloat("sensitivityValue") == 0f)
			{
				PlayerPrefs.SetFloat("sensitivityValue", 1f);
			}
			if (PlayerPrefs.GetFloat("brightnessValue") == 0f)
			{
				PlayerPrefs.SetFloat("brightnessValue", 1f);
			}
			if (PlayerPrefs.GetFloat("cursorBrightnessValue") == 0f)
			{
				PlayerPrefs.SetFloat("cursorBrightnessValue", 1f);
			}
			if (!PlayerPrefs.HasKey("vsyncValue"))
			{
				PlayerPrefs.SetFloat("vsyncValue", 1f);
			}
		}
		QualitySettings.SetQualityLevel(0, true);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0004200C File Offset: 0x0004020C
	private void Start()
	{
		if (!XRDevice.isPresent)
		{
			this.LoadValues();
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0004201C File Offset: 0x0004021C
	private void LoadValues()
	{
		this.volumetricLightingValue = PlayerPrefs.GetInt("volumetricLightingValue");
		this.fovValue = PlayerPrefs.GetInt("fovValue");
		this.fovSlider.value = (float)this.fovValue;
		this.vsyncValue = PlayerPrefs.GetInt("vsyncValue");
		this.reflectionValue = PlayerPrefs.GetInt("reflectionValue");
		this.sensitivityValue = PlayerPrefs.GetFloat("sensitivityValue");
		this.sensitivitySlider.value = this.sensitivityValue;
		this.cursorBrightnessValue = PlayerPrefs.GetFloat("cursorBrightnessValue");
		this.cursorBrightnessSlider.value = this.cursorBrightnessValue;
		this.invertedXLookValue = PlayerPrefs.GetInt("invertedXLookValue");
		this.invertedYLookValue = PlayerPrefs.GetInt("invertedYLookValue");
		this.localPushToTalkValue = PlayerPrefs.GetInt("localPushToTalkValue");
		this.UpdateUIValues();
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x000420F4 File Offset: 0x000402F4
	public void SetValues()
	{
		PlayerPrefs.SetInt("volumetricLightingValue", this.volumetricLightingValue);
		PlayerPrefs.SetInt("fovValue", this.fovValue);
		PlayerPrefs.SetInt("vsyncValue", this.vsyncValue);
		PlayerPrefs.SetFloat("sensitivityValue", this.sensitivityValue);
		PlayerPrefs.SetInt("reflectionValue", this.reflectionValue);
		PlayerPrefs.SetFloat("cursorBrightnessValue", this.cursorBrightnessValue);
		PlayerPrefs.SetInt("invertedXLookValue", this.invertedXLookValue);
		PlayerPrefs.SetInt("invertedYLookValue", this.invertedYLookValue);
		PlayerPrefs.SetInt("localPushToTalkValue", this.localPushToTalkValue);
		if (XRDevice.isPresent)
		{
			return;
		}
		QualitySettings.vSyncCount = this.vsyncValue;
		if (this.vsyncValue == 0)
		{
			Application.targetFrameRate = 240;
		}
		MainManager.instance.localPlayer.cam.fieldOfView = (float)PlayerPrefs.GetInt("fovValue");
		MainManager.instance.localPlayer.pcPropGrab.ChangeItemSpotWithFOV((float)PlayerPrefs.GetInt("fovValue"));
		MainManager.instance.localPlayer.itemSway.SetPosition();
		bool flag = PlayerPrefs.GetInt("invertedXLookValue") == 1;
		bool flag2 = PlayerPrefs.GetInt("invertedYLookValue") == 1;
		MainManager.instance.localPlayer.firstPersonController.m_MouseLook.XSensitivity = (flag ? (-PlayerPrefs.GetFloat("sensitivityValue")) : PlayerPrefs.GetFloat("sensitivityValue"));
		MainManager.instance.localPlayer.firstPersonController.m_MouseLook.YSensitivity = (flag2 ? (-PlayerPrefs.GetFloat("sensitivityValue")) : PlayerPrefs.GetFloat("sensitivityValue"));
		MainManager.instance.localPlayer.pcCanvas.UpdateCursorBrightness();
		if (PlayerPrefs.GetInt("volumetricLightingValue") != 0)
		{
			MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricCamera>().enabled = true;
			MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricImageEffect>().enabled = true;
			if (PlayerPrefs.GetInt("volumetricLightingValue") == 1)
			{
				MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricCamera>().resolution = HxVolumetricCamera.Resolution.quarter;
			}
			else if (PlayerPrefs.GetInt("volumetricLightingValue") == 2)
			{
				MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricCamera>().resolution = HxVolumetricCamera.Resolution.half;
			}
			else if (PlayerPrefs.GetInt("volumetricLightingValue") == 3)
			{
				MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricCamera>().resolution = HxVolumetricCamera.Resolution.full;
			}
		}
		else
		{
			MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricCamera>().enabled = false;
			MainManager.instance.localPlayer.headObject.GetComponent<HxVolumetricImageEffect>().enabled = false;
		}
		ScreenSpaceReflections screenSpaceReflections = null;
		MainManager.instance.localPlayer.postProcessingVolume.profile.TryGetSettings<ScreenSpaceReflections>(out screenSpaceReflections);
		screenSpaceReflections.enabled.value = (PlayerPrefs.GetInt("reflectionValue") == 1);
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x000423C9 File Offset: 0x000405C9
	public void ApplyButton()
	{
		this.SetValues();
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x000423D4 File Offset: 0x000405D4
	private void UpdateUIValues()
	{
		this.volumetricLightingValueText.text = this.GetVolumetricLightingText();
		this.fovValueText.text = this.fovValue.ToString();
		this.vsyncValueText.text = this.GetVSyncText();
		this.reflectionValueText.text = this.GetReflectionText();
		this.sensitivityValueText.text = this.sensitivityValue.ToString("0.0");
		this.cursorBrightnessValueText.text = this.cursorBrightnessValue.ToString("0.0");
		this.invertedXLookValueText.text = this.GetInvertedXLookText();
		this.invertedYLookValueText.text = this.GetInvertedYLookText();
		this.localPushToTalkValueText.text = this.GetlocalPushToTalkText();
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00042493 File Offset: 0x00040693
	public void VolumetricLightingUIChangeValue(int value)
	{
		this.volumetricLightingValue += value;
		if (this.volumetricLightingValue < 0)
		{
			this.volumetricLightingValue = 0;
		}
		else if (this.volumetricLightingValue > 3)
		{
			this.volumetricLightingValue = 3;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x000424CB File Offset: 0x000406CB
	public void FOVChangeValue()
	{
		this.fovValue = (int)this.fovSlider.value;
		this.UpdateUIValues();
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x000424E5 File Offset: 0x000406E5
	public void VSyncChangeValue(int value)
	{
		this.vsyncValue += value;
		if (this.vsyncValue < 0)
		{
			this.vsyncValue = 0;
		}
		else if (this.vsyncValue > 1)
		{
			this.vsyncValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0004251D File Offset: 0x0004071D
	public void ReflectionChangeValue(int value)
	{
		this.reflectionValue += value;
		if (this.reflectionValue < 0)
		{
			this.reflectionValue = 0;
		}
		else if (this.reflectionValue > 1)
		{
			this.reflectionValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00042555 File Offset: 0x00040755
	public void SensitivityChangeValue()
	{
		this.sensitivityValue = this.sensitivitySlider.value;
		this.UpdateUIValues();
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0004256E File Offset: 0x0004076E
	public void CursorBrightnessChangeValue()
	{
		this.cursorBrightnessValue = this.cursorBrightnessSlider.value;
		this.UpdateUIValues();
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00042587 File Offset: 0x00040787
	public void InvertedXLookChangeValue(int value)
	{
		this.invertedXLookValue += value;
		if (this.invertedXLookValue < 0)
		{
			this.invertedXLookValue = 0;
		}
		else if (this.invertedXLookValue > 1)
		{
			this.invertedXLookValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x000425BF File Offset: 0x000407BF
	public void InvertedYLookChangeValue(int value)
	{
		this.invertedYLookValue += value;
		if (this.invertedYLookValue < 0)
		{
			this.invertedYLookValue = 0;
		}
		else if (this.invertedYLookValue > 1)
		{
			this.invertedYLookValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x000425F7 File Offset: 0x000407F7
	public void LocalPushToTalkChangeValue(int value)
	{
		this.localPushToTalkValue += value;
		if (this.localPushToTalkValue < 0)
		{
			this.localPushToTalkValue = 0;
		}
		else if (this.localPushToTalkValue > 1)
		{
			this.localPushToTalkValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00042630 File Offset: 0x00040830
	private string GetVolumetricLightingText()
	{
		string result = string.Empty;
		if (this.volumetricLightingValue == 0)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		else if (this.volumetricLightingValue == 1)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Low");
		}
		else if (this.volumetricLightingValue == 2)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_Medium");
		}
		else if (this.volumetricLightingValue == 3)
		{
			result = LocalisationSystem.GetLocalisedValue("Graphics_High");
		}
		return result;
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00042699 File Offset: 0x00040899
	private string GetVSyncText()
	{
		if (this.vsyncValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return LocalisationSystem.GetLocalisedValue("Graphics_Off");
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x000426B8 File Offset: 0x000408B8
	private string GetReflectionText()
	{
		if (this.reflectionValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return LocalisationSystem.GetLocalisedValue("Graphics_Off");
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000426D7 File Offset: 0x000408D7
	private string GetInvertedXLookText()
	{
		if (this.invertedXLookValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return LocalisationSystem.GetLocalisedValue("Graphics_Off");
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x000426F6 File Offset: 0x000408F6
	private string GetInvertedYLookText()
	{
		if (this.invertedYLookValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Graphics_On");
		}
		return LocalisationSystem.GetLocalisedValue("Graphics_Off");
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00042715 File Offset: 0x00040915
	private string GetlocalPushToTalkText()
	{
		if (this.localPushToTalkValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Graphics_Off");
		}
		return LocalisationSystem.GetLocalisedValue("Graphics_On");
	}

	// Token: 0x04000ACC RID: 2764
	private int volumetricLightingValue;

	// Token: 0x04000ACD RID: 2765
	private int fovValue;

	// Token: 0x04000ACE RID: 2766
	private int vsyncValue;

	// Token: 0x04000ACF RID: 2767
	private float sensitivityValue;

	// Token: 0x04000AD0 RID: 2768
	private int reflectionValue;

	// Token: 0x04000AD1 RID: 2769
	private float cursorBrightnessValue;

	// Token: 0x04000AD2 RID: 2770
	private int invertedXLookValue;

	// Token: 0x04000AD3 RID: 2771
	private int invertedYLookValue;

	// Token: 0x04000AD4 RID: 2772
	private int localPushToTalkValue;

	// Token: 0x04000AD5 RID: 2773
	[SerializeField]
	private Button PCButton;

	// Token: 0x04000AD6 RID: 2774
	[SerializeField]
	private Text PCButtonText;

	// Token: 0x04000AD7 RID: 2775
	[SerializeField]
	private Text volumetricLightingValueText;

	// Token: 0x04000AD8 RID: 2776
	[SerializeField]
	private Text vsyncValueText;

	// Token: 0x04000AD9 RID: 2777
	[SerializeField]
	private Text reflectionValueText;

	// Token: 0x04000ADA RID: 2778
	[SerializeField]
	private Text invertedXLookValueText;

	// Token: 0x04000ADB RID: 2779
	[SerializeField]
	private Text invertedYLookValueText;

	// Token: 0x04000ADC RID: 2780
	[SerializeField]
	private Text localPushToTalkValueText;

	// Token: 0x04000ADD RID: 2781
	[SerializeField]
	private Slider fovSlider;

	// Token: 0x04000ADE RID: 2782
	[SerializeField]
	private Text fovValueText;

	// Token: 0x04000ADF RID: 2783
	[SerializeField]
	private Slider sensitivitySlider;

	// Token: 0x04000AE0 RID: 2784
	[SerializeField]
	private Text sensitivityValueText;

	// Token: 0x04000AE1 RID: 2785
	[SerializeField]
	private Slider cursorBrightnessSlider;

	// Token: 0x04000AE2 RID: 2786
	[SerializeField]
	private Text cursorBrightnessValueText;
}
