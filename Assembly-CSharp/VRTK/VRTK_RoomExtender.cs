using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D6 RID: 726
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_RoomExtender")]
	public class VRTK_RoomExtender : MonoBehaviour
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00080AC0 File Offset: 0x0007ECC0
		protected virtual void OnEnable()
		{
			this.movementTransform = VRTK_DeviceFinder.HeadsetTransform();
			if (this.movementTransform == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_RoomExtender",
					"Headset Transform"
				}));
			}
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.additionalMovementEnabled = !this.additionalMovementEnabledOnButtonPress;
			if (this.debugTransform)
			{
				this.debugTransform.localScale = new Vector3(this.headZoneRadius * 2f, 0.01f, this.headZoneRadius * 2f);
			}
			this.MoveHeadCircleNonLinearDrift();
			this.lastPosition = this.movementTransform.localPosition;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00080B74 File Offset: 0x0007ED74
		protected virtual void Update()
		{
			VRTK_RoomExtender.MovementFunction movementFunction = this.movementFunction;
			if (movementFunction == VRTK_RoomExtender.MovementFunction.Nonlinear)
			{
				this.MoveHeadCircleNonLinearDrift();
				return;
			}
			if (movementFunction != VRTK_RoomExtender.MovementFunction.LinearDirect)
			{
				return;
			}
			this.MoveHeadCircle();
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00080BA0 File Offset: 0x0007EDA0
		protected virtual void Move(Vector3 movement)
		{
			this.headCirclePosition += movement;
			if (this.debugTransform)
			{
				this.debugTransform.localPosition = new Vector3(this.headCirclePosition.x, this.debugTransform.localPosition.y, this.headCirclePosition.z);
			}
			if (this.additionalMovementEnabled)
			{
				this.playArea.localPosition += movement * this.additionalMovementMultiplier;
				this.relativeMovementOfCameraRig += movement * this.additionalMovementMultiplier;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00080C4C File Offset: 0x0007EE4C
		protected virtual void MoveHeadCircle()
		{
			Vector3 vector = new Vector3(this.movementTransform.localPosition.x - this.headCirclePosition.x, 0f, this.movementTransform.localPosition.z - this.headCirclePosition.z);
			this.UpdateLastMovement();
			if (vector.sqrMagnitude > this.headZoneRadius * this.headZoneRadius && this.lastMovement != Vector3.zero)
			{
				this.Move(this.lastMovement);
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00080CD8 File Offset: 0x0007EED8
		protected virtual void MoveHeadCircleNonLinearDrift()
		{
			Vector3 vector = new Vector3(this.movementTransform.localPosition.x - this.headCirclePosition.x, 0f, this.movementTransform.localPosition.z - this.headCirclePosition.z);
			if (vector.sqrMagnitude > this.headZoneRadius * this.headZoneRadius)
			{
				Vector3 movement = vector.normalized * (vector.magnitude - this.headZoneRadius);
				this.Move(movement);
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00080D61 File Offset: 0x0007EF61
		protected virtual void UpdateLastMovement()
		{
			this.lastMovement = this.movementTransform.localPosition - this.lastPosition;
			this.lastMovement.y = 0f;
			this.lastPosition = this.movementTransform.localPosition;
		}

		// Token: 0x040013B1 RID: 5041
		[Tooltip("This determines the type of movement used by the extender.")]
		public VRTK_RoomExtender.MovementFunction movementFunction = VRTK_RoomExtender.MovementFunction.LinearDirect;

		// Token: 0x040013B2 RID: 5042
		[Tooltip("This is the a public variable to enable the additional movement. This can be used in other scripts to toggle the play area movement.")]
		public bool additionalMovementEnabled = true;

		// Token: 0x040013B3 RID: 5043
		[Tooltip("This configures the controls of the RoomExtender. If this is true then the touchpad needs to be pressed to enable it. If this is false then it is disabled by pressing the touchpad.")]
		public bool additionalMovementEnabledOnButtonPress = true;

		// Token: 0x040013B4 RID: 5044
		[Tooltip("This is the factor by which movement at the edge of the circle is amplified. 0 is no movement of the play area. Higher values simulate a bigger play area but may be too uncomfortable.")]
		[Range(0f, 10f)]
		public float additionalMovementMultiplier = 1f;

		// Token: 0x040013B5 RID: 5045
		[Tooltip("This is the size of the circle in which the playArea is not moved and everything is normal. If it is to low it becomes uncomfortable when crouching.")]
		[Range(0f, 5f)]
		public float headZoneRadius = 0.25f;

		// Token: 0x040013B6 RID: 5046
		[Tooltip("This transform visualises the circle around the user where the play area is not moved. In the demo scene this is a cylinder at floor level. Remember to turn of collisions.")]
		public Transform debugTransform;

		// Token: 0x040013B7 RID: 5047
		[HideInInspector]
		public Vector3 relativeMovementOfCameraRig;

		// Token: 0x040013B8 RID: 5048
		protected Transform movementTransform;

		// Token: 0x040013B9 RID: 5049
		protected Transform playArea;

		// Token: 0x040013BA RID: 5050
		protected Vector3 headCirclePosition;

		// Token: 0x040013BB RID: 5051
		protected Vector3 lastPosition;

		// Token: 0x040013BC RID: 5052
		protected Vector3 lastMovement;

		// Token: 0x020005EA RID: 1514
		public enum MovementFunction
		{
			// Token: 0x040027FC RID: 10236
			Nonlinear,
			// Token: 0x040027FD RID: 10237
			LinearDirect
		}
	}
}
