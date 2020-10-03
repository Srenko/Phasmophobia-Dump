using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C6 RID: 710
	[AddComponentMenu("VRTK/Scripts/Locomotion/Object Control Actions/VRTK_SnapRotateObjectControlAction")]
	public class VRTK_SnapRotateObjectControlAction : VRTK_BaseObjectControlAction
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x0007E034 File Offset: 0x0007C234
		protected override void Process(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, float deadzone, bool currentlyFalling, bool modifierActive)
		{
			this.CheckForPlayerBeforeRotation(controlledGameObject);
			if (this.snapDelayTimer < Time.time && this.ValidThreshold(axis))
			{
				float num = this.Rotate(axis, modifierActive);
				if (num != 0f)
				{
					this.Blink(this.blinkTransitionSpeed);
					this.RotateAroundPlayer(controlledGameObject, num);
				}
			}
			this.CheckForPlayerAfterRotation(controlledGameObject);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0007E090 File Offset: 0x0007C290
		protected virtual bool ValidThreshold(float axis)
		{
			return this.axisThreshold == 0f || (this.axisThreshold > 0f && axis >= this.axisThreshold) || (this.axisThreshold < 0f && axis <= this.axisThreshold);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0007E0E0 File Offset: 0x0007C2E0
		protected virtual float Rotate(float axis, bool modifierActive)
		{
			this.snapDelayTimer = Time.time + this.snapDelay;
			int axisDirection = this.GetAxisDirection(axis);
			return this.anglePerSnap * (modifierActive ? this.angleMultiplier : 1f) * (float)axisDirection;
		}

		// Token: 0x04001329 RID: 4905
		[Tooltip("The angle to rotate for each snap.")]
		public float anglePerSnap = 30f;

		// Token: 0x0400132A RID: 4906
		[Tooltip("The snap angle multiplier to be applied when the modifier button is pressed.")]
		public float angleMultiplier = 1.5f;

		// Token: 0x0400132B RID: 4907
		[Tooltip("The amount of time required to pass before another snap rotation can be carried out.")]
		public float snapDelay = 0.5f;

		// Token: 0x0400132C RID: 4908
		[Tooltip("The speed for the headset to fade out and back in. Having a blink between rotations can reduce nausia.")]
		public float blinkTransitionSpeed = 0.6f;

		// Token: 0x0400132D RID: 4909
		[Range(-1f, 1f)]
		[Tooltip("The threshold the listened axis needs to exceed before the action occurs. This can be used to limit the snap rotate to a single axis direction (e.g. pull down to flip rotate). The threshold is ignored if it is 0.")]
		public float axisThreshold;

		// Token: 0x0400132E RID: 4910
		protected float snapDelayTimer;
	}
}
