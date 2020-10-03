using System;
using Photon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000197 RID: 407
public class VRManager : Photon.MonoBehaviour
{
	// Token: 0x06000AF9 RID: 2809 RVA: 0x000449AC File Offset: 0x00042BAC
	private void Awake()
	{
		if (!XRDevice.isPresent)
		{
			this.VRButton.interactable = false;
			this.VRButtonText.color = this.VRButton.colors.disabledColor;
			base.enabled = false;
		}
		QualitySettings.SetQualityLevel(0, true);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x000449F8 File Offset: 0x00042BF8
	private void Start()
	{
		this.LoadValues();
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00044A00 File Offset: 0x00042C00
	private void LoadValues()
	{
		this.locomotionValue = PlayerPrefs.GetInt("locomotionValue");
		this.turningValue = PlayerPrefs.GetInt("turningValue");
		this.turningAngleValue = PlayerPrefs.GetInt("turningAngleValue");
		this.movementDirectionValue = PlayerPrefs.GetInt("movementDirectionValue");
		this.grabTypeValue = PlayerPrefs.GetInt("grabTypeValue");
		this.SmoothCamValue = PlayerPrefs.GetInt("SmoothCamValue");
		this.gripTypeValue = PlayerPrefs.GetInt("gripTypeValue");
		this.teleportGrabValue = PlayerPrefs.GetInt("teleportGrabValue");
		this.turningSpeedValue = ((PlayerPrefs.GetInt("turningSpeedValue") == 0) ? 6 : PlayerPrefs.GetInt("turningSpeedValue"));
		this.controllerRotationXValue = PlayerPrefs.GetInt("controllerRotationXValue");
		this.controllerRotationYValue = PlayerPrefs.GetInt("controllerRotationYValue");
		this.controllerRotationZValue = PlayerPrefs.GetInt("controllerRotationZValue");
		this.UpdateUIValues();
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00044AE4 File Offset: 0x00042CE4
	public void SetValues()
	{
		PlayerPrefs.SetInt("locomotionValue", this.locomotionValue);
		PlayerPrefs.SetInt("turningValue", this.turningValue);
		PlayerPrefs.SetInt("turningAngleValue", this.turningAngleValue);
		PlayerPrefs.SetInt("movementDirectionValue", this.movementDirectionValue);
		PlayerPrefs.SetInt("grabTypeValue", this.grabTypeValue);
		PlayerPrefs.SetInt("SmoothCamValue", this.SmoothCamValue);
		PlayerPrefs.SetInt("gripTypeValue", this.gripTypeValue);
		PlayerPrefs.SetInt("teleportGrabValue", this.teleportGrabValue);
		PlayerPrefs.SetInt("turningSpeedValue", (this.turningSpeedValue == 0) ? 6 : this.turningSpeedValue);
		PlayerPrefs.SetInt("controllerRotationXValue", this.controllerRotationXValue);
		PlayerPrefs.SetInt("controllerRotationYValue", this.controllerRotationYValue);
		PlayerPrefs.SetInt("controllerRotationZValue", this.controllerRotationZValue);
		Object.FindObjectOfType<VRMovementSettings>().ApplySettings();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00044BC6 File Offset: 0x00042DC6
	public void ApplyButton()
	{
		this.SetValues();
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00044BD0 File Offset: 0x00042DD0
	private void UpdateUIValues()
	{
		this.locomotionValueText.text = this.GetLocomotionText();
		this.turningValueText.text = this.GetTurningText();
		this.turningAngleValueText.text = this.GetTurningAngleValueText();
		this.movementDirectionValueText.text = this.GetMovementDirectionValueText();
		this.grabTypeValueText.text = this.GetGrabTypeValueText();
		this.smoothCamValueText.text = this.GetSmoothCamValueText();
		this.gripTypeValueText.text = this.GetGripTypeValueText();
		this.teleportGrabValueText.text = this.GetTeleportGrabText();
		this.turningSpeedValueText.text = this.GetTurningSpeedValueText();
		this.controllerRotationXValueText.text = this.GetControllerXRotationValueText();
		this.controllerRotationYValueText.text = this.GetControllerYRotationValueText();
		this.controllerRotationZValueText.text = this.GetControllerZRotationValueText();
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x00044CA9 File Offset: 0x00042EA9
	public void LocomotionChangeValue(int value)
	{
		this.locomotionValue += value;
		if (this.locomotionValue < 0)
		{
			this.locomotionValue = 0;
		}
		else if (this.locomotionValue > 1)
		{
			this.locomotionValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00044CE1 File Offset: 0x00042EE1
	public void TurningChangeValue(int value)
	{
		this.turningValue += value;
		if (this.turningValue < 0)
		{
			this.turningValue = 0;
		}
		else if (this.turningValue > 1)
		{
			this.turningValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00044D19 File Offset: 0x00042F19
	public void TurningAngleChangeValue(int value)
	{
		this.turningAngleValue += value;
		if (this.turningAngleValue < 0)
		{
			this.turningAngleValue = 0;
		}
		else if (this.turningAngleValue > 3)
		{
			this.turningAngleValue = 3;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00044D51 File Offset: 0x00042F51
	public void TurningSpeedChangeValue(int value)
	{
		this.turningSpeedValue += value;
		if (this.turningSpeedValue < 1)
		{
			this.turningSpeedValue = 1;
		}
		else if (this.turningSpeedValue > 15)
		{
			this.turningSpeedValue = 15;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x00044D8B File Offset: 0x00042F8B
	public void MovementDirectionChangeValue(int value)
	{
		this.movementDirectionValue += value;
		if (this.movementDirectionValue < 0)
		{
			this.movementDirectionValue = 0;
		}
		else if (this.movementDirectionValue > 1)
		{
			this.movementDirectionValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00044DC3 File Offset: 0x00042FC3
	public void GrabTypeChangeValue(int value)
	{
		this.grabTypeValue += value;
		if (this.grabTypeValue < 0)
		{
			this.grabTypeValue = 0;
		}
		else if (this.grabTypeValue > 1)
		{
			this.grabTypeValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00044DFB File Offset: 0x00042FFB
	public void SmoothCamChangeValue(int value)
	{
		this.SmoothCamValue += value;
		if (this.SmoothCamValue < 0)
		{
			this.SmoothCamValue = 0;
		}
		else if (this.SmoothCamValue > 1)
		{
			this.SmoothCamValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00044E33 File Offset: 0x00043033
	public void GripTypeChangeValue(int value)
	{
		this.gripTypeValue += value;
		if (this.gripTypeValue < 0)
		{
			this.gripTypeValue = 0;
		}
		else if (this.gripTypeValue > 2)
		{
			this.gripTypeValue = 2;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00044E6B File Offset: 0x0004306B
	public void TeleportGrabChangeValue(int value)
	{
		this.teleportGrabValue += value;
		if (this.teleportGrabValue < 0)
		{
			this.teleportGrabValue = 0;
		}
		else if (this.teleportGrabValue > 1)
		{
			this.teleportGrabValue = 1;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00044EA3 File Offset: 0x000430A3
	public void ControllerRotationXChangeValue(int value)
	{
		this.controllerRotationXValue += value;
		if (this.controllerRotationXValue < -6)
		{
			this.controllerRotationXValue = -6;
		}
		else if (this.controllerRotationXValue > 6)
		{
			this.controllerRotationXValue = 6;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00044EDD File Offset: 0x000430DD
	public void ControllerRotationYChangeValue(int value)
	{
		this.controllerRotationYValue += value;
		if (this.controllerRotationYValue < -6)
		{
			this.controllerRotationYValue = -6;
		}
		else if (this.controllerRotationYValue > 6)
		{
			this.controllerRotationYValue = 6;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00044F17 File Offset: 0x00043117
	public void ControllerRotationZChangeValue(int value)
	{
		this.controllerRotationZValue += value;
		if (this.controllerRotationZValue < -6)
		{
			this.controllerRotationZValue = -6;
		}
		else if (this.controllerRotationZValue > 6)
		{
			this.controllerRotationZValue = 6;
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00044F51 File Offset: 0x00043151
	private string GetLocomotionText()
	{
		if (this.locomotionValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("VR_Teleport");
		}
		return LocalisationSystem.GetLocalisedValue("VR_Smooth");
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00044F70 File Offset: 0x00043170
	private string GetTurningText()
	{
		if (this.turningValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("VR_Snap");
		}
		return LocalisationSystem.GetLocalisedValue("VR_Smooth");
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00044F8F File Offset: 0x0004318F
	private string GetTurningAngleValueText()
	{
		if (this.turningAngleValue == 0)
		{
			return "15";
		}
		if (this.turningAngleValue == 1)
		{
			return "45";
		}
		return "90";
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00044FB3 File Offset: 0x000431B3
	private string GetTurningSpeedValueText()
	{
		return this.turningSpeedValue.ToString("0");
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00044FC5 File Offset: 0x000431C5
	private string GetMovementDirectionValueText()
	{
		if (this.movementDirectionValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("VR_MovementHead");
		}
		return LocalisationSystem.GetLocalisedValue("VR_MovementController");
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00044FE4 File Offset: 0x000431E4
	private string GetGrabTypeValueText()
	{
		if (this.grabTypeValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("VR_Hold");
		}
		return LocalisationSystem.GetLocalisedValue("VR_Toggle");
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00045003 File Offset: 0x00043203
	private string GetSmoothCamValueText()
	{
		if (this.SmoothCamValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Menu_On");
		}
		return LocalisationSystem.GetLocalisedValue("Menu_Off");
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00045022 File Offset: 0x00043222
	private string GetGripTypeValueText()
	{
		if (this.gripTypeValue == 0)
		{
			return LocalisationSystem.GetLocalisedValue("VR_GripPress");
		}
		if (this.gripTypeValue == 1)
		{
			return LocalisationSystem.GetLocalisedValue("VR_GripTouch");
		}
		return LocalisationSystem.GetLocalisedValue("VR_GripThreshold");
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00045055 File Offset: 0x00043255
	private string GetTeleportGrabText()
	{
		if (this.teleportGrabValue != 0)
		{
			return LocalisationSystem.GetLocalisedValue("Menu_On");
		}
		return LocalisationSystem.GetLocalisedValue("Menu_Off");
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00045074 File Offset: 0x00043274
	private string GetControllerXRotationValueText()
	{
		if (this.controllerRotationXValue == -6)
		{
			return "-90";
		}
		if (this.controllerRotationXValue == -5)
		{
			return "-75";
		}
		if (this.controllerRotationXValue == -4)
		{
			return "-60";
		}
		if (this.controllerRotationXValue == -3)
		{
			return "-45";
		}
		if (this.controllerRotationXValue == -2)
		{
			return "-30";
		}
		if (this.controllerRotationXValue == -1)
		{
			return "-15";
		}
		if (this.controllerRotationXValue == 0)
		{
			return "0";
		}
		if (this.controllerRotationXValue == 1)
		{
			return "15";
		}
		if (this.controllerRotationXValue == 2)
		{
			return "30";
		}
		if (this.controllerRotationXValue == 3)
		{
			return "45";
		}
		if (this.controllerRotationXValue == 4)
		{
			return "60";
		}
		if (this.controllerRotationXValue == 5)
		{
			return "75";
		}
		return "90";
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00045140 File Offset: 0x00043340
	private string GetControllerYRotationValueText()
	{
		if (this.controllerRotationYValue == -6)
		{
			return "-90";
		}
		if (this.controllerRotationYValue == -5)
		{
			return "-75";
		}
		if (this.controllerRotationYValue == -4)
		{
			return "-60";
		}
		if (this.controllerRotationYValue == -3)
		{
			return "-45";
		}
		if (this.controllerRotationYValue == -2)
		{
			return "-30";
		}
		if (this.controllerRotationYValue == -1)
		{
			return "-15";
		}
		if (this.controllerRotationYValue == 0)
		{
			return "0";
		}
		if (this.controllerRotationYValue == 1)
		{
			return "15";
		}
		if (this.controllerRotationYValue == 2)
		{
			return "30";
		}
		if (this.controllerRotationYValue == 3)
		{
			return "45";
		}
		if (this.controllerRotationYValue == 4)
		{
			return "60";
		}
		if (this.controllerRotationYValue == 5)
		{
			return "75";
		}
		return "90";
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0004520C File Offset: 0x0004340C
	private string GetControllerZRotationValueText()
	{
		if (this.controllerRotationZValue == -6)
		{
			return "-90";
		}
		if (this.controllerRotationZValue == -5)
		{
			return "-75";
		}
		if (this.controllerRotationZValue == -4)
		{
			return "-60";
		}
		if (this.controllerRotationZValue == -3)
		{
			return "-45";
		}
		if (this.controllerRotationZValue == -2)
		{
			return "-30";
		}
		if (this.controllerRotationZValue == -1)
		{
			return "-15";
		}
		if (this.controllerRotationZValue == 0)
		{
			return "0";
		}
		if (this.controllerRotationZValue == 1)
		{
			return "15";
		}
		if (this.controllerRotationZValue == 2)
		{
			return "30";
		}
		if (this.controllerRotationZValue == 3)
		{
			return "45";
		}
		if (this.controllerRotationZValue == 4)
		{
			return "60";
		}
		if (this.controllerRotationZValue == 5)
		{
			return "75";
		}
		return "90";
	}

	// Token: 0x04000B4F RID: 2895
	private int locomotionValue;

	// Token: 0x04000B50 RID: 2896
	private int turningValue;

	// Token: 0x04000B51 RID: 2897
	private int turningAngleValue;

	// Token: 0x04000B52 RID: 2898
	private int movementDirectionValue;

	// Token: 0x04000B53 RID: 2899
	private int grabTypeValue;

	// Token: 0x04000B54 RID: 2900
	private int SmoothCamValue;

	// Token: 0x04000B55 RID: 2901
	private int gripTypeValue;

	// Token: 0x04000B56 RID: 2902
	private int teleportGrabValue;

	// Token: 0x04000B57 RID: 2903
	private int turningSpeedValue;

	// Token: 0x04000B58 RID: 2904
	private int controllerRotationXValue;

	// Token: 0x04000B59 RID: 2905
	private int controllerRotationYValue;

	// Token: 0x04000B5A RID: 2906
	private int controllerRotationZValue;

	// Token: 0x04000B5B RID: 2907
	[SerializeField]
	private Button VRButton;

	// Token: 0x04000B5C RID: 2908
	[SerializeField]
	private Text VRButtonText;

	// Token: 0x04000B5D RID: 2909
	[SerializeField]
	private Text locomotionValueText;

	// Token: 0x04000B5E RID: 2910
	[SerializeField]
	private Text turningValueText;

	// Token: 0x04000B5F RID: 2911
	[SerializeField]
	private Text turningAngleValueText;

	// Token: 0x04000B60 RID: 2912
	[SerializeField]
	private Text movementDirectionValueText;

	// Token: 0x04000B61 RID: 2913
	[SerializeField]
	private Text grabTypeValueText;

	// Token: 0x04000B62 RID: 2914
	[SerializeField]
	private Text smoothCamValueText;

	// Token: 0x04000B63 RID: 2915
	[SerializeField]
	private Text gripTypeValueText;

	// Token: 0x04000B64 RID: 2916
	[SerializeField]
	private Text teleportGrabValueText;

	// Token: 0x04000B65 RID: 2917
	[SerializeField]
	private Text turningSpeedValueText;

	// Token: 0x04000B66 RID: 2918
	[SerializeField]
	private Text controllerRotationXValueText;

	// Token: 0x04000B67 RID: 2919
	[SerializeField]
	private Text controllerRotationYValueText;

	// Token: 0x04000B68 RID: 2920
	[SerializeField]
	private Text controllerRotationZValueText;
}
