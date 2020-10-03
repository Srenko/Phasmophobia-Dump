using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002B2 RID: 690
	public class VRTK_CollisionTracker : MonoBehaviour
	{
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060016CC RID: 5836 RVA: 0x0007AD98 File Offset: 0x00078F98
		// (remove) Token: 0x060016CD RID: 5837 RVA: 0x0007ADD0 File Offset: 0x00078FD0
		public event CollisionTrackerEventHandler CollisionEnter;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060016CE RID: 5838 RVA: 0x0007AE08 File Offset: 0x00079008
		// (remove) Token: 0x060016CF RID: 5839 RVA: 0x0007AE40 File Offset: 0x00079040
		public event CollisionTrackerEventHandler CollisionStay;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060016D0 RID: 5840 RVA: 0x0007AE78 File Offset: 0x00079078
		// (remove) Token: 0x060016D1 RID: 5841 RVA: 0x0007AEB0 File Offset: 0x000790B0
		public event CollisionTrackerEventHandler CollisionExit;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060016D2 RID: 5842 RVA: 0x0007AEE8 File Offset: 0x000790E8
		// (remove) Token: 0x060016D3 RID: 5843 RVA: 0x0007AF20 File Offset: 0x00079120
		public event CollisionTrackerEventHandler TriggerEnter;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060016D4 RID: 5844 RVA: 0x0007AF58 File Offset: 0x00079158
		// (remove) Token: 0x060016D5 RID: 5845 RVA: 0x0007AF90 File Offset: 0x00079190
		public event CollisionTrackerEventHandler TriggerStay;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060016D6 RID: 5846 RVA: 0x0007AFC8 File Offset: 0x000791C8
		// (remove) Token: 0x060016D7 RID: 5847 RVA: 0x0007B000 File Offset: 0x00079200
		public event CollisionTrackerEventHandler TriggerExit;

		// Token: 0x060016D8 RID: 5848 RVA: 0x0007B035 File Offset: 0x00079235
		protected void OnCollisionEnterEvent(CollisionTrackerEventArgs e)
		{
			if (this.CollisionEnter != null)
			{
				this.CollisionEnter(this, e);
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0007B04C File Offset: 0x0007924C
		protected void OnCollisionStayEvent(CollisionTrackerEventArgs e)
		{
			if (this.CollisionStay != null)
			{
				this.CollisionStay(this, e);
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0007B063 File Offset: 0x00079263
		protected void OnCollisionExitEvent(CollisionTrackerEventArgs e)
		{
			if (this.CollisionExit != null)
			{
				this.CollisionExit(this, e);
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0007B07A File Offset: 0x0007927A
		protected void OnTriggerEnterEvent(CollisionTrackerEventArgs e)
		{
			if (this.TriggerEnter != null)
			{
				this.TriggerEnter(this, e);
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0007B091 File Offset: 0x00079291
		protected void OnTriggerStayEvent(CollisionTrackerEventArgs e)
		{
			if (this.TriggerStay != null)
			{
				this.TriggerStay(this, e);
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0007B0A8 File Offset: 0x000792A8
		protected void OnTriggerExitEvent(CollisionTrackerEventArgs e)
		{
			if (this.TriggerExit != null)
			{
				this.TriggerExit(this, e);
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0007B0BF File Offset: 0x000792BF
		protected virtual void OnCollisionEnter(Collision collision)
		{
			this.OnCollisionEnterEvent(this.SetCollisionTrackerEvent(false, collision, collision.collider));
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0007B0D5 File Offset: 0x000792D5
		protected virtual void OnCollisionStay(Collision collision)
		{
			this.OnCollisionStayEvent(this.SetCollisionTrackerEvent(false, collision, collision.collider));
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0007B0EB File Offset: 0x000792EB
		protected virtual void OnCollisionExit(Collision collision)
		{
			this.OnCollisionExitEvent(this.SetCollisionTrackerEvent(false, collision, collision.collider));
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0007B101 File Offset: 0x00079301
		protected virtual void OnTriggerEnter(Collider collider)
		{
			this.OnTriggerEnterEvent(this.SetCollisionTrackerEvent(true, null, collider));
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0007B112 File Offset: 0x00079312
		protected virtual void OnTriggerStay(Collider collider)
		{
			this.OnTriggerStayEvent(this.SetCollisionTrackerEvent(true, null, collider));
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0007B123 File Offset: 0x00079323
		protected virtual void OnTriggerExit(Collider collider)
		{
			this.OnTriggerExitEvent(this.SetCollisionTrackerEvent(true, null, collider));
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0007B134 File Offset: 0x00079334
		protected virtual CollisionTrackerEventArgs SetCollisionTrackerEvent(bool isTrigger, Collision givenCollision, Collider givenCollider)
		{
			CollisionTrackerEventArgs result;
			result.isTrigger = isTrigger;
			result.collision = givenCollision;
			result.collider = givenCollider;
			return result;
		}
	}
}
