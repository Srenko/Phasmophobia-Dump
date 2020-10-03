using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x02000292 RID: 658
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Lever")]
	public class VRTK_Lever : VRTK_Control
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x00070488 File Offset: 0x0006E688
		protected override void InitRequiredComponents()
		{
			if (base.GetComponentInChildren<Collider>() == null)
			{
				VRTK_SharedMethods.CreateColliders(base.gameObject);
			}
			this.InitRigidbody();
			this.InitInteractableObject();
			this.InitHingeJoint();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000704B8 File Offset: 0x0006E6B8
		protected override bool DetectSetup()
		{
			if (this.leverHingeJointCreated)
			{
				Bounds bounds = VRTK_SharedMethods.GetBounds(base.transform, base.transform, null);
				switch (this.direction)
				{
				case VRTK_Lever.LeverDirection.x:
					this.leverHingeJoint.anchor = ((bounds.extents.y > bounds.extents.z) ? new Vector3(0f, bounds.extents.y / base.transform.lossyScale.y, 0f) : new Vector3(0f, 0f, bounds.extents.z / base.transform.lossyScale.z));
					break;
				case VRTK_Lever.LeverDirection.y:
					this.leverHingeJoint.axis = new Vector3(0f, 1f, 0f);
					this.leverHingeJoint.anchor = ((bounds.extents.x > bounds.extents.z) ? new Vector3(bounds.extents.x / base.transform.lossyScale.x, 0f, 0f) : new Vector3(0f, 0f, bounds.extents.z / base.transform.lossyScale.z));
					break;
				case VRTK_Lever.LeverDirection.z:
					this.leverHingeJoint.axis = new Vector3(0f, 0f, 1f);
					this.leverHingeJoint.anchor = ((bounds.extents.y > bounds.extents.x) ? new Vector3(0f, bounds.extents.y / base.transform.lossyScale.y, 0f) : new Vector3(bounds.extents.x / base.transform.lossyScale.x, 0f));
					break;
				}
				this.leverHingeJoint.anchor *= -1f;
			}
			if (this.leverHingeJoint)
			{
				this.leverHingeJoint.useLimits = true;
				JointLimits limits = this.leverHingeJoint.limits;
				limits.min = this.minAngle;
				limits.max = this.maxAngle;
				this.leverHingeJoint.limits = limits;
				if (this.connectedTo)
				{
					this.leverHingeJoint.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
				}
			}
			return true;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0007074C File Offset: 0x0006E94C
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.minAngle,
				controlMax = this.maxAngle
			};
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0007077C File Offset: 0x0006E97C
		protected override void HandleUpdate()
		{
			this.value = this.CalculateValue();
			this.SnapToValue(this.value);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00070798 File Offset: 0x0006E998
		protected virtual void InitRigidbody()
		{
			this.leverRigidbody = base.GetComponent<Rigidbody>();
			if (this.leverRigidbody == null)
			{
				this.leverRigidbody = base.gameObject.AddComponent<Rigidbody>();
				this.leverRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				this.leverRigidbody.angularDrag = this.releasedFriction;
			}
			this.leverRigidbody.isKinematic = false;
			this.leverRigidbody.useGravity = false;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00070808 File Offset: 0x0006EA08
		protected virtual void InitInteractableObject()
		{
			VRTK_InteractableObject vrtk_InteractableObject = base.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = base.gameObject.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			vrtk_InteractableObject.grabAttachMechanicScript = base.gameObject.AddComponent<VRTK_RotatorTrackGrabAttach>();
			vrtk_InteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.grabAttachMechanicScript.precisionGrab = true;
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
			vrtk_InteractableObject.InteractableObjectGrabbed += this.InteractableObjectGrabbed;
			vrtk_InteractableObject.InteractableObjectUngrabbed += this.InteractableObjectUngrabbed;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00070893 File Offset: 0x0006EA93
		protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.leverRigidbody.angularDrag = this.grabbedFriction;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000708A6 File Offset: 0x0006EAA6
		protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.leverRigidbody.angularDrag = this.releasedFriction;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000708BC File Offset: 0x0006EABC
		protected virtual void InitHingeJoint()
		{
			this.leverHingeJoint = base.GetComponent<HingeJoint>();
			if (this.leverHingeJoint == null)
			{
				this.leverHingeJoint = base.gameObject.AddComponent<HingeJoint>();
				this.leverHingeJointCreated = true;
			}
			if (this.connectedTo)
			{
				Rigidbody rigidbody = this.connectedTo.GetComponent<Rigidbody>();
				if (rigidbody == null)
				{
					rigidbody = this.connectedTo.AddComponent<Rigidbody>();
				}
				rigidbody.useGravity = false;
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00070930 File Offset: 0x0006EB30
		protected virtual float CalculateValue()
		{
			return Mathf.Round(this.leverHingeJoint.angle / this.stepSize) * this.stepSize;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00070950 File Offset: 0x0006EB50
		protected virtual void SnapToValue(float value)
		{
			float num = (value - this.minAngle) / (this.maxAngle - this.minAngle) * (this.leverHingeJoint.limits.max - this.leverHingeJoint.limits.min);
			JointLimits limits = this.leverHingeJoint.limits;
			JointLimits limits2 = this.leverHingeJoint.limits;
			limits2.min = num;
			limits2.max = num;
			this.leverHingeJoint.limits = limits2;
			this.leverHingeJoint.limits = limits;
		}

		// Token: 0x0400116A RID: 4458
		[Tooltip("An optional game object to which the lever will be connected. If the game object moves the lever will follow along.")]
		public GameObject connectedTo;

		// Token: 0x0400116B RID: 4459
		[Tooltip("The axis on which the lever should rotate. All other axis will be frozen.")]
		public VRTK_Lever.LeverDirection direction = VRTK_Lever.LeverDirection.y;

		// Token: 0x0400116C RID: 4460
		[Tooltip("The minimum angle of the lever counted from its initial position.")]
		public float minAngle;

		// Token: 0x0400116D RID: 4461
		[Tooltip("The maximum angle of the lever counted from its initial position.")]
		public float maxAngle = 130f;

		// Token: 0x0400116E RID: 4462
		[Tooltip("The increments in which lever values can change.")]
		public float stepSize = 1f;

		// Token: 0x0400116F RID: 4463
		[Tooltip("The amount of friction the lever will have whilst swinging when it is not grabbed.")]
		public float releasedFriction = 30f;

		// Token: 0x04001170 RID: 4464
		[Tooltip("The amount of friction the lever will have whilst swinging when it is grabbed.")]
		public float grabbedFriction = 60f;

		// Token: 0x04001171 RID: 4465
		protected HingeJoint leverHingeJoint;

		// Token: 0x04001172 RID: 4466
		protected bool leverHingeJointCreated;

		// Token: 0x04001173 RID: 4467
		protected Rigidbody leverRigidbody;

		// Token: 0x020005CD RID: 1485
		public enum LeverDirection
		{
			// Token: 0x04002757 RID: 10071
			x,
			// Token: 0x04002758 RID: 10072
			y,
			// Token: 0x04002759 RID: 10073
			z
		}
	}
}
