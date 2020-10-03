using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F2 RID: 754
	public class VRTK_HeadsetCollider : MonoBehaviour
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0008AF2C File Offset: 0x0008912C
		public virtual void SetParent(GameObject setParent)
		{
			this.parent = setParent.GetComponent<VRTK_HeadsetCollision>();
			base.gameObject.tag = "Player";
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0008AF4A File Offset: 0x0008914A
		public virtual void SetIgnoreTarget(VRTK_PolicyList list = null)
		{
			this.targetListPolicy = list;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0008AF54 File Offset: 0x00089154
		public virtual void EndCollision(Collider collider)
		{
			if (collider == null || !VRTK_PlayerObject.IsPlayerObject(collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null))
			{
				this.parent.headsetColliding = false;
				this.parent.collidingWith = null;
				this.parent.OnHeadsetCollisionEnded(this.SetHeadsetCollisionEvent(collider, base.transform));
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0008AFA8 File Offset: 0x000891A8
		protected virtual void OnTriggerStay(Collider collider)
		{
			if (this.parent.ignoreTriggerColliders && collider != null && collider.isTrigger)
			{
				return;
			}
			if (base.enabled && !VRTK_PlayerObject.IsPlayerObject(collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null) && this.ValidTarget(collider.transform))
			{
				this.parent.headsetColliding = true;
				this.parent.collidingWith = collider;
				this.parent.OnHeadsetCollisionDetect(this.SetHeadsetCollisionEvent(collider, base.transform));
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0008B028 File Offset: 0x00089228
		protected virtual void OnTriggerExit(Collider collider)
		{
			if (this.parent.ignoreTriggerColliders && collider != null && collider.isTrigger)
			{
				return;
			}
			this.EndCollision(collider);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0008B050 File Offset: 0x00089250
		protected virtual void Update()
		{
			if (this.parent.headsetColliding && (this.parent.collidingWith == null || !this.parent.collidingWith.gameObject.activeInHierarchy))
			{
				this.EndCollision(this.parent.collidingWith);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0008B0A8 File Offset: 0x000892A8
		protected virtual HeadsetCollisionEventArgs SetHeadsetCollisionEvent(Collider collider, Transform currentTransform)
		{
			HeadsetCollisionEventArgs result;
			result.collider = collider;
			result.currentTransform = currentTransform;
			return result;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0008B0C6 File Offset: 0x000892C6
		protected virtual bool ValidTarget(Transform target)
		{
			return target != null && !VRTK_PolicyList.Check(target.gameObject, this.targetListPolicy);
		}

		// Token: 0x04001541 RID: 5441
		protected VRTK_HeadsetCollision parent;

		// Token: 0x04001542 RID: 5442
		protected VRTK_PolicyList targetListPolicy;
	}
}
