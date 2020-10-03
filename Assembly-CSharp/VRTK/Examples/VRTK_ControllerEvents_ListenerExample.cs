using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200036E RID: 878
	public class VRTK_ControllerEvents_ListenerExample : MonoBehaviour
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x0009937C File Offset: 0x0009757C
		private void Start()
		{
			if (base.GetComponent<VRTK_ControllerEvents>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_ControllerEvents_ListenerExample",
					"VRTK_ControllerEvents",
					"the same"
				}));
				return;
			}
			base.GetComponent<VRTK_ControllerEvents>().TriggerPressed += this.DoTriggerPressed;
			base.GetComponent<VRTK_ControllerEvents>().TriggerReleased += this.DoTriggerReleased;
			base.GetComponent<VRTK_ControllerEvents>().TriggerTouchStart += this.DoTriggerTouchStart;
			base.GetComponent<VRTK_ControllerEvents>().TriggerTouchEnd += this.DoTriggerTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().TriggerHairlineStart += this.DoTriggerHairlineStart;
			base.GetComponent<VRTK_ControllerEvents>().TriggerHairlineEnd += this.DoTriggerHairlineEnd;
			base.GetComponent<VRTK_ControllerEvents>().TriggerClicked += this.DoTriggerClicked;
			base.GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += this.DoTriggerUnclicked;
			base.GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += this.DoTriggerAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().GripPressed += this.DoGripPressed;
			base.GetComponent<VRTK_ControllerEvents>().GripReleased += this.DoGripReleased;
			base.GetComponent<VRTK_ControllerEvents>().GripTouchStart += this.DoGripTouchStart;
			base.GetComponent<VRTK_ControllerEvents>().GripTouchEnd += this.DoGripTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().GripHairlineStart += this.DoGripHairlineStart;
			base.GetComponent<VRTK_ControllerEvents>().GripHairlineEnd += this.DoGripHairlineEnd;
			base.GetComponent<VRTK_ControllerEvents>().GripClicked += this.DoGripClicked;
			base.GetComponent<VRTK_ControllerEvents>().GripUnclicked += this.DoGripUnclicked;
			base.GetComponent<VRTK_ControllerEvents>().GripAxisChanged += this.DoGripAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += this.DoTouchpadPressed;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += this.DoTouchpadReleased;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += this.DoTouchpadTouchStart;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += this.DoTouchpadTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += this.DoTouchpadAxisChanged;
			base.GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += this.DoButtonOnePressed;
			base.GetComponent<VRTK_ControllerEvents>().ButtonOneReleased += this.DoButtonOneReleased;
			base.GetComponent<VRTK_ControllerEvents>().ButtonOneTouchStart += this.DoButtonOneTouchStart;
			base.GetComponent<VRTK_ControllerEvents>().ButtonOneTouchEnd += this.DoButtonOneTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += this.DoButtonTwoPressed;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += this.DoButtonTwoReleased;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchStart += this.DoButtonTwoTouchStart;
			base.GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchEnd += this.DoButtonTwoTouchEnd;
			base.GetComponent<VRTK_ControllerEvents>().StartMenuPressed += this.DoStartMenuPressed;
			base.GetComponent<VRTK_ControllerEvents>().StartMenuReleased += this.DoStartMenuReleased;
			base.GetComponent<VRTK_ControllerEvents>().ControllerEnabled += this.DoControllerEnabled;
			base.GetComponent<VRTK_ControllerEvents>().ControllerDisabled += this.DoControllerDisabled;
			base.GetComponent<VRTK_ControllerEvents>().ControllerIndexChanged += this.DoControllerIndexChanged;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00099700 File Offset: 0x00097900
		private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
		{
			VRTK_Logger.Info(string.Concat(new object[]
			{
				"Controller on index '",
				index,
				"' ",
				button,
				" has been ",
				action,
				" with a pressure of ",
				e.buttonPressure,
				" / trackpad axis at: ",
				e.touchpadAxis,
				" (",
				e.touchpadAngle,
				" degrees)"
			}));
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00099798 File Offset: 0x00097998
		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "pressed", e);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000997B6 File Offset: 0x000979B6
		private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000997D4 File Offset: 0x000979D4
		private void DoTriggerTouchStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "touched", e);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000997F2 File Offset: 0x000979F2
		private void DoTriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "untouched", e);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00099810 File Offset: 0x00097A10
		private void DoTriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline start", e);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0009982E File Offset: 0x00097A2E
		private void DoTriggerHairlineEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline end", e);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0009984C File Offset: 0x00097A4C
		private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "clicked", e);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0009986A File Offset: 0x00097A6A
		private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "unclicked", e);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00099888 File Offset: 0x00097A88
		private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "axis changed", e);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000998A6 File Offset: 0x00097AA6
		private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "pressed", e);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000998C4 File Offset: 0x00097AC4
		private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "released", e);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000998E2 File Offset: 0x00097AE2
		private void DoGripTouchStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "touched", e);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00099900 File Offset: 0x00097B00
		private void DoGripTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "untouched", e);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0009991E File Offset: 0x00097B1E
		private void DoGripHairlineStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline start", e);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0009993C File Offset: 0x00097B3C
		private void DoGripHairlineEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline end", e);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0009995A File Offset: 0x00097B5A
		private void DoGripClicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "clicked", e);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00099978 File Offset: 0x00097B78
		private void DoGripUnclicked(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "unclicked", e);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00099996 File Offset: 0x00097B96
		private void DoGripAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "axis changed", e);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000999B4 File Offset: 0x00097BB4
		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "pressed down", e);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000999D2 File Offset: 0x00097BD2
		private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "released", e);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000999F0 File Offset: 0x00097BF0
		private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "touched", e);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00099A0E File Offset: 0x00097C0E
		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "untouched", e);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00099A2C File Offset: 0x00097C2C
		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "axis changed", e);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00099A4A File Offset: 0x00097C4A
		private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "pressed down", e);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00099A68 File Offset: 0x00097C68
		private void DoButtonOneReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "released", e);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00099A86 File Offset: 0x00097C86
		private void DoButtonOneTouchStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "touched", e);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00099AA4 File Offset: 0x00097CA4
		private void DoButtonOneTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "untouched", e);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00099AC2 File Offset: 0x00097CC2
		private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "pressed down", e);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00099AE0 File Offset: 0x00097CE0
		private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "released", e);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00099AFE File Offset: 0x00097CFE
		private void DoButtonTwoTouchStart(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "touched", e);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00099B1C File Offset: 0x00097D1C
		private void DoButtonTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "untouched", e);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00099B3A File Offset: 0x00097D3A
		private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "pressed down", e);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00099B58 File Offset: 0x00097D58
		private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "released", e);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00099B76 File Offset: 0x00097D76
		private void DoControllerEnabled(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "ENABLED", e);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00099B94 File Offset: 0x00097D94
		private void DoControllerDisabled(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "DISABLED", e);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00099BB2 File Offset: 0x00097DB2
		private void DoControllerIndexChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "INDEX CHANGED", e);
		}
	}
}
