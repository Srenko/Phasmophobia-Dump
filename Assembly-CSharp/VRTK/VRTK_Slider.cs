using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x02000293 RID: 659
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Slider")]
	public class VRTK_Slider : VRTK_Control
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x00070A18 File Offset: 0x0006EC18
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (!base.enabled || !this.setupSuccessful)
			{
				return;
			}
			Gizmos.DrawLine(base.transform.position, this.minimumLimit.transform.position);
			Gizmos.DrawLine(base.transform.position, this.maximumLimit.transform.position);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00070A7C File Offset: 0x0006EC7C
		protected override void InitRequiredComponents()
		{
			this.DetectSetup();
			this.InitRigidbody();
			this.InitInteractableObject();
			this.InitJoint();
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00070A98 File Offset: 0x0006EC98
		protected override bool DetectSetup()
		{
			if (this.sliderJointCreated && this.connectedTo)
			{
				this.sliderJoint.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
			}
			this.finalDirection = this.direction;
			if (this.direction == VRTK_Control.Direction.autodetect)
			{
				RaycastHit raycastHit;
				bool flag = Physics.Raycast(base.transform.position, base.transform.right, out raycastHit);
				RaycastHit raycastHit2;
				bool flag2 = Physics.Raycast(base.transform.position, base.transform.up, out raycastHit2);
				RaycastHit raycastHit3;
				bool flag3 = Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit3);
				Vector3 vector = base.transform.localScale / 2f;
				if (flag && raycastHit.collider.gameObject.Equals(this.minimumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.x;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.right, this.minimumLimit.transform.localScale.x, vector.x, false);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.right, this.maximumLimit.transform.localScale.x, vector.x, true);
				}
				if (flag && raycastHit.collider.gameObject.Equals(this.maximumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.x;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.right, this.minimumLimit.transform.localScale.x, vector.x, true);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.right, this.maximumLimit.transform.localScale.x, vector.x, false);
				}
				if (flag2 && raycastHit2.collider.gameObject.Equals(this.minimumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.y;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.up, this.minimumLimit.transform.localScale.y, vector.y, false);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.up, this.maximumLimit.transform.localScale.y, vector.y, true);
				}
				if (flag2 && raycastHit2.collider.gameObject.Equals(this.maximumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.y;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.up, this.minimumLimit.transform.localScale.y, vector.y, true);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.up, this.maximumLimit.transform.localScale.y, vector.y, false);
				}
				if (flag3 && raycastHit3.collider.gameObject.Equals(this.minimumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.z;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.forward, this.minimumLimit.transform.localScale.y, vector.y, false);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.forward, this.maximumLimit.transform.localScale.y, vector.y, true);
				}
				if (flag3 && raycastHit3.collider.gameObject.Equals(this.maximumLimit.gameObject))
				{
					this.finalDirection = VRTK_Control.Direction.z;
					this.minimumLimitDiff = this.CalculateDiff(this.minimumLimit.transform.localPosition, Vector3.forward, this.minimumLimit.transform.localScale.z, vector.z, true);
					this.maximumLimitDiff = this.CalculateDiff(this.maximumLimit.transform.localPosition, Vector3.forward, this.maximumLimit.transform.localScale.z, vector.z, false);
				}
			}
			return true;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00070F58 File Offset: 0x0006F158
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.minimumValue,
				controlMax = this.maximumValue
			};
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00070F88 File Offset: 0x0006F188
		protected override void HandleUpdate()
		{
			this.CalculateValue();
			if (this.snapToStep)
			{
				this.SnapToValue();
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00070FA0 File Offset: 0x0006F1A0
		protected virtual Vector3 CalculateDiff(Vector3 initialPosition, Vector3 givenDirection, float scaleValue, float diffMultiplier, bool addition)
		{
			Vector3 b = givenDirection * diffMultiplier;
			Vector3 b2 = givenDirection * (scaleValue / 2f);
			if (addition)
			{
				b2 = initialPosition + b2;
			}
			else
			{
				b2 = initialPosition - b2;
			}
			Vector3 vector = initialPosition - b2;
			if (addition)
			{
				vector -= b;
			}
			else
			{
				vector += b;
			}
			return vector;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00070FFC File Offset: 0x0006F1FC
		protected virtual void InitRigidbody()
		{
			this.sliderRigidbody = base.GetComponent<Rigidbody>();
			if (this.sliderRigidbody == null)
			{
				this.sliderRigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			this.sliderRigidbody.isKinematic = false;
			this.sliderRigidbody.useGravity = false;
			this.sliderRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			this.sliderRigidbody.drag = this.releasedFriction;
			if (this.connectedTo && this.connectedTo.GetComponent<Rigidbody>() == null)
			{
				Rigidbody rigidbody = this.connectedTo.AddComponent<Rigidbody>();
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000710A4 File Offset: 0x0006F2A4
		protected virtual void InitInteractableObject()
		{
			VRTK_InteractableObject vrtk_InteractableObject = base.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = base.gameObject.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			vrtk_InteractableObject.grabAttachMechanicScript = base.gameObject.AddComponent<VRTK_TrackObjectGrabAttach>();
			vrtk_InteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.grabAttachMechanicScript.precisionGrab = true;
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0007110C File Offset: 0x0006F30C
		protected virtual void InitJoint()
		{
			this.sliderJoint = base.GetComponent<ConfigurableJoint>();
			if (this.sliderJoint == null)
			{
				this.sliderJoint = base.gameObject.AddComponent<ConfigurableJoint>();
			}
			this.sliderJoint.xMotion = ((this.finalDirection == VRTK_Control.Direction.x) ? ConfigurableJointMotion.Free : ConfigurableJointMotion.Locked);
			this.sliderJoint.yMotion = ((this.finalDirection == VRTK_Control.Direction.y) ? ConfigurableJointMotion.Free : ConfigurableJointMotion.Locked);
			this.sliderJoint.zMotion = ((this.finalDirection == VRTK_Control.Direction.z) ? ConfigurableJointMotion.Free : ConfigurableJointMotion.Locked);
			this.sliderJoint.angularXMotion = ConfigurableJointMotion.Locked;
			this.sliderJoint.angularYMotion = ConfigurableJointMotion.Locked;
			this.sliderJoint.angularZMotion = ConfigurableJointMotion.Locked;
			this.ToggleSpring(false);
			this.sliderJointCreated = true;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000711C0 File Offset: 0x0006F3C0
		protected virtual void CalculateValue()
		{
			Vector3 vector = this.minimumLimit.transform.localPosition - this.minimumLimitDiff;
			Vector3 vector2 = this.maximumLimit.transform.localPosition - this.maximumLimitDiff;
			float num = Vector3.Distance(vector, vector2);
			float num2 = Vector3.Distance(vector, base.transform.localPosition);
			float num3 = Mathf.Round((this.minimumValue + Mathf.Clamp01(num2 / num) * (this.maximumValue - this.minimumValue)) / this.stepSize) * this.stepSize;
			float num4 = num3 - this.minimumValue;
			float num5 = this.maximumValue - this.minimumValue;
			float d = num4 / num5;
			this.snapPosition = vector + (vector2 - vector) * d;
			this.value = num3;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00071290 File Offset: 0x0006F490
		protected virtual void ToggleSpring(bool state)
		{
			JointDrive jointDrive = default(JointDrive);
			jointDrive.positionSpring = (state ? 10000f : 0f);
			jointDrive.positionDamper = (state ? 10f : 0f);
			jointDrive.maximumForce = (state ? 100f : 0f);
			this.sliderJoint.xDrive = jointDrive;
			this.sliderJoint.yDrive = jointDrive;
			this.sliderJoint.zDrive = jointDrive;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0007130B File Offset: 0x0006F50B
		protected virtual void SnapToValue()
		{
			this.ToggleSpring(true);
			this.sliderJoint.targetPosition = this.snapPosition * -1f;
			this.sliderJoint.targetVelocity = Vector3.zero;
		}

		// Token: 0x04001174 RID: 4468
		[Tooltip("An optional game object to which the wheel will be connected. If the game object moves the wheel will follow along.")]
		public GameObject connectedTo;

		// Token: 0x04001175 RID: 4469
		[Tooltip("The axis on which the slider should move. All other axis will be frozen.")]
		public VRTK_Control.Direction direction;

		// Token: 0x04001176 RID: 4470
		[Tooltip("The collider to specify the minimum limit of the slider.")]
		public Collider minimumLimit;

		// Token: 0x04001177 RID: 4471
		[Tooltip("The collider to specify the maximum limit of the slider.")]
		public Collider maximumLimit;

		// Token: 0x04001178 RID: 4472
		[Tooltip("The minimum value of the slider.")]
		public float minimumValue;

		// Token: 0x04001179 RID: 4473
		[Tooltip("The maximum value of the slider.")]
		public float maximumValue = 100f;

		// Token: 0x0400117A RID: 4474
		[Tooltip("The increments in which slider values can change.")]
		public float stepSize = 0.1f;

		// Token: 0x0400117B RID: 4475
		[Tooltip("If this is checked then when the slider is released, it will snap to the nearest value position.")]
		public bool snapToStep;

		// Token: 0x0400117C RID: 4476
		[Tooltip("The amount of friction the slider will have when it is released.")]
		public float releasedFriction = 50f;

		// Token: 0x0400117D RID: 4477
		protected VRTK_Control.Direction finalDirection;

		// Token: 0x0400117E RID: 4478
		protected Rigidbody sliderRigidbody;

		// Token: 0x0400117F RID: 4479
		protected ConfigurableJoint sliderJoint;

		// Token: 0x04001180 RID: 4480
		protected bool sliderJointCreated;

		// Token: 0x04001181 RID: 4481
		protected Vector3 minimumLimitDiff;

		// Token: 0x04001182 RID: 4482
		protected Vector3 maximumLimitDiff;

		// Token: 0x04001183 RID: 4483
		protected Vector3 snapPosition;
	}
}
