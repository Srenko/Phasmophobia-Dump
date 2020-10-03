using System;
using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000490 RID: 1168
	public class FirstPersonController : BaseController
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000B1CEB File Offset: 0x000AFEEB
		public Vector3 Velocity
		{
			get
			{
				return this.rigidBody.velocity;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000B1CF8 File Offset: 0x000AFEF8
		protected override void SetCamera()
		{
			base.SetCamera();
			this.mouseLook.Init(base.transform, this.camTrans);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000B1D18 File Offset: 0x000AFF18
		protected override void Move(float h, float v)
		{
			Vector3 vector = this.camTrans.forward * v + this.camTrans.right * h;
			vector.x *= this.speed;
			vector.z *= this.speed;
			vector.y = 0f;
			this.rigidBody.velocity = vector;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000B1D8D File Offset: 0x000AFF8D
		private void Update()
		{
			this.RotateView();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000B1D98 File Offset: 0x000AFF98
		private void RotateView()
		{
			this.oldYRotation = base.transform.eulerAngles.y;
			this.mouseLook.LookRotation(base.transform, this.camTrans);
			this.velRotation = Quaternion.AngleAxis(base.transform.eulerAngles.y - this.oldYRotation, Vector3.up);
			this.rigidBody.velocity = this.velRotation * this.rigidBody.velocity;
		}

		// Token: 0x0400219B RID: 8603
		[SerializeField]
		private MouseLookHelper mouseLook = new MouseLookHelper();

		// Token: 0x0400219C RID: 8604
		private float oldYRotation;

		// Token: 0x0400219D RID: 8605
		private Quaternion velRotation;
	}
}
