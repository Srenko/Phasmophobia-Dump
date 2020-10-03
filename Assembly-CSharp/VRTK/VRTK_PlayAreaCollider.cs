using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002E9 RID: 745
	public class VRTK_PlayAreaCollider : MonoBehaviour
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x00086D65 File Offset: 0x00084F65
		public virtual void SetParent(VRTK_PlayAreaCursor setParent)
		{
			this.parent = setParent;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00086D6E File Offset: 0x00084F6E
		public virtual void SetIgnoreTarget(VRTK_PolicyList list = null)
		{
			this.targetListPolicy = list;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00086D77 File Offset: 0x00084F77
		protected virtual void OnDisable()
		{
			if (this.parent != null)
			{
				this.parent.SetPlayAreaCursorCollision(false, null);
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00086D94 File Offset: 0x00084F94
		protected virtual void OnTriggerStay(Collider collider)
		{
			if (this.parent != null && this.parent.enabled && this.parent.gameObject.activeInHierarchy && this.ValidTarget(collider))
			{
				this.parent.SetPlayAreaCursorCollision(true, collider);
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00086DE4 File Offset: 0x00084FE4
		protected virtual void OnTriggerExit(Collider collider)
		{
			if (this.parent != null && this.ValidTarget(collider))
			{
				this.parent.SetPlayAreaCursorCollision(false, collider);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00086E0A File Offset: 0x0008500A
		protected virtual bool ValidTarget(Collider collider)
		{
			return !VRTK_PlayerObject.IsPlayerObject(collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null) && !VRTK_PolicyList.Check(collider.gameObject, this.targetListPolicy);
		}

		// Token: 0x040014B0 RID: 5296
		protected VRTK_PlayAreaCursor parent;

		// Token: 0x040014B1 RID: 5297
		protected VRTK_PolicyList targetListPolicy;
	}
}
