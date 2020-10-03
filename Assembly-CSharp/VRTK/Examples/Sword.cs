using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000369 RID: 873
	public class Sword : VRTK_InteractableObject
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x0009894F File Offset: 0x00096B4F
		public float CollisionForce()
		{
			return this.collisionForce;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00098957 File Offset: 0x00096B57
		public override void Grabbed(VRTK_InteractGrab grabbingObject)
		{
			base.Grabbed(grabbingObject);
			this.controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00098976 File Offset: 0x00096B76
		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			base.Ungrabbed(previousGrabbingObject);
			this.controllerReference = null;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00098986 File Offset: 0x00096B86
		public override void OnEnable()
		{
			base.OnEnable();
			this.controllerReference = null;
			this.interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000989A4 File Offset: 0x00096BA4
		private void OnCollisionEnter(Collision collision)
		{
			if (VRTK_ControllerReference.IsValid(this.controllerReference) && this.IsGrabbed(null))
			{
				this.collisionForce = VRTK_DeviceFinder.GetControllerVelocity(this.controllerReference).magnitude * this.impactMagnifier;
				float strength = this.collisionForce / this.maxCollisionForce;
				VRTK_ControllerHaptics.TriggerHapticPulse(this.controllerReference, strength, 0.5f, 0.01f);
				return;
			}
			this.collisionForce = collision.relativeVelocity.magnitude * this.impactMagnifier;
		}

		// Token: 0x040017AC RID: 6060
		private float impactMagnifier = 120f;

		// Token: 0x040017AD RID: 6061
		private float collisionForce;

		// Token: 0x040017AE RID: 6062
		private float maxCollisionForce = 4000f;

		// Token: 0x040017AF RID: 6063
		private VRTK_ControllerReference controllerReference;
	}
}
