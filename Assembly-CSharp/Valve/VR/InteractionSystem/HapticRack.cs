using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200041F RID: 1055
	[RequireComponent(typeof(Interactable))]
	public class HapticRack : MonoBehaviour
	{
		// Token: 0x06002089 RID: 8329 RVA: 0x000A0E05 File Offset: 0x0009F005
		private void Awake()
		{
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000A0E21 File Offset: 0x0009F021
		private void OnHandHoverBegin(Hand hand)
		{
			this.hand = hand;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000A0E2A File Offset: 0x0009F02A
		private void OnHandHoverEnd(Hand hand)
		{
			this.hand = null;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000A0E34 File Offset: 0x0009F034
		private void Update()
		{
			int num = Mathf.RoundToInt(this.linearMapping.value * (float)this.teethCount - 0.5f);
			if (num != this.previousToothIndex)
			{
				this.Pulse();
				this.previousToothIndex = num;
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000A0E78 File Offset: 0x0009F078
		private void Pulse()
		{
			if (this.hand && this.hand.controller != null && this.hand.GetStandardInteractionButton())
			{
				ushort durationMicroSec = (ushort)Random.Range(this.minimumPulseDuration, this.maximumPulseDuration + 1);
				this.hand.controller.TriggerHapticPulse(durationMicroSec, EVRButtonId.k_EButton_Axis0);
				this.onPulse.Invoke();
			}
		}

		// Token: 0x04001E0C RID: 7692
		[Tooltip("The linear mapping driving the haptic rack")]
		public LinearMapping linearMapping;

		// Token: 0x04001E0D RID: 7693
		[Tooltip("The number of haptic pulses evenly distributed along the mapping")]
		public int teethCount = 128;

		// Token: 0x04001E0E RID: 7694
		[Tooltip("Minimum duration of the haptic pulse")]
		public int minimumPulseDuration = 500;

		// Token: 0x04001E0F RID: 7695
		[Tooltip("Maximum duration of the haptic pulse")]
		public int maximumPulseDuration = 900;

		// Token: 0x04001E10 RID: 7696
		[Tooltip("This event is triggered every time a haptic pulse is made")]
		public UnityEvent onPulse;

		// Token: 0x04001E11 RID: 7697
		private Hand hand;

		// Token: 0x04001E12 RID: 7698
		private int previousToothIndex = -1;
	}
}
