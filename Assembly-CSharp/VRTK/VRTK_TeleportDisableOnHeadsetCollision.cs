using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D8 RID: 728
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_TeleportDisableOnHeadsetCollision")]
	public class VRTK_TeleportDisableOnHeadsetCollision : MonoBehaviour
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x00080E74 File Offset: 0x0007F074
		protected virtual void OnEnable()
		{
			this.basicTeleport = base.GetComponent<VRTK_BasicTeleport>();
			base.StartCoroutine(this.EnableAtEndOfFrame());
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00080E90 File Offset: 0x0007F090
		protected virtual void OnDisable()
		{
			if (this.basicTeleport == null)
			{
				return;
			}
			if (this.headsetCollision)
			{
				this.headsetCollision.HeadsetCollisionDetect -= this.DisableTeleport;
				this.headsetCollision.HeadsetCollisionEnded -= this.EnableTeleport;
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00080EE9 File Offset: 0x0007F0E9
		protected virtual IEnumerator EnableAtEndOfFrame()
		{
			if (this.basicTeleport == null)
			{
				yield break;
			}
			yield return new WaitForEndOfFrame();
			this.headsetCollision = VRTK_ObjectCache.registeredHeadsetCollider;
			if (this.headsetCollision)
			{
				this.headsetCollision.HeadsetCollisionDetect += this.DisableTeleport;
				this.headsetCollision.HeadsetCollisionEnded += this.EnableTeleport;
			}
			yield break;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00080EF8 File Offset: 0x0007F0F8
		protected virtual void DisableTeleport(object sender, HeadsetCollisionEventArgs e)
		{
			this.basicTeleport.ToggleTeleportEnabled(false);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00080F06 File Offset: 0x0007F106
		protected virtual void EnableTeleport(object sender, HeadsetCollisionEventArgs e)
		{
			this.basicTeleport.ToggleTeleportEnabled(true);
		}

		// Token: 0x040013BF RID: 5055
		protected VRTK_BasicTeleport basicTeleport;

		// Token: 0x040013C0 RID: 5056
		protected VRTK_HeadsetCollision headsetCollision;
	}
}
