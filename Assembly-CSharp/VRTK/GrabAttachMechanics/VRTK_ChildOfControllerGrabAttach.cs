using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000345 RID: 837
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_ChildOfControllerGrabAttach")]
	public class VRTK_ChildOfControllerGrabAttach : VRTK_BaseGrabAttach
	{
		// Token: 0x06001D5D RID: 7517 RVA: 0x00096297 File Offset: 0x00094497
		public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
		{
			if (base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint))
			{
				this.SnapObjectToGrabToController(givenGrabbedObject);
				this.grabbedObjectScript.isKinematic = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000960A5 File Offset: 0x000942A5
		public override void StopGrab(bool applyGrabbingObjectVelocity)
		{
			this.ReleaseObject(applyGrabbingObjectVelocity);
			base.StopGrab(applyGrabbingObjectVelocity);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000962BA File Offset: 0x000944BA
		protected override void Initialise()
		{
			this.tracked = false;
			this.climbable = false;
			this.kinematic = true;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000962D4 File Offset: 0x000944D4
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

		// Token: 0x06001D61 RID: 7521 RVA: 0x00096380 File Offset: 0x00094580
		protected virtual void SnapObjectToGrabToController(GameObject obj)
		{
			if (!this.precisionGrab)
			{
				this.SetSnappedObjectPosition(obj);
			}
			obj.transform.SetParent(this.controllerAttachPoint.transform);
		}
	}
}
