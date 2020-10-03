using System;
using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000495 RID: 1173
	public class ThirdPersonController : BaseController
	{
		// Token: 0x0600248F RID: 9359 RVA: 0x000B28CC File Offset: 0x000B0ACC
		protected override void Move(float h, float v)
		{
			this.rigidBody.velocity = v * this.speed * base.transform.forward;
			base.transform.rotation *= Quaternion.AngleAxis(this.movingTurnSpeed * h * Time.deltaTime, Vector3.up);
		}

		// Token: 0x040021B7 RID: 8631
		[SerializeField]
		private float movingTurnSpeed = 360f;
	}
}
