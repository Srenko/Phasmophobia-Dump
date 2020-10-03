using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000364 RID: 868
	public class Remote_Beam : MonoBehaviour
	{
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0009840F File Offset: 0x0009660F
		public void SetTouchAxis(Vector2 data)
		{
			this.touchAxis = data;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00098418 File Offset: 0x00096618
		private void FixedUpdate()
		{
			this.currentYaw += this.touchAxis.y * this.rotationSpeed * Time.deltaTime;
			this.currentPitch += this.touchAxis.x * this.rotationSpeed * Time.deltaTime;
			base.transform.localRotation = Quaternion.AngleAxis(this.currentPitch, Vector3.up) * Quaternion.AngleAxis(this.currentYaw, Vector3.left);
		}

		// Token: 0x040017A0 RID: 6048
		private Vector2 touchAxis;

		// Token: 0x040017A1 RID: 6049
		private float rotationSpeed = 180f;

		// Token: 0x040017A2 RID: 6050
		private float currentYaw;

		// Token: 0x040017A3 RID: 6051
		private float currentPitch;
	}
}
