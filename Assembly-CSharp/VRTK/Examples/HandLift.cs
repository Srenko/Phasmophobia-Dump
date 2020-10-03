using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000357 RID: 855
	public class HandLift : VRTK_InteractableObject
	{
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0009722B File Offset: 0x0009542B
		public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
		{
			base.OnInteractableObjectGrabbed(e);
			this.isMoving = true;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0009723C File Offset: 0x0009543C
		protected override void Update()
		{
			base.Update();
			if (this.isMoving)
			{
				Vector3 b = (this.isMovingUp ? Vector3.up : Vector3.down) * this.speed * Time.deltaTime;
				this.handle.transform.position += b;
				Vector3 localScale = this.rope.transform.localScale;
				localScale.y = (this.ropeTop.position.y - this.handle.transform.position.y) / 2f;
				Vector3 position = this.ropeTop.transform.position;
				position.y -= localScale.y;
				this.rope.transform.localScale = localScale;
				this.rope.transform.position = position;
				if ((!this.isMovingUp && this.handle.transform.position.y <= this.ropeBottom.position.y) || (this.isMovingUp && this.handle.transform.position.y >= this.handleTop.position.y))
				{
					this.isMoving = false;
					this.isMovingUp = !this.isMovingUp;
				}
			}
		}

		// Token: 0x04001761 RID: 5985
		[Header("Hand Lift Options", order = 4)]
		public float speed = 0.1f;

		// Token: 0x04001762 RID: 5986
		public Transform handleTop;

		// Token: 0x04001763 RID: 5987
		public Transform ropeTop;

		// Token: 0x04001764 RID: 5988
		public Transform ropeBottom;

		// Token: 0x04001765 RID: 5989
		public GameObject rope;

		// Token: 0x04001766 RID: 5990
		public GameObject handle;

		// Token: 0x04001767 RID: 5991
		private bool isMoving;

		// Token: 0x04001768 RID: 5992
		private bool isMovingUp = true;
	}
}
