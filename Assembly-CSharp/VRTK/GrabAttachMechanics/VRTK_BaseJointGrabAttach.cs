using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000344 RID: 836
	public abstract class VRTK_BaseJointGrabAttach : VRTK_BaseGrabAttach
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x0009606B File Offset: 0x0009426B
		public override bool ValidGrab(Rigidbody checkAttachPoint)
		{
			return this.controllerAttachJoint == null || this.controllerAttachJoint.connectedBody != checkAttachPoint;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0009608E File Offset: 0x0009428E
		public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
		{
			if (base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint))
			{
				this.SnapObjectToGrabToController(givenGrabbedObject);
				return true;
			}
			return false;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000960A5 File Offset: 0x000942A5
		public override void StopGrab(bool applyGrabbingObjectVelocity)
		{
			this.ReleaseObject(applyGrabbingObjectVelocity);
			base.StopGrab(applyGrabbingObjectVelocity);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000960B5 File Offset: 0x000942B5
		protected override void Initialise()
		{
			this.tracked = false;
			this.climbable = false;
			this.kinematic = false;
			this.controllerAttachJoint = null;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000960D3 File Offset: 0x000942D3
		protected override Rigidbody ReleaseFromController(bool applyGrabbingObjectVelocity)
		{
			if (this.controllerAttachJoint)
			{
				Rigidbody component = this.controllerAttachJoint.GetComponent<Rigidbody>();
				this.DestroyJoint(this.destroyImmediatelyOnThrow, applyGrabbingObjectVelocity);
				this.controllerAttachJoint = null;
				return component;
			}
			return null;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00096103 File Offset: 0x00094303
		protected virtual void OnJointBreak(float force)
		{
			this.ForceReleaseGrab();
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0009610C File Offset: 0x0009430C
		protected virtual void CreateJoint(GameObject obj)
		{
			if (this.precisionGrab)
			{
				this.givenJoint.anchor = obj.transform.InverseTransformPoint(this.controllerAttachPoint.position);
			}
			this.controllerAttachJoint = this.givenJoint;
			this.controllerAttachJoint.breakForce = ((!this.grabbedObjectScript.IsDroppable() || this.grabbedObjectScript.validDrop == VRTK_InteractableObject.ValidDropTypes.DropValidSnapDropZone) ? float.PositiveInfinity : this.controllerAttachJoint.breakForce);
			this.controllerAttachJoint.connectedBody = this.controllerAttachPoint;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00096197 File Offset: 0x00094397
		protected virtual void DestroyJoint(bool withDestroyImmediate, bool applyGrabbingObjectVelocity)
		{
			this.controllerAttachJoint.connectedBody = null;
			if (withDestroyImmediate && applyGrabbingObjectVelocity)
			{
				Object.DestroyImmediate(this.controllerAttachJoint);
				return;
			}
			Object.Destroy(this.controllerAttachJoint);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000961C4 File Offset: 0x000943C4
		protected virtual void SetSnappedObjectPosition(GameObject obj)
		{
			if (this.grabbedSnapHandle == null)
			{
				obj.transform.position = this.controllerAttachPoint.transform.position;
				return;
			}
			obj.transform.rotation = this.controllerAttachPoint.transform.rotation * Quaternion.Euler(this.grabbedSnapHandle.transform.localEulerAngles);
			obj.transform.position = this.controllerAttachPoint.transform.position - (this.grabbedSnapHandle.transform.position - obj.transform.position);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00096270 File Offset: 0x00094470
		protected virtual void SnapObjectToGrabToController(GameObject obj)
		{
			if (!this.precisionGrab)
			{
				this.SetSnappedObjectPosition(obj);
			}
			this.CreateJoint(obj);
		}

		// Token: 0x04001734 RID: 5940
		[Header("Joint Options", order = 2)]
		[Tooltip("Determines whether the joint should be destroyed immediately on release or whether to wait till the end of the frame before being destroyed.")]
		public bool destroyImmediatelyOnThrow = true;

		// Token: 0x04001735 RID: 5941
		protected Joint givenJoint;

		// Token: 0x04001736 RID: 5942
		protected Joint controllerAttachJoint;
	}
}
