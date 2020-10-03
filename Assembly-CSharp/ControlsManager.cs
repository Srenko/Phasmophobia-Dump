using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using VRTK;

// Token: 0x0200017B RID: 379
public class ControlsManager : MonoBehaviour
{
	// Token: 0x06000A02 RID: 2562 RVA: 0x0003DF54 File Offset: 0x0003C154
	private void Start()
	{
		if (XRDevice.isPresent)
		{
			this.pcControls.SetActive(false);
			this.controlsImage.gameObject.SetActive(true);
			this.resetButton.interactable = false;
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				this.controlsImage.sprite = this.oculusRiftSprite;
				return;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				this.controlsImage.sprite = this.htcViveSprite;
				return;
			}
			Debug.LogError(string.Concat(new object[]
			{
				"Device not found for controls: ",
				XRDevice.model,
				" : ",
				VRTK_DeviceFinder.GetHeadsetType(true)
			}));
			this.controlsImage.sprite = this.oculusRiftSprite;
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0003E010 File Offset: 0x0003C210
	private void Update()
	{
		if (!XRDevice.isPresent && MainManager.instance && MainManager.instance.localPlayer && MainManager.instance.localPlayer.playerInput && MainManager.instance.localPlayer.playerInput.currentControlScheme != this.currentControlScheme)
		{
			this.currentControlScheme = MainManager.instance.localPlayer.playerInput.currentControlScheme;
			this.OnControlSchemeChanged();
		}
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0003E098 File Offset: 0x0003C298
	public void OnControlSchemeChanged()
	{
		if (this.currentControlScheme == "Keyboard")
		{
			this.keyboardObj.SetActive(true);
			this.controllerObj.SetActive(false);
			return;
		}
		this.keyboardObj.SetActive(false);
		this.controllerObj.SetActive(true);
		GamepadUISelector.instance.SetSelection();
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0003E0F2 File Offset: 0x0003C2F2
	public void LoadControls()
	{
		this.controlsImage.gameObject.SetActive(false);
		this.pcControls.SetActive(true);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0003E111 File Offset: 0x0003C311
	public void ControlsButton()
	{
		if (!XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.playerInput.DeactivateInput();
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0003E12E File Offset: 0x0003C32E
	public void ResetButton()
	{
		if (!XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.pcControls.ResetKeybindings();
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0003E14B File Offset: 0x0003C34B
	public void BackButton()
	{
		if (!XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.playerInput.ActivateInput();
			MainManager.instance.localPlayer.pcControls.StoreControlOverrides();
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003E17C File Offset: 0x0003C37C
	private void OnEnable()
	{
		if (!XRDevice.isPresent)
		{
			if (MainManager.instance.localPlayer.playerInput.currentControlScheme == "Keyboard")
			{
				MainManager.instance.localPlayer.playerInput.DeactivateInput();
				this.keyboardObj.SetActive(true);
				return;
			}
			MainManager.instance.localPlayer.playerInput.DeactivateInput();
			this.controllerObj.SetActive(true);
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0003E1F1 File Offset: 0x0003C3F1
	private void OnDisable()
	{
		this.keyboardObj.SetActive(false);
		this.controllerObj.SetActive(false);
		if (!XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.playerInput.ActivateInput();
		}
	}

	// Token: 0x04000A2D RID: 2605
	[SerializeField]
	private Image controlsImage;

	// Token: 0x04000A2E RID: 2606
	[SerializeField]
	private GameObject pcControls;

	// Token: 0x04000A2F RID: 2607
	[SerializeField]
	private Sprite oculusRiftSprite;

	// Token: 0x04000A30 RID: 2608
	[SerializeField]
	private Sprite htcViveSprite;

	// Token: 0x04000A31 RID: 2609
	[SerializeField]
	private GameObject keyboardObj;

	// Token: 0x04000A32 RID: 2610
	[SerializeField]
	private GameObject controllerObj;

	// Token: 0x04000A33 RID: 2611
	private string currentControlScheme;

	// Token: 0x04000A34 RID: 2612
	[SerializeField]
	private Button resetButton;
}
