using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200036C RID: 876
	public class UseRotate : VRTK_InteractableObject
	{
		// Token: 0x06001E1B RID: 7707 RVA: 0x00098BA5 File Offset: 0x00096DA5
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.spinSpeed = this.activeSpinSpeed;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00098BBA File Offset: 0x00096DBA
		public override void StopUsing(VRTK_InteractUse usingObject)
		{
			base.StopUsing(usingObject);
			this.spinSpeed = this.idleSpinSpeed;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00098BCF File Offset: 0x00096DCF
		protected void Start()
		{
			if (this.rotatingObject == null)
			{
				this.rotatingObject = base.transform;
			}
			this.spinSpeed = this.idleSpinSpeed;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00098BF7 File Offset: 0x00096DF7
		protected override void Update()
		{
			base.Update();
			this.rotatingObject.Rotate(this.rotationAxis, this.spinSpeed * Time.deltaTime);
		}

		// Token: 0x040017B2 RID: 6066
		[Header("Rotation when in use")]
		[SerializeField]
		[Tooltip("Rotation speed when not in use (deg/sec)")]
		private float idleSpinSpeed;

		// Token: 0x040017B3 RID: 6067
		[SerializeField]
		[Tooltip("Rotation speed when in use (deg/sec)")]
		private float activeSpinSpeed = 360f;

		// Token: 0x040017B4 RID: 6068
		[Tooltip("Game object to rotate\n(leave empty to use this object)")]
		[SerializeField]
		private Transform rotatingObject;

		// Token: 0x040017B5 RID: 6069
		[SerializeField]
		private Vector3 rotationAxis = Vector3.up;

		// Token: 0x040017B6 RID: 6070
		private float spinSpeed;
	}
}
