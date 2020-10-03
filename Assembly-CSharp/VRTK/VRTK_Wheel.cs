using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x02000295 RID: 661
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Wheel")]
	public class VRTK_Wheel : VRTK_Control
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x000714DC File Offset: 0x0006F6DC
		protected override void InitRequiredComponents()
		{
			this.initialLocalRotation = base.transform.localRotation;
			this.InitWheel();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000714F8 File Offset: 0x0006F6F8
		protected override bool DetectSetup()
		{
			if (this.wheelHingeCreated)
			{
				this.wheelHinge.anchor = Vector3.up;
				this.wheelHinge.axis = Vector3.up;
				if (this.connectedTo)
				{
					this.wheelHinge.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
				}
			}
			return true;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00071554 File Offset: 0x0006F754
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.minimumValue,
				controlMax = this.maximumValue
			};
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00071584 File Offset: 0x0006F784
		protected override void HandleUpdate()
		{
			this.CalculateValue();
			if (this.lockAtLimits && !this.initialValueCalculated)
			{
				base.transform.localRotation = this.initialLocalRotation;
				this.initialValueCalculated = true;
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000715B4 File Offset: 0x0006F7B4
		protected virtual void InitWheel()
		{
			this.SetupRigidbody();
			this.SetupHinge();
			this.SetupInteractableObject();
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000715C8 File Offset: 0x0006F7C8
		protected virtual void SetupRigidbody()
		{
			this.wheelRigidbody = base.GetComponent<Rigidbody>();
			if (this.wheelRigidbody == null)
			{
				this.wheelRigidbody = base.gameObject.AddComponent<Rigidbody>();
				this.wheelRigidbody.angularDrag = this.releasedFriction;
			}
			this.wheelRigidbody.isKinematic = false;
			this.wheelRigidbody.useGravity = false;
			if (this.connectedTo && this.connectedTo.GetComponent<Rigidbody>() == null)
			{
				Rigidbody rigidbody = this.connectedTo.AddComponent<Rigidbody>();
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00071661 File Offset: 0x0006F861
		protected virtual void SetupHinge()
		{
			this.wheelHinge = base.GetComponent<HingeJoint>();
			if (this.wheelHinge == null)
			{
				this.wheelHinge = base.gameObject.AddComponent<HingeJoint>();
				this.wheelHingeCreated = true;
			}
			this.SetupHingeRestrictions();
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0007169C File Offset: 0x0006F89C
		protected virtual void SetupHingeRestrictions()
		{
			float num = 0f;
			float max = this.maxAngle;
			float num2 = this.maxAngle - 180f;
			if (num2 > 0f)
			{
				num -= num2;
				max = 180f;
			}
			if (this.lockAtLimits)
			{
				this.wheelHinge.useLimits = true;
				JointLimits limits = default(JointLimits);
				limits.min = num;
				limits.max = max;
				this.wheelHinge.limits = limits;
				Vector3 localEulerAngles = base.transform.localEulerAngles;
				int num3 = Mathf.RoundToInt(this.initialLocalRotation.eulerAngles.z);
				if (num3 != 0)
				{
					if (num3 != 90)
					{
						if (num3 == 180)
						{
							localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y + num, base.transform.localEulerAngles.z);
						}
					}
					else
					{
						localEulerAngles = new Vector3(base.transform.localEulerAngles.x + num, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
					}
				}
				else
				{
					localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y - num, base.transform.localEulerAngles.z);
				}
				base.transform.localEulerAngles = localEulerAngles;
				this.initialValueCalculated = false;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00071810 File Offset: 0x0006FA10
		protected virtual void ConfigureHingeSpring()
		{
			JointSpring spring = default(JointSpring);
			spring.spring = this.springStrengthValue;
			spring.damper = this.springDamperValue;
			spring.targetPosition = this.springAngle + this.wheelHinge.limits.min;
			this.wheelHinge.spring = spring;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0007186C File Offset: 0x0006FA6C
		protected virtual void SetupInteractableObject()
		{
			VRTK_InteractableObject vrtk_InteractableObject = base.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = base.gameObject.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			VRTK_TrackObjectGrabAttach vrtk_TrackObjectGrabAttach;
			if (this.grabType == VRTK_Wheel.GrabTypes.TrackObject)
			{
				vrtk_TrackObjectGrabAttach = base.gameObject.AddComponent<VRTK_TrackObjectGrabAttach>();
				if (this.lockAtLimits)
				{
					vrtk_TrackObjectGrabAttach.velocityLimit = 0f;
					vrtk_TrackObjectGrabAttach.angularVelocityLimit = this.angularVelocityLimit;
				}
			}
			else
			{
				vrtk_TrackObjectGrabAttach = base.gameObject.AddComponent<VRTK_RotatorTrackGrabAttach>();
			}
			vrtk_TrackObjectGrabAttach.precisionGrab = true;
			vrtk_TrackObjectGrabAttach.detachDistance = this.detatchDistance;
			vrtk_InteractableObject.grabAttachMechanicScript = vrtk_TrackObjectGrabAttach;
			vrtk_InteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
			vrtk_InteractableObject.InteractableObjectGrabbed += this.WheelInteractableObjectGrabbed;
			vrtk_InteractableObject.InteractableObjectUngrabbed += this.WheelInteractableObjectUngrabbed;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00071935 File Offset: 0x0006FB35
		protected virtual void WheelInteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.wheelRigidbody.angularDrag = this.grabbedFriction;
			this.wheelHinge.useSpring = false;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00071954 File Offset: 0x0006FB54
		protected virtual void WheelInteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.wheelRigidbody.angularDrag = this.releasedFriction;
			if (this.snapToStep)
			{
				this.wheelHinge.useSpring = true;
				this.ConfigureHingeSpring();
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00071984 File Offset: 0x0006FB84
		protected virtual void CalculateValue()
		{
			VRTK_Control.ControlValueRange controlValueRange = this.RegisterValueRange();
			float num;
			Vector3 vector;
			(base.transform.localRotation * Quaternion.Inverse(this.initialLocalRotation)).ToAngleAxis(out num, out vector);
			float num2 = Mathf.Round((controlValueRange.controlMin + Mathf.Clamp01(num / this.maxAngle) * (controlValueRange.controlMax - controlValueRange.controlMin)) / this.stepSize) * this.stepSize;
			float num3 = num2 - controlValueRange.controlMin;
			float num4 = controlValueRange.controlMax - controlValueRange.controlMin;
			this.springAngle = num3 / num4 * this.maxAngle;
			this.value = num2;
		}

		// Token: 0x0400118A RID: 4490
		[Tooltip("An optional game object to which the wheel will be connected. If the game object moves the wheel will follow along.")]
		public GameObject connectedTo;

		// Token: 0x0400118B RID: 4491
		[Tooltip("The grab attach mechanic to use. Track Object allows for rotations of the controller, Rotator Track allows for grabbing the wheel and spinning it.")]
		public VRTK_Wheel.GrabTypes grabType;

		// Token: 0x0400118C RID: 4492
		[Tooltip("The maximum distance the grabbing controller is away from the wheel before it is automatically released.")]
		public float detatchDistance = 0.5f;

		// Token: 0x0400118D RID: 4493
		[Tooltip("The minimum value the wheel can be set to.")]
		public float minimumValue;

		// Token: 0x0400118E RID: 4494
		[Tooltip("The maximum value the wheel can be set to.")]
		public float maximumValue = 10f;

		// Token: 0x0400118F RID: 4495
		[Tooltip("The increments in which values can change.")]
		public float stepSize = 1f;

		// Token: 0x04001190 RID: 4496
		[Tooltip("If this is checked then when the wheel is released, it will snap to the step rotation.")]
		public bool snapToStep;

		// Token: 0x04001191 RID: 4497
		[Tooltip("The amount of friction the wheel will have when it is grabbed.")]
		public float grabbedFriction = 25f;

		// Token: 0x04001192 RID: 4498
		[Tooltip("The amount of friction the wheel will have when it is released.")]
		public float releasedFriction = 10f;

		// Token: 0x04001193 RID: 4499
		[Range(0f, 359f)]
		[Tooltip("The maximum angle the wheel has to be turned to reach it's maximum value.")]
		public float maxAngle = 359f;

		// Token: 0x04001194 RID: 4500
		[Tooltip("If this is checked then the wheel cannot be turned beyond the minimum and maximum value.")]
		public bool lockAtLimits;

		// Token: 0x04001195 RID: 4501
		protected float angularVelocityLimit = 150f;

		// Token: 0x04001196 RID: 4502
		protected float springStrengthValue = 150f;

		// Token: 0x04001197 RID: 4503
		protected float springDamperValue = 5f;

		// Token: 0x04001198 RID: 4504
		protected Quaternion initialLocalRotation;

		// Token: 0x04001199 RID: 4505
		protected Rigidbody wheelRigidbody;

		// Token: 0x0400119A RID: 4506
		protected HingeJoint wheelHinge;

		// Token: 0x0400119B RID: 4507
		protected bool wheelHingeCreated;

		// Token: 0x0400119C RID: 4508
		protected bool initialValueCalculated;

		// Token: 0x0400119D RID: 4509
		protected float springAngle;

		// Token: 0x020005CE RID: 1486
		public enum GrabTypes
		{
			// Token: 0x0400275B RID: 10075
			TrackObject,
			// Token: 0x0400275C RID: 10076
			RotatorTrack
		}
	}
}
