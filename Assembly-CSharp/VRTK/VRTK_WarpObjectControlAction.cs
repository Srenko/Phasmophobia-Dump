using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C7 RID: 711
	[AddComponentMenu("VRTK/Scripts/Locomotion/Object Control Actions/VRTK_WarpObjectControlAction")]
	public class VRTK_WarpObjectControlAction : VRTK_BaseObjectControlAction
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x0007E155 File Offset: 0x0007C355
		protected override void Process(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, float deadzone, bool currentlyFalling, bool modifierActive)
		{
			if (this.warpDelayTimer < Time.time && axis != 0f)
			{
				this.Warp(controlledGameObject, directionDevice, axisDirection, axis, modifierActive);
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0007E17A File Offset: 0x0007C37A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0007E190 File Offset: 0x0007C390
		protected virtual void Warp(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, bool modifierActive)
		{
			Vector3 objectCenter = this.GetObjectCenter(controlledGameObject.transform);
			Vector3 vector = controlledGameObject.transform.TransformPoint(objectCenter);
			float num = this.warpDistance * (modifierActive ? this.warpMultiplier : 1f);
			int axisDirection2 = this.GetAxisDirection(axis);
			Vector3 a = vector + axisDirection * num * (float)axisDirection2;
			float num2 = 0.2f;
			Vector3 vector2 = (float)axisDirection2 * axisDirection;
			RaycastHit raycastHit;
			if (Physics.Raycast(((controlledGameObject.transform == this.playArea) ? this.headset.position : controlledGameObject.transform.position) + Vector3.up * num2, vector2, out raycastHit, num - this.colliderRadius))
			{
				a = raycastHit.point - vector2 * this.colliderRadius;
			}
			if (Physics.Raycast(a + Vector3.up * (this.floorHeightTolerance + num2), Vector3.down, out raycastHit, (this.floorHeightTolerance + num2) * 2f))
			{
				a.y = raycastHit.point.y + this.colliderHeight / 2f;
				Vector3 vector3 = a - vector + controlledGameObject.transform.position;
				this.warpDelayTimer = Time.time + this.warpDelay;
				if (this.CanMove(this.bodyPhysics, controlledGameObject.transform.position, vector3))
				{
					controlledGameObject.transform.position = vector3;
					this.Blink(this.blinkTransitionSpeed);
				}
			}
		}

		// Token: 0x0400132F RID: 4911
		[Header("Warp Settings")]
		[Tooltip("The distance to warp in the facing direction.")]
		public float warpDistance = 1f;

		// Token: 0x04001330 RID: 4912
		[Tooltip("The multiplier to be applied to the warp when the modifier button is pressed.")]
		public float warpMultiplier = 2f;

		// Token: 0x04001331 RID: 4913
		[Tooltip("The amount of time required to pass before another warp can be carried out.")]
		public float warpDelay = 0.5f;

		// Token: 0x04001332 RID: 4914
		[Tooltip("The height different in floor allowed to be a valid warp.")]
		public float floorHeightTolerance = 1f;

		// Token: 0x04001333 RID: 4915
		[Tooltip("The speed for the headset to fade out and back in. Having a blink between warps can reduce nausia.")]
		public float blinkTransitionSpeed = 0.6f;

		// Token: 0x04001334 RID: 4916
		[Header("Custom Settings")]
		[Tooltip("An optional Body Physics script to check for potential collisions in the moving direction. If any potential collision is found then the move will not take place. This can help reduce collision tunnelling.")]
		public VRTK_BodyPhysics bodyPhysics;

		// Token: 0x04001335 RID: 4917
		protected float warpDelayTimer;

		// Token: 0x04001336 RID: 4918
		protected Transform headset;
	}
}
