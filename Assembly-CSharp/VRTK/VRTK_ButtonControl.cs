using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002CA RID: 714
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_ButtonControl")]
	public class VRTK_ButtonControl : VRTK_ObjectControl
	{
		// Token: 0x060017B5 RID: 6069 RVA: 0x0007EC58 File Offset: 0x0007CE58
		protected override void Update()
		{
			base.Update();
			if (this.forwardButton != this.subscribedForwardButton || this.backwardButton != this.subscribedBackwardButton || this.leftButton != this.subscribedLeftButton || this.rightButton != this.subscribedRightButton)
			{
				this.SetListeners(true);
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0007ECAC File Offset: 0x0007CEAC
		protected override void ControlFixedUpdate()
		{
			float x = this.leftPressed ? -1f : (this.rightPressed ? 1f : 0f);
			float y = this.forwardPressed ? 1f : (this.backwardPressed ? -1f : 0f);
			this.currentAxis = new Vector2(x, y);
			if (this.currentAxis.x != 0f)
			{
				this.OnXAxisChanged(this.SetEventArguements(this.directionDevice.right, this.currentAxis.x, this.axisDeadzone.x));
			}
			if (this.currentAxis.y != 0f)
			{
				this.OnYAxisChanged(this.SetEventArguements(this.directionDevice.forward, this.currentAxis.y, this.axisDeadzone.y));
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0007ED8C File Offset: 0x0007CF8C
		protected override VRTK_ObjectControl GetOtherControl()
		{
			GameObject gameObject = VRTK_DeviceFinder.IsControllerLeftHand(base.gameObject) ? VRTK_DeviceFinder.GetControllerRightHand(false) : VRTK_DeviceFinder.GetControllerLeftHand(false);
			if (gameObject)
			{
				return gameObject.GetComponent<VRTK_ButtonControl>();
			}
			return null;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0007EDC8 File Offset: 0x0007CFC8
		protected override void SetListeners(bool state)
		{
			this.SetDirectionListener(state, this.forwardButton, ref this.subscribedForwardButton, new ControllerInteractionEventHandler(this.ForwardButtonPressed), new ControllerInteractionEventHandler(this.ForwardButtonReleased));
			this.SetDirectionListener(state, this.backwardButton, ref this.subscribedBackwardButton, new ControllerInteractionEventHandler(this.BackwardButtonPressed), new ControllerInteractionEventHandler(this.BackwardButtonReleased));
			this.SetDirectionListener(state, this.leftButton, ref this.subscribedLeftButton, new ControllerInteractionEventHandler(this.LeftButtonPressed), new ControllerInteractionEventHandler(this.LeftButtonReleased));
			this.SetDirectionListener(state, this.rightButton, ref this.subscribedRightButton, new ControllerInteractionEventHandler(this.RightButtonPressed), new ControllerInteractionEventHandler(this.RightButtonReleased));
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0007EE89 File Offset: 0x0007D089
		protected override bool IsInAction()
		{
			return this.forwardPressed || this.backwardPressed || this.leftPressed || this.rightPressed;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0007EEAC File Offset: 0x0007D0AC
		protected virtual void SetDirectionListener(bool state, VRTK_ControllerEvents.ButtonAlias directionButton, ref VRTK_ControllerEvents.ButtonAlias subscribedDirectionButton, ControllerInteractionEventHandler pressCallback, ControllerInteractionEventHandler releaseCallback)
		{
			if (this.controllerEvents)
			{
				if (subscribedDirectionButton != VRTK_ControllerEvents.ButtonAlias.Undefined && (!state || directionButton == VRTK_ControllerEvents.ButtonAlias.Undefined || directionButton != subscribedDirectionButton))
				{
					this.controllerEvents.UnsubscribeToButtonAliasEvent(subscribedDirectionButton, true, pressCallback);
					this.controllerEvents.UnsubscribeToButtonAliasEvent(subscribedDirectionButton, false, releaseCallback);
					subscribedDirectionButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
				}
				if (state && directionButton != VRTK_ControllerEvents.ButtonAlias.Undefined && directionButton != subscribedDirectionButton)
				{
					this.controllerEvents.SubscribeToButtonAliasEvent(directionButton, true, pressCallback);
					this.controllerEvents.SubscribeToButtonAliasEvent(directionButton, false, releaseCallback);
					subscribedDirectionButton = directionButton;
				}
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0007EF24 File Offset: 0x0007D124
		protected virtual void ForwardButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.forwardPressed = true;
			this.backwardPressed = false;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0007EF34 File Offset: 0x0007D134
		protected virtual void ForwardButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.forwardPressed = false;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0007EF3D File Offset: 0x0007D13D
		protected virtual void BackwardButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.backwardPressed = true;
			this.forwardPressed = false;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0007EF4D File Offset: 0x0007D14D
		protected virtual void BackwardButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.backwardPressed = false;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0007EF56 File Offset: 0x0007D156
		protected virtual void LeftButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.leftPressed = true;
			this.rightPressed = false;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0007EF66 File Offset: 0x0007D166
		protected virtual void LeftButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.leftPressed = false;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0007EF6F File Offset: 0x0007D16F
		protected virtual void RightButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.rightPressed = true;
			this.leftPressed = false;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0007EF7F File Offset: 0x0007D17F
		protected virtual void RightButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.rightPressed = false;
		}

		// Token: 0x04001349 RID: 4937
		[Header("Button Control Settings")]
		[Tooltip("The button to set the y axis to +1.")]
		public VRTK_ControllerEvents.ButtonAlias forwardButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

		// Token: 0x0400134A RID: 4938
		[Tooltip("The button to set the y axis to -1.")]
		public VRTK_ControllerEvents.ButtonAlias backwardButton;

		// Token: 0x0400134B RID: 4939
		[Tooltip("The button to set the x axis to -1.")]
		public VRTK_ControllerEvents.ButtonAlias leftButton;

		// Token: 0x0400134C RID: 4940
		[Tooltip("The button to set the x axis to +1.")]
		public VRTK_ControllerEvents.ButtonAlias rightButton;

		// Token: 0x0400134D RID: 4941
		protected bool forwardPressed;

		// Token: 0x0400134E RID: 4942
		protected bool backwardPressed;

		// Token: 0x0400134F RID: 4943
		protected bool leftPressed;

		// Token: 0x04001350 RID: 4944
		protected bool rightPressed;

		// Token: 0x04001351 RID: 4945
		protected VRTK_ControllerEvents.ButtonAlias subscribedForwardButton;

		// Token: 0x04001352 RID: 4946
		protected VRTK_ControllerEvents.ButtonAlias subscribedBackwardButton;

		// Token: 0x04001353 RID: 4947
		protected VRTK_ControllerEvents.ButtonAlias subscribedLeftButton;

		// Token: 0x04001354 RID: 4948
		protected VRTK_ControllerEvents.ButtonAlias subscribedRightButton;

		// Token: 0x04001355 RID: 4949
		protected Vector2 axisDeadzone = Vector2.zero;
	}
}
