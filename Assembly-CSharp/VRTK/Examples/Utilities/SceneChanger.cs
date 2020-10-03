using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRTK.Examples.Utilities
{
	// Token: 0x02000377 RID: 887
	public class SceneChanger : MonoBehaviour
	{
		// Token: 0x06001E93 RID: 7827 RVA: 0x0009B7AA File Offset: 0x000999AA
		private void Awake()
		{
			this.canPress = false;
			base.Invoke("ResetPress", 1f);
			DynamicGI.UpdateEnvironment();
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0009B7C8 File Offset: 0x000999C8
		private bool IsForwardPressed()
		{
			return VRTK_ControllerReference.IsValid(this.controllerReference) && (this.canPress && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference) && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference) && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference));
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009B81C File Offset: 0x00099A1C
		private bool IsBackPressed()
		{
			return VRTK_ControllerReference.IsValid(this.controllerReference) && (this.canPress && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference) && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference) && VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.Press, this.controllerReference));
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0009B870 File Offset: 0x00099A70
		private void ResetPress()
		{
			this.canPress = true;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0009B87C File Offset: 0x00099A7C
		private void Update()
		{
			GameObject controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(true);
			this.controllerReference = VRTK_ControllerReference.GetControllerReference(controllerRightHand);
			int buildIndex = SceneManager.GetActiveScene().buildIndex;
			int num = buildIndex;
			if (this.IsForwardPressed() || Input.GetKeyUp(KeyCode.Space))
			{
				num++;
				if (num >= SceneManager.sceneCountInBuildSettings)
				{
					num = 0;
				}
			}
			else if (this.IsBackPressed() || Input.GetKeyUp(KeyCode.Backspace))
			{
				num--;
				if (num < 0)
				{
					num = SceneManager.sceneCountInBuildSettings - 1;
				}
			}
			if (num == buildIndex)
			{
				return;
			}
			SceneManager.LoadScene(num);
		}

		// Token: 0x040017DE RID: 6110
		private bool canPress;

		// Token: 0x040017DF RID: 6111
		private VRTK_ControllerReference controllerReference;
	}
}
