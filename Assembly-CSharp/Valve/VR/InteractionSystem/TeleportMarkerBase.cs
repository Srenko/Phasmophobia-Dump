using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000454 RID: 1108
	public abstract class TeleportMarkerBase : MonoBehaviour
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000694B6 File Offset: 0x000676B6
		public virtual bool showReticle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A9CB9 File Offset: 0x000A7EB9
		public void SetLocked(bool locked)
		{
			this.locked = locked;
			this.UpdateVisuals();
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void TeleportPlayer(Vector3 pointedAtPosition)
		{
		}

		// Token: 0x06002230 RID: 8752
		public abstract void UpdateVisuals();

		// Token: 0x06002231 RID: 8753
		public abstract void Highlight(bool highlight);

		// Token: 0x06002232 RID: 8754
		public abstract void SetAlpha(float tintAlpha, float alphaPercent);

		// Token: 0x06002233 RID: 8755
		public abstract bool ShouldActivate(Vector3 playerPosition);

		// Token: 0x06002234 RID: 8756
		public abstract bool ShouldMovePlayer();

		// Token: 0x04001FD0 RID: 8144
		public bool locked;

		// Token: 0x04001FD1 RID: 8145
		public bool markerActive = true;
	}
}
