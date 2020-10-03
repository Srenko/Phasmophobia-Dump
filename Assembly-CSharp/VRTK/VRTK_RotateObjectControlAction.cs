using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C4 RID: 708
	[AddComponentMenu("VRTK/Scripts/Locomotion/Object Control Actions/VRTK_RotateObjectControlAction")]
	public class VRTK_RotateObjectControlAction : VRTK_BaseObjectControlAction
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x0007DDA0 File Offset: 0x0007BFA0
		protected override void Process(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, float deadzone, bool currentlyFalling, bool modifierActive)
		{
			this.CheckForPlayerBeforeRotation(controlledGameObject);
			float num = this.Rotate(axis, modifierActive);
			if (num != 0f)
			{
				this.RotateAroundPlayer(controlledGameObject, num);
			}
			this.CheckForPlayerAfterRotation(controlledGameObject);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0007DDD6 File Offset: 0x0007BFD6
		protected virtual float Rotate(float axis, bool modifierActive)
		{
			return axis * this.maximumRotationSpeed * Time.deltaTime * (modifierActive ? this.rotationMultiplier : 1f) * 10f;
		}

		// Token: 0x04001320 RID: 4896
		[Tooltip("The maximum speed the controlled object can be rotated based on the position of the axis.")]
		public float maximumRotationSpeed = 3f;

		// Token: 0x04001321 RID: 4897
		[Tooltip("The rotation multiplier to be applied when the modifier button is pressed.")]
		public float rotationMultiplier = 1.5f;
	}
}
