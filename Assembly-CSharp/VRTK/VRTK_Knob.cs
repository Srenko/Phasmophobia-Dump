using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x02000291 RID: 657
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Knob")]
	public class VRTK_Knob : VRTK_Control
	{
		// Token: 0x06001420 RID: 5152 RVA: 0x0006FCA0 File Offset: 0x0006DEA0
		protected override void InitRequiredComponents()
		{
			this.initialRotation = base.transform.rotation;
			this.initialLocalRotation = base.transform.localRotation.eulerAngles;
			this.InitKnob();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006FCE0 File Offset: 0x0006DEE0
		protected override bool DetectSetup()
		{
			this.finalDirection = this.direction;
			if (this.knobJointCreated)
			{
				this.knobJoint.angularXMotion = ConfigurableJointMotion.Locked;
				this.knobJoint.angularYMotion = ConfigurableJointMotion.Locked;
				this.knobJoint.angularZMotion = ConfigurableJointMotion.Locked;
				switch (this.finalDirection)
				{
				case VRTK_Knob.KnobDirection.x:
					this.knobJoint.angularXMotion = ConfigurableJointMotion.Free;
					break;
				case VRTK_Knob.KnobDirection.y:
					this.knobJoint.angularYMotion = ConfigurableJointMotion.Free;
					break;
				case VRTK_Knob.KnobDirection.z:
					this.knobJoint.angularZMotion = ConfigurableJointMotion.Free;
					break;
				}
			}
			if (this.knobJoint)
			{
				this.knobJoint.xMotion = ConfigurableJointMotion.Locked;
				this.knobJoint.yMotion = ConfigurableJointMotion.Locked;
				this.knobJoint.zMotion = ConfigurableJointMotion.Locked;
				if (this.connectedTo)
				{
					this.knobJoint.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
				}
			}
			return true;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.min,
				controlMax = this.max
			};
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
		protected override void HandleUpdate()
		{
			this.value = this.CalculateValue();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006FE00 File Offset: 0x0006E000
		protected virtual void InitKnob()
		{
			Rigidbody rigidbody = base.GetComponent<Rigidbody>();
			if (rigidbody == null)
			{
				rigidbody = base.gameObject.AddComponent<Rigidbody>();
				rigidbody.angularDrag = 10f;
			}
			rigidbody.isKinematic = false;
			rigidbody.useGravity = false;
			VRTK_InteractableObject vrtk_InteractableObject = base.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = base.gameObject.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			vrtk_InteractableObject.grabAttachMechanicScript = base.gameObject.AddComponent<VRTK_TrackObjectGrabAttach>();
			vrtk_InteractableObject.grabAttachMechanicScript.precisionGrab = true;
			vrtk_InteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
			this.knobJoint = base.GetComponent<ConfigurableJoint>();
			if (this.knobJoint == null)
			{
				this.knobJoint = base.gameObject.AddComponent<ConfigurableJoint>();
				this.knobJoint.configuredInWorldSpace = false;
				this.knobJointCreated = true;
			}
			if (this.connectedTo && this.connectedTo.GetComponent<Rigidbody>() == null)
			{
				Rigidbody rigidbody2 = this.connectedTo.AddComponent<Rigidbody>();
				rigidbody2.useGravity = false;
				rigidbody2.isKinematic = true;
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006FF10 File Offset: 0x0006E110
		protected virtual VRTK_Knob.KnobDirection DetectDirection()
		{
			VRTK_Knob.KnobDirection result = VRTK_Knob.KnobDirection.x;
			Bounds bounds = VRTK_SharedMethods.GetBounds(base.transform, null, null);
			RaycastHit raycastHit;
			Physics.Raycast(bounds.center, Vector3.forward, out raycastHit, bounds.extents.z * 3f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit2;
			Physics.Raycast(bounds.center, Vector3.back, out raycastHit2, bounds.extents.z * 3f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit3;
			Physics.Raycast(bounds.center, Vector3.left, out raycastHit3, bounds.extents.x * 3f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit4;
			Physics.Raycast(bounds.center, Vector3.right, out raycastHit4, bounds.extents.x * 3f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit5;
			Physics.Raycast(bounds.center, Vector3.up, out raycastHit5, bounds.extents.y * 3f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit6;
			Physics.Raycast(bounds.center, Vector3.down, out raycastHit6, bounds.extents.y * 3f, -5, QueryTriggerInteraction.UseGlobal);
			float num = (raycastHit4.collider != null) ? raycastHit4.distance : float.MaxValue;
			float num2 = (raycastHit6.collider != null) ? raycastHit6.distance : float.MaxValue;
			float num3 = (raycastHit2.collider != null) ? raycastHit2.distance : float.MaxValue;
			float num4 = (raycastHit3.collider != null) ? raycastHit3.distance : float.MaxValue;
			float num5 = (raycastHit5.collider != null) ? raycastHit5.distance : float.MaxValue;
			float num6 = (raycastHit.collider != null) ? raycastHit.distance : float.MaxValue;
			if (VRTK_SharedMethods.IsLowest(num, new float[]
			{
				num2,
				num3,
				num4,
				num5,
				num6
			}))
			{
				result = VRTK_Knob.KnobDirection.z;
			}
			else if (VRTK_SharedMethods.IsLowest(num2, new float[]
			{
				num,
				num3,
				num4,
				num5,
				num6
			}))
			{
				result = VRTK_Knob.KnobDirection.y;
			}
			else if (VRTK_SharedMethods.IsLowest(num3, new float[]
			{
				num,
				num2,
				num4,
				num5,
				num6
			}))
			{
				result = VRTK_Knob.KnobDirection.x;
			}
			else if (VRTK_SharedMethods.IsLowest(num4, new float[]
			{
				num,
				num2,
				num3,
				num5,
				num6
			}))
			{
				result = VRTK_Knob.KnobDirection.z;
			}
			else if (VRTK_SharedMethods.IsLowest(num5, new float[]
			{
				num,
				num2,
				num3,
				num4,
				num6
			}))
			{
				result = VRTK_Knob.KnobDirection.y;
			}
			else if (VRTK_SharedMethods.IsLowest(num6, new float[]
			{
				num,
				num2,
				num3,
				num4,
				num5
			}))
			{
				result = VRTK_Knob.KnobDirection.x;
			}
			return result;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000701F0 File Offset: 0x0006E3F0
		protected virtual float CalculateValue()
		{
			if (!this.subDirectionFound)
			{
				float num = Mathf.Abs(base.transform.localRotation.eulerAngles.x - this.initialLocalRotation.x) % 90f;
				float num2 = Mathf.Abs(base.transform.localRotation.eulerAngles.y - this.initialLocalRotation.y) % 90f;
				float num3 = Mathf.Abs(base.transform.localRotation.eulerAngles.z - this.initialLocalRotation.z) % 90f;
				num = ((Mathf.RoundToInt(num) >= 89) ? 0f : num);
				num2 = ((Mathf.RoundToInt(num2) >= 89) ? 0f : num2);
				num3 = ((Mathf.RoundToInt(num3) >= 89) ? 0f : num3);
				if (Mathf.RoundToInt(num) != 0 || Mathf.RoundToInt(num2) != 0 || Mathf.RoundToInt(num3) != 0)
				{
					this.subDirection = ((num < num2) ? ((num2 < num3) ? VRTK_Knob.KnobDirection.z : VRTK_Knob.KnobDirection.y) : ((num < num3) ? VRTK_Knob.KnobDirection.z : VRTK_Knob.KnobDirection.x));
					this.subDirectionFound = true;
				}
			}
			float num4 = 0f;
			switch (this.subDirection)
			{
			case VRTK_Knob.KnobDirection.x:
				num4 = base.transform.localRotation.eulerAngles.x - this.initialLocalRotation.x;
				break;
			case VRTK_Knob.KnobDirection.y:
				num4 = base.transform.localRotation.eulerAngles.y - this.initialLocalRotation.y;
				break;
			case VRTK_Knob.KnobDirection.z:
				num4 = base.transform.localRotation.eulerAngles.z - this.initialLocalRotation.z;
				break;
			}
			num4 = Mathf.Round(num4 * 1000f) / 1000f;
			float num5;
			if (num4 > 0f && num4 <= 180f)
			{
				num5 = 360f - Quaternion.Angle(this.initialRotation, base.transform.rotation);
			}
			else
			{
				num5 = Quaternion.Angle(this.initialRotation, base.transform.rotation);
			}
			num5 = Mathf.Round((this.min + Mathf.Clamp01(num5 / 360f) * (this.max - this.min)) / this.stepSize) * this.stepSize;
			if (this.min > this.max && num4 != 0f)
			{
				num5 = this.max + this.min - num5;
			}
			return num5;
		}

		// Token: 0x0400115D RID: 4445
		[Tooltip("An optional game object to which the knob will be connected. If the game object moves the knob will follow along.")]
		public GameObject connectedTo;

		// Token: 0x0400115E RID: 4446
		[Tooltip("The axis on which the knob should rotate. All other axis will be frozen.")]
		public VRTK_Knob.KnobDirection direction;

		// Token: 0x0400115F RID: 4447
		[Tooltip("The minimum value of the knob.")]
		public float min;

		// Token: 0x04001160 RID: 4448
		[Tooltip("The maximum value of the knob.")]
		public float max = 100f;

		// Token: 0x04001161 RID: 4449
		[Tooltip("The increments in which knob values can change.")]
		public float stepSize = 1f;

		// Token: 0x04001162 RID: 4450
		protected const float MAX_AUTODETECT_KNOB_WIDTH = 3f;

		// Token: 0x04001163 RID: 4451
		protected VRTK_Knob.KnobDirection finalDirection;

		// Token: 0x04001164 RID: 4452
		protected VRTK_Knob.KnobDirection subDirection;

		// Token: 0x04001165 RID: 4453
		protected bool subDirectionFound;

		// Token: 0x04001166 RID: 4454
		protected Quaternion initialRotation;

		// Token: 0x04001167 RID: 4455
		protected Vector3 initialLocalRotation;

		// Token: 0x04001168 RID: 4456
		protected ConfigurableJoint knobJoint;

		// Token: 0x04001169 RID: 4457
		protected bool knobJointCreated;

		// Token: 0x020005CC RID: 1484
		public enum KnobDirection
		{
			// Token: 0x04002753 RID: 10067
			x,
			// Token: 0x04002754 RID: 10068
			y,
			// Token: 0x04002755 RID: 10069
			z
		}
	}
}
