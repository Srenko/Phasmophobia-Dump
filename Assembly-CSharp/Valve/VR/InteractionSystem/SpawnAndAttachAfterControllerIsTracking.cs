using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000436 RID: 1078
	public class SpawnAndAttachAfterControllerIsTracking : MonoBehaviour
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x000A2C9F File Offset: 0x000A0E9F
		private void Start()
		{
			this.hand = base.GetComponentInParent<Hand>();
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000A2CB0 File Offset: 0x000A0EB0
		private void Update()
		{
			if (this.itemPrefab != null && this.hand.controller != null && this.hand.controller.hasTracking)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemPrefab);
				gameObject.SetActive(true);
				this.hand.AttachObject(gameObject, Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand, "");
				this.hand.controller.TriggerHapticPulse(800, EVRButtonId.k_EButton_Axis0);
				Object.Destroy(base.gameObject);
				gameObject.transform.localScale = this.itemPrefab.transform.localScale;
			}
		}

		// Token: 0x04001E89 RID: 7817
		private Hand hand;

		// Token: 0x04001E8A RID: 7818
		public GameObject itemPrefab;
	}
}
