using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C5 RID: 709
	[AddComponentMenu("VRTK/Scripts/Locomotion/Object Control Actions/VRTK_SlideObjectControlAction")]
	public class VRTK_SlideObjectControlAction : VRTK_BaseObjectControlAction
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0007DE1C File Offset: 0x0007C01C
		protected override void Process(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, float deadzone, bool currentlyFalling, bool modifierActive)
		{
			this.currentSpeed = this.CalculateSpeed(axis, currentlyFalling, modifierActive);
			this.Move(controlledGameObject, directionDevice, axisDirection);
			if (GameController.instance)
			{
				if (this.id == 1)
				{
					GameController.instance.myPlayer.player.footstepController.XAxisSpeed = this.currentSpeed;
					return;
				}
				if (this.id == 2)
				{
					GameController.instance.myPlayer.player.footstepController.YAxisSpeed = this.currentSpeed;
				}
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0007DEA4 File Offset: 0x0007C0A4
		protected virtual float CalculateSpeed(float inputValue, bool currentlyFalling, bool modifierActive)
		{
			float num = this.currentSpeed;
			if (inputValue != 0f)
			{
				num = this.maximumSpeed * inputValue;
				num = (modifierActive ? (num * this.speedMultiplier) : num);
			}
			else
			{
				num = this.Decelerate(num, currentlyFalling);
			}
			return num;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0007DEE4 File Offset: 0x0007C0E4
		protected virtual float Decelerate(float speed, bool currentlyFalling)
		{
			float num = currentlyFalling ? this.fallingDeceleration : this.deceleration;
			if (speed > 0f)
			{
				speed -= Mathf.Lerp(num, this.maximumSpeed, 0f);
			}
			else if (speed < 0f)
			{
				speed += Mathf.Lerp(num, -this.maximumSpeed, 0f);
			}
			else
			{
				speed = 0f;
			}
			if (speed < num && speed > -num)
			{
				speed = 0f;
			}
			return speed;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0007DF5C File Offset: 0x0007C15C
		protected virtual void Move(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection)
		{
			if (directionDevice && directionDevice.gameObject.activeInHierarchy && controlledGameObject.activeInHierarchy)
			{
				float y = controlledGameObject.transform.position.y;
				Vector3 b = axisDirection * this.currentSpeed * Time.deltaTime;
				Vector3 vector = controlledGameObject.transform.position + b;
				vector = new Vector3(vector.x, y, vector.z);
				if (this.CanMove(this.bodyPhysics, controlledGameObject.transform.position, vector))
				{
					controlledGameObject.transform.position = vector;
				}
			}
		}

		// Token: 0x04001322 RID: 4898
		[Header("Slide Settings")]
		[Tooltip("The maximum speed the controlled object can be moved in based on the position of the axis.")]
		public float maximumSpeed = 3f;

		// Token: 0x04001323 RID: 4899
		[Tooltip("The rate of speed deceleration when the axis is no longer being changed.")]
		public float deceleration = 0.1f;

		// Token: 0x04001324 RID: 4900
		[Tooltip("The rate of speed deceleration when the axis is no longer being changed and the object is falling.")]
		public float fallingDeceleration = 0.01f;

		// Token: 0x04001325 RID: 4901
		[Tooltip("The speed multiplier to be applied when the modifier button is pressed.")]
		public float speedMultiplier = 1.5f;

		// Token: 0x04001326 RID: 4902
		[Header("Custom Settings")]
		[Tooltip("An optional Body Physics script to check for potential collisions in the moving direction. If any potential collision is found then the move will not take place. This can help reduce collision tunnelling.")]
		public VRTK_BodyPhysics bodyPhysics;

		// Token: 0x04001327 RID: 4903
		protected float currentSpeed;

		// Token: 0x04001328 RID: 4904
		public int id;
	}
}
