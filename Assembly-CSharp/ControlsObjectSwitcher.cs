using System;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000138 RID: 312
public class ControlsObjectSwitcher : MonoBehaviour
{
	// Token: 0x06000831 RID: 2097 RVA: 0x000310E8 File Offset: 0x0002F2E8
	private void OnEnable()
	{
		this.keyboardObj.SetActive(false);
		this.controllerObj.SetActive(false);
		if (!XRDevice.isPresent)
		{
			if (MainManager.instance)
			{
				if (MainManager.instance.localPlayer.playerInput.currentControlScheme == "Keyboard")
				{
					this.keyboardObj.SetActive(true);
					return;
				}
				this.controllerObj.SetActive(true);
				return;
			}
			else if (GameController.instance)
			{
				if (GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Keyboard")
				{
					this.keyboardObj.SetActive(true);
					return;
				}
				this.controllerObj.SetActive(true);
			}
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x000311A7 File Offset: 0x0002F3A7
	private void OnDisable()
	{
		this.keyboardObj.SetActive(false);
		this.controllerObj.SetActive(false);
	}

	// Token: 0x0400084F RID: 2127
	[SerializeField]
	private GameObject keyboardObj;

	// Token: 0x04000850 RID: 2128
	[SerializeField]
	private GameObject controllerObj;
}
