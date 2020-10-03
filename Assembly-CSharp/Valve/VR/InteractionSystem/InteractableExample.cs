using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200044D RID: 1101
	[RequireComponent(typeof(Interactable))]
	public class InteractableExample : MonoBehaviour
	{
		// Token: 0x060021D8 RID: 8664 RVA: 0x000A78D8 File Offset: 0x000A5AD8
		private void Awake()
		{
			this.textMesh = base.GetComponentInChildren<TextMesh>();
			this.textMesh.text = "No Hand Hovering";
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000A78F6 File Offset: 0x000A5AF6
		private void OnHandHoverBegin(Hand hand)
		{
			this.textMesh.text = "Hovering hand: " + hand.name;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000A7913 File Offset: 0x000A5B13
		private void OnHandHoverEnd(Hand hand)
		{
			this.textMesh.text = "No Hand Hovering";
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000A7928 File Offset: 0x000A5B28
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown() || (hand.controller != null && hand.controller.GetPressDown(EVRButtonId.k_EButton_Grip)))
			{
				if (hand.currentAttachedObject != base.gameObject)
				{
					this.oldPosition = base.transform.position;
					this.oldRotation = base.transform.rotation;
					hand.HoverLock(base.GetComponent<Interactable>());
					hand.AttachObject(base.gameObject, this.attachmentFlags, "");
					return;
				}
				hand.DetachObject(base.gameObject, true);
				hand.HoverUnlock(base.GetComponent<Interactable>());
				base.transform.position = this.oldPosition;
				base.transform.rotation = this.oldRotation;
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000A79ED File Offset: 0x000A5BED
		private void OnAttachedToHand(Hand hand)
		{
			this.textMesh.text = "Attached to hand: " + hand.name;
			this.attachTime = Time.time;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000A7A15 File Offset: 0x000A5C15
		private void OnDetachedFromHand(Hand hand)
		{
			this.textMesh.text = "Detached from hand: " + hand.name;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000A7A34 File Offset: 0x000A5C34
		private void HandAttachedUpdate(Hand hand)
		{
			this.textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + (Time.time - this.attachTime).ToString("F2");
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00003F60 File Offset: 0x00002160
		private void OnHandFocusAcquired(Hand hand)
		{
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00003F60 File Offset: 0x00002160
		private void OnHandFocusLost(Hand hand)
		{
		}

		// Token: 0x04001F5C RID: 8028
		private TextMesh textMesh;

		// Token: 0x04001F5D RID: 8029
		private Vector3 oldPosition;

		// Token: 0x04001F5E RID: 8030
		private Quaternion oldRotation;

		// Token: 0x04001F5F RID: 8031
		private float attachTime;

		// Token: 0x04001F60 RID: 8032
		private Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand;
	}
}
