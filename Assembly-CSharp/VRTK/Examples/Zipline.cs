using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000374 RID: 884
	public class Zipline : VRTK_InteractableObject
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x0009A2AD File Offset: 0x000984AD
		public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
		{
			base.OnInteractableObjectGrabbed(e);
			this.isMoving = true;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0009A2BD File Offset: 0x000984BD
		protected override void Awake()
		{
			base.Awake();
			this.currentSpeed = this.downStartSpeed;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0009A2D4 File Offset: 0x000984D4
		protected override void Update()
		{
			base.Update();
			if (this.isMoving)
			{
				Vector3 b;
				if (this.isMovingDown)
				{
					this.currentSpeed += this.acceleration * Time.deltaTime;
					b = Vector3.down * this.currentSpeed * Time.deltaTime;
				}
				else
				{
					b = Vector3.up * this.upSpeed * Time.deltaTime;
				}
				this.handle.transform.localPosition += b;
				if ((this.isMovingDown && this.handle.transform.localPosition.y <= this.handleEndPosition.localPosition.y) || (!this.isMovingDown && this.handle.transform.localPosition.y >= this.handleStartPosition.localPosition.y))
				{
					this.isMoving = false;
					this.isMovingDown = !this.isMovingDown;
					this.currentSpeed = this.downStartSpeed;
				}
			}
		}

		// Token: 0x040017C9 RID: 6089
		[Header("Zipline Options", order = 4)]
		public float downStartSpeed = 0.2f;

		// Token: 0x040017CA RID: 6090
		public float acceleration = 1f;

		// Token: 0x040017CB RID: 6091
		public float upSpeed = 1f;

		// Token: 0x040017CC RID: 6092
		public Transform handleEndPosition;

		// Token: 0x040017CD RID: 6093
		public Transform handleStartPosition;

		// Token: 0x040017CE RID: 6094
		public GameObject handle;

		// Token: 0x040017CF RID: 6095
		private bool isMoving;

		// Token: 0x040017D0 RID: 6096
		private bool isMovingDown = true;

		// Token: 0x040017D1 RID: 6097
		private float currentSpeed;
	}
}
