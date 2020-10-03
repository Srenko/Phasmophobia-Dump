using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000425 RID: 1061
	[RequireComponent(typeof(Interactable))]
	public class InteractableHoverEvents : MonoBehaviour
	{
		// Token: 0x060020A2 RID: 8354 RVA: 0x000A1211 File Offset: 0x0009F411
		private void OnHandHoverBegin()
		{
			this.onHandHoverBegin.Invoke();
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000A121E File Offset: 0x0009F41E
		private void OnHandHoverEnd()
		{
			this.onHandHoverEnd.Invoke();
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000A122B File Offset: 0x0009F42B
		private void OnAttachedToHand(Hand hand)
		{
			this.onAttachedToHand.Invoke();
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000A1238 File Offset: 0x0009F438
		private void OnDetachedFromHand(Hand hand)
		{
			this.onDetachedFromHand.Invoke();
		}

		// Token: 0x04001E20 RID: 7712
		public UnityEvent onHandHoverBegin;

		// Token: 0x04001E21 RID: 7713
		public UnityEvent onHandHoverEnd;

		// Token: 0x04001E22 RID: 7714
		public UnityEvent onAttachedToHand;

		// Token: 0x04001E23 RID: 7715
		public UnityEvent onDetachedFromHand;
	}
}
