using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000349 RID: 841
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_RotatorTrackGrabAttach")]
	public class VRTK_RotatorTrackGrabAttach : VRTK_TrackObjectGrabAttach
	{
		// Token: 0x06001D6C RID: 7532 RVA: 0x00096561 File Offset: 0x00094761
		public override void StopGrab(bool applyGrabbingObjectVelocity)
		{
			this.isReleasable = false;
			base.StopGrab(applyGrabbingObjectVelocity);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00096574 File Offset: 0x00094774
		public override void ProcessFixedUpdate()
		{
			Vector3 force = this.trackPoint.position - this.initialAttachPoint.position;
			this.grabbedObjectRigidBody.AddForceAtPosition(force, this.initialAttachPoint.position, ForceMode.VelocityChange);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000965B5 File Offset: 0x000947B5
		protected override void SetTrackPointOrientation(ref Transform trackPoint, Transform currentGrabbedObject, Transform controllerPoint)
		{
			trackPoint.position = controllerPoint.position;
			trackPoint.rotation = controllerPoint.rotation;
		}
	}
}
