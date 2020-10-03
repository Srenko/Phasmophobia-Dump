using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000423 RID: 1059
	public class Interactable : MonoBehaviour
	{
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06002099 RID: 8345 RVA: 0x000A0FF8 File Offset: 0x0009F1F8
		// (remove) Token: 0x0600209A RID: 8346 RVA: 0x000A1030 File Offset: 0x0009F230
		[HideInInspector]
		public event Interactable.OnAttachedToHandDelegate onAttachedToHand;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x0600209B RID: 8347 RVA: 0x000A1068 File Offset: 0x0009F268
		// (remove) Token: 0x0600209C RID: 8348 RVA: 0x000A10A0 File Offset: 0x0009F2A0
		[HideInInspector]
		public event Interactable.OnDetachedFromHandDelegate onDetachedFromHand;

		// Token: 0x0600209D RID: 8349 RVA: 0x000A10D5 File Offset: 0x0009F2D5
		private void OnAttachedToHand(Hand hand)
		{
			if (this.onAttachedToHand != null)
			{
				this.onAttachedToHand(hand);
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000A10EB File Offset: 0x0009F2EB
		private void OnDetachedFromHand(Hand hand)
		{
			if (this.onDetachedFromHand != null)
			{
				this.onDetachedFromHand(hand);
			}
		}

		// Token: 0x02000779 RID: 1913
		// (Invoke) Token: 0x06002FC8 RID: 12232
		public delegate void OnAttachedToHandDelegate(Hand hand);

		// Token: 0x0200077A RID: 1914
		// (Invoke) Token: 0x06002FCC RID: 12236
		public delegate void OnDetachedFromHandDelegate(Hand hand);
	}
}
