using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000437 RID: 1079
	public class SpawnAndAttachToHand : MonoBehaviour
	{
		// Token: 0x06002102 RID: 8450 RVA: 0x000A2D50 File Offset: 0x000A0F50
		public void SpawnAndAttach(Hand passedInhand)
		{
			Hand hand = passedInhand;
			if (passedInhand == null)
			{
				hand = this.hand;
			}
			if (hand == null)
			{
				return;
			}
			GameObject objectToAttach = Object.Instantiate<GameObject>(this.prefab);
			hand.AttachObject(objectToAttach, Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand, "");
		}

		// Token: 0x04001E8B RID: 7819
		public Hand hand;

		// Token: 0x04001E8C RID: 7820
		public GameObject prefab;
	}
}
