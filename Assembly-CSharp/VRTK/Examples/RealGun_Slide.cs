using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000363 RID: 867
	public class RealGun_Slide : VRTK_InteractableObject
	{
		// Token: 0x06001DEC RID: 7660 RVA: 0x0009826F File Offset: 0x0009646F
		public void Fire()
		{
			this.fireTimer = this.fireDistance;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0009827D File Offset: 0x0009647D
		protected override void Awake()
		{
			base.Awake();
			this.restPosition = base.transform.localPosition.z;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0009829C File Offset: 0x0009649C
		protected override void Update()
		{
			base.Update();
			if (base.transform.localPosition.z >= this.restPosition)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, this.restPosition);
			}
			if (this.fireTimer == 0f && base.transform.localPosition.z < this.restPosition && !this.IsGrabbed(null))
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + this.boltSpeed);
			}
			if (this.fireTimer > 0f)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z - this.boltSpeed);
				this.fireTimer -= this.boltSpeed;
			}
			if (this.fireTimer < 0f)
			{
				this.fireTimer = 0f;
			}
		}

		// Token: 0x0400179C RID: 6044
		private float restPosition;

		// Token: 0x0400179D RID: 6045
		private float fireTimer;

		// Token: 0x0400179E RID: 6046
		private float fireDistance = 0.05f;

		// Token: 0x0400179F RID: 6047
		private float boltSpeed = 0.01f;
	}
}
