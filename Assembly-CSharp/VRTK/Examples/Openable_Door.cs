using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035E RID: 862
	public class Openable_Door : VRTK_InteractableObject
	{
		// Token: 0x06001DC3 RID: 7619 RVA: 0x00097885 File Offset: 0x00095A85
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.SetDoorRotation(usingObject.transform.position);
			this.SetRotation();
			this.open = !this.open;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000978B4 File Offset: 0x00095AB4
		protected void Start()
		{
			this.defaultRotation = base.transform.eulerAngles;
			this.SetRotation();
			this.sideFlip = (float)(this.flipped ? 1 : -1);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000978E0 File Offset: 0x00095AE0
		protected override void Update()
		{
			base.Update();
			if (this.open)
			{
				base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.Euler(this.openRotation), Time.deltaTime * this.smooth);
				return;
			}
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.Euler(this.defaultRotation), Time.deltaTime * this.smooth);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00097960 File Offset: 0x00095B60
		private void SetRotation()
		{
			this.openRotation = new Vector3(this.defaultRotation.x, this.defaultRotation.y + this.doorOpenAngle * (this.sideFlip * this.side), this.defaultRotation.z);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000979B0 File Offset: 0x00095BB0
		private void SetDoorRotation(Vector3 interacterPosition)
		{
			this.side = (float)(((!this.rotated && interacterPosition.z > base.transform.position.z) || (this.rotated && interacterPosition.x > base.transform.position.x)) ? -1 : 1);
		}

		// Token: 0x04001776 RID: 6006
		public bool flipped;

		// Token: 0x04001777 RID: 6007
		public bool rotated;

		// Token: 0x04001778 RID: 6008
		private float sideFlip = -1f;

		// Token: 0x04001779 RID: 6009
		private float side = -1f;

		// Token: 0x0400177A RID: 6010
		private float smooth = 270f;

		// Token: 0x0400177B RID: 6011
		private float doorOpenAngle = -90f;

		// Token: 0x0400177C RID: 6012
		private bool open;

		// Token: 0x0400177D RID: 6013
		private Vector3 defaultRotation;

		// Token: 0x0400177E RID: 6014
		private Vector3 openRotation;
	}
}
