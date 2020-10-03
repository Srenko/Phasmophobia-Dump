using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000424 RID: 1060
	[RequireComponent(typeof(Interactable))]
	public class InteractableButtonEvents : MonoBehaviour
	{
		// Token: 0x060020A0 RID: 8352 RVA: 0x000A1104 File Offset: 0x0009F304
		private void Update()
		{
			for (int i = 0; i < Player.instance.handCount; i++)
			{
				Hand hand = Player.instance.GetHand(i);
				if (hand.controller != null)
				{
					if (hand.controller.GetPressDown(EVRButtonId.k_EButton_Axis1))
					{
						this.onTriggerDown.Invoke();
					}
					if (hand.controller.GetPressUp(EVRButtonId.k_EButton_Axis1))
					{
						this.onTriggerUp.Invoke();
					}
					if (hand.controller.GetPressDown(EVRButtonId.k_EButton_Grip))
					{
						this.onGripDown.Invoke();
					}
					if (hand.controller.GetPressUp(EVRButtonId.k_EButton_Grip))
					{
						this.onGripUp.Invoke();
					}
					if (hand.controller.GetPressDown(EVRButtonId.k_EButton_Axis0))
					{
						this.onTouchpadDown.Invoke();
					}
					if (hand.controller.GetPressUp(EVRButtonId.k_EButton_Axis0))
					{
						this.onTouchpadUp.Invoke();
					}
					if (hand.controller.GetTouchDown(EVRButtonId.k_EButton_Axis0))
					{
						this.onTouchpadTouch.Invoke();
					}
					if (hand.controller.GetTouchUp(EVRButtonId.k_EButton_Axis0))
					{
						this.onTouchpadRelease.Invoke();
					}
				}
			}
		}

		// Token: 0x04001E18 RID: 7704
		public UnityEvent onTriggerDown;

		// Token: 0x04001E19 RID: 7705
		public UnityEvent onTriggerUp;

		// Token: 0x04001E1A RID: 7706
		public UnityEvent onGripDown;

		// Token: 0x04001E1B RID: 7707
		public UnityEvent onGripUp;

		// Token: 0x04001E1C RID: 7708
		public UnityEvent onTouchpadDown;

		// Token: 0x04001E1D RID: 7709
		public UnityEvent onTouchpadUp;

		// Token: 0x04001E1E RID: 7710
		public UnityEvent onTouchpadTouch;

		// Token: 0x04001E1F RID: 7711
		public UnityEvent onTouchpadRelease;
	}
}
