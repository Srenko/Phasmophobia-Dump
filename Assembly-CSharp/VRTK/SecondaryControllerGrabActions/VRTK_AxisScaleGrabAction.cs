using System;
using UnityEngine;

namespace VRTK.SecondaryControllerGrabActions
{
	// Token: 0x0200033B RID: 827
	[AddComponentMenu("VRTK/Scripts/Interactions/Secondary Controller Grab Actions/VRTK_AxisScaleGrabAction")]
	public class VRTK_AxisScaleGrabAction : VRTK_BaseGrabAction
	{
		// Token: 0x06001CFC RID: 7420 RVA: 0x000949BC File Offset: 0x00092BBC
		public override void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
		{
			base.Initialise(currentGrabbdObject, currentPrimaryGrabbingObject, currentSecondaryGrabbingObject, primaryGrabPoint, secondaryGrabPoint);
			this.initialScale = currentGrabbdObject.transform.localScale;
			this.initalLength = (this.grabbedObject.transform.position - this.secondaryGrabbingObject.transform.position).magnitude;
			this.initialScaleFactor = currentGrabbdObject.transform.localScale.x / this.initalLength;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00094A37 File Offset: 0x00092C37
		public override void ProcessUpdate()
		{
			base.ProcessUpdate();
			this.CheckForceStopDistance(this.ungrabDistance);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00094A4B File Offset: 0x00092C4B
		public override void ProcessFixedUpdate()
		{
			base.ProcessFixedUpdate();
			if (this.initialised)
			{
				if (this.uniformScaling)
				{
					this.UniformScale();
					return;
				}
				this.NonUniformScale();
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00094A70 File Offset: 0x00092C70
		protected virtual void ApplyScale(Vector3 newScale)
		{
			Vector3 localScale = this.grabbedObject.transform.localScale;
			float num = this.lockXAxis ? localScale.x : newScale.x;
			float num2 = this.lockYAxis ? localScale.y : newScale.y;
			float num3 = this.lockZAxis ? localScale.z : newScale.z;
			if (num > 0f && num2 > 0f && num3 > 0f)
			{
				this.grabbedObject.transform.localScale = new Vector3(num, num2, num3);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00094B04 File Offset: 0x00092D04
		protected virtual void NonUniformScale()
		{
			Vector3 vector = this.grabbedObject.transform.rotation * this.grabbedObject.transform.position;
			Vector3 vector2 = this.grabbedObject.transform.rotation * this.secondaryInitialGrabPoint.position;
			Vector3 vector3 = this.grabbedObject.transform.rotation * this.secondaryGrabbingObject.transform.position;
			float x = this.CalculateAxisScale(vector.x, vector2.x, vector3.x);
			float y = this.CalculateAxisScale(vector.y, vector2.y, vector3.y);
			float z = this.CalculateAxisScale(vector.z, vector2.z, vector3.z);
			Vector3 newScale = new Vector3(x, y, z) + this.initialScale;
			this.ApplyScale(newScale);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00094BE8 File Offset: 0x00092DE8
		protected virtual void UniformScale()
		{
			float magnitude = (this.grabbedObject.transform.position - this.secondaryGrabbingObject.transform.position).magnitude;
			float num = this.initialScaleFactor * magnitude;
			Vector3 newScale = new Vector3(num, num, num);
			this.ApplyScale(newScale);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00094C40 File Offset: 0x00092E40
		protected virtual float CalculateAxisScale(float centerPosition, float initialPosition, float currentPosition)
		{
			float num = currentPosition - initialPosition;
			return (centerPosition < initialPosition) ? num : (-num);
		}

		// Token: 0x040016FB RID: 5883
		[Tooltip("The distance the secondary controller must move away from the original grab position before the secondary controller auto ungrabs the object.")]
		public float ungrabDistance = 1f;

		// Token: 0x040016FC RID: 5884
		[Tooltip("If checked the current X Axis of the object won't be scaled")]
		public bool lockXAxis;

		// Token: 0x040016FD RID: 5885
		[Tooltip("If checked the current Y Axis of the object won't be scaled")]
		public bool lockYAxis;

		// Token: 0x040016FE RID: 5886
		[Tooltip("If checked the current Z Axis of the object won't be scaled")]
		public bool lockZAxis;

		// Token: 0x040016FF RID: 5887
		[Tooltip("If checked all the axes will be scaled together (unless locked)")]
		public bool uniformScaling;

		// Token: 0x04001700 RID: 5888
		protected Vector3 initialScale;

		// Token: 0x04001701 RID: 5889
		protected float initalLength;

		// Token: 0x04001702 RID: 5890
		protected float initialScaleFactor;
	}
}
