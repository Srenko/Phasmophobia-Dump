using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D9 RID: 729
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_TouchpadControl")]
	public class VRTK_TouchpadControl : VRTK_ObjectControl
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x00080F14 File Offset: 0x0007F114
		protected override void OnEnable()
		{
			base.OnEnable();
			this.touchpadFirstChange = true;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00080F24 File Offset: 0x0007F124
		protected override void ControlFixedUpdate()
		{
			this.ModifierButtonActive();
			if (this.OutsideDeadzone(this.currentAxis.x, this.axisDeadzone.x) || this.currentAxis.x == 0f)
			{
				this.OnXAxisChanged(this.SetEventArguements(this.directionDevice.right, this.currentAxis.x, this.axisDeadzone.x));
			}
			if (this.OutsideDeadzone(this.currentAxis.y, this.axisDeadzone.y) || this.currentAxis.y == 0f)
			{
				this.OnYAxisChanged(this.SetEventArguements(this.directionDevice.forward, this.currentAxis.y, this.axisDeadzone.y));
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00080FF4 File Offset: 0x0007F1F4
		protected override VRTK_ObjectControl GetOtherControl()
		{
			GameObject gameObject = VRTK_DeviceFinder.IsControllerLeftHand(base.gameObject) ? VRTK_DeviceFinder.GetControllerRightHand(false) : VRTK_DeviceFinder.GetControllerLeftHand(false);
			if (gameObject)
			{
				return gameObject.GetComponent<VRTK_TouchpadControl>();
			}
			return null;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00081030 File Offset: 0x0007F230
		protected override void SetListeners(bool state)
		{
			if (this.controllerEvents)
			{
				if (state)
				{
					this.controllerEvents.TouchpadAxisChanged += this.TouchpadAxisChanged;
					this.controllerEvents.TouchpadTouchEnd += this.TouchpadTouchEnd;
					return;
				}
				this.controllerEvents.TouchpadAxisChanged -= this.TouchpadAxisChanged;
				this.controllerEvents.TouchpadTouchEnd -= this.TouchpadTouchEnd;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000810AE File Offset: 0x0007F2AE
		protected override bool IsInAction()
		{
			return this.ValidPrimaryButton() && this.TouchpadTouched();
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000810C0 File Offset: 0x0007F2C0
		protected virtual bool OutsideDeadzone(float axisValue, float deadzoneThreshold)
		{
			return axisValue > deadzoneThreshold || axisValue < -deadzoneThreshold;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000810CD File Offset: 0x0007F2CD
		protected virtual bool ValidPrimaryButton()
		{
			return this.controllerEvents && (this.primaryActivationButton == VRTK_ControllerEvents.ButtonAlias.Undefined || this.controllerEvents.IsButtonPressed(this.primaryActivationButton));
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000810F9 File Offset: 0x0007F2F9
		protected virtual void ModifierButtonActive()
		{
			this.modifierActive = (this.controllerEvents && this.actionModifierButton != VRTK_ControllerEvents.ButtonAlias.Undefined && this.controllerEvents.IsButtonPressed(this.actionModifierButton));
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008112A File Offset: 0x0007F32A
		protected virtual bool TouchpadTouched()
		{
			return this.controllerEvents && this.controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00081148 File Offset: 0x0007F348
		protected virtual void TouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			if (this.touchpadFirstChange && this.otherObjectControl && this.disableOtherControlsOnActive && e.touchpadAxis != Vector2.zero)
			{
				this.otherTouchpadControlEnabledState = this.otherObjectControl.enabled;
				this.otherObjectControl.enabled = false;
			}
			this.currentAxis = (this.ValidPrimaryButton() ? e.touchpadAxis : Vector2.zero);
			if (this.currentAxis != Vector2.zero)
			{
				this.touchpadFirstChange = false;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000811D5 File Offset: 0x0007F3D5
		protected virtual void TouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			if (this.otherObjectControl && this.disableOtherControlsOnActive)
			{
				this.otherObjectControl.enabled = this.otherTouchpadControlEnabledState;
			}
			this.currentAxis = Vector2.zero;
			this.touchpadFirstChange = true;
		}

		// Token: 0x040013C1 RID: 5057
		[Header("Touchpad Control Settings")]
		[Tooltip("An optional button that has to be engaged to allow the touchpad control to activate.")]
		public VRTK_ControllerEvents.ButtonAlias primaryActivationButton = VRTK_ControllerEvents.ButtonAlias.TouchpadTouch;

		// Token: 0x040013C2 RID: 5058
		[Tooltip("An optional button that when engaged will activate the modifier on the touchpad control action.")]
		public VRTK_ControllerEvents.ButtonAlias actionModifierButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040013C3 RID: 5059
		[Tooltip("Any input on the axis will be ignored if it is within this deadzone threshold. Between `0f` and `1f`.")]
		public Vector2 axisDeadzone = new Vector2(0.2f, 0.2f);

		// Token: 0x040013C4 RID: 5060
		protected bool touchpadFirstChange;

		// Token: 0x040013C5 RID: 5061
		protected bool otherTouchpadControlEnabledState;
	}
}
