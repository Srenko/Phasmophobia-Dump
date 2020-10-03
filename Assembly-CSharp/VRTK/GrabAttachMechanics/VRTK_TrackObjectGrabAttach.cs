using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x0200034B RID: 843
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_TrackObjectGrabAttach")]
	public class VRTK_TrackObjectGrabAttach : VRTK_BaseGrabAttach
	{
		// Token: 0x06001D72 RID: 7538 RVA: 0x0009665F File Offset: 0x0009485F
		public override void StopGrab(bool applyGrabbingObjectVelocity)
		{
			if (this.isReleasable)
			{
				this.ReleaseObject(applyGrabbingObjectVelocity);
			}
			base.StopGrab(applyGrabbingObjectVelocity);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00096678 File Offset: 0x00094878
		public override Transform CreateTrackPoint(Transform controllerPoint, GameObject currentGrabbedObject, GameObject currentGrabbingObject, ref bool customTrackPoint)
		{
			Transform transform = null;
			if (this.precisionGrab)
			{
				transform = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					currentGrabbedObject.name,
					"TrackObject",
					"PrecisionSnap",
					"AttachPoint"
				})).transform;
				transform.parent = currentGrabbingObject.transform;
				this.SetTrackPointOrientation(ref transform, currentGrabbedObject.transform, controllerPoint);
				customTrackPoint = true;
			}
			else
			{
				transform = base.CreateTrackPoint(controllerPoint, currentGrabbedObject, currentGrabbingObject, ref customTrackPoint);
			}
			return transform;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000966F8 File Offset: 0x000948F8
		public override void ProcessUpdate()
		{
			if (this.trackPoint && this.grabbedObjectScript.IsDroppable() && Vector3.Distance(this.trackPoint.position, this.initialAttachPoint.position) > this.detachDistance)
			{
				this.ForceReleaseGrab();
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00096748 File Offset: 0x00094948
		public override void ProcessFixedUpdate()
		{
			if (!this.grabbedObject)
			{
				return;
			}
			float maxDistanceDelta = 10f;
			Vector3 a = this.trackPoint.position - ((this.grabbedSnapHandle != null) ? this.grabbedSnapHandle.position : this.grabbedObject.transform.position);
			float num;
			Vector3 a2;
			(this.trackPoint.rotation * Quaternion.Inverse((this.grabbedSnapHandle != null) ? this.grabbedSnapHandle.rotation : this.grabbedObject.transform.rotation)).ToAngleAxis(out num, out a2);
			num = ((num > 180f) ? (num -= 360f) : num);
			if (num != 0f)
			{
				Vector3 target = num * a2;
				Vector3 angularVelocity = Vector3.MoveTowards(this.grabbedObjectRigidBody.angularVelocity, target, maxDistanceDelta);
				if (this.angularVelocityLimit == float.PositiveInfinity || angularVelocity.sqrMagnitude < this.angularVelocityLimit)
				{
					this.grabbedObjectRigidBody.angularVelocity = angularVelocity;
				}
			}
			Vector3 target2 = a / Time.fixedDeltaTime;
			Vector3 velocity = Vector3.MoveTowards(this.grabbedObjectRigidBody.velocity, target2, maxDistanceDelta);
			if (this.velocityLimit == float.PositiveInfinity || velocity.sqrMagnitude < this.velocityLimit)
			{
				this.grabbedObjectRigidBody.velocity = velocity;
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0009689E File Offset: 0x00094A9E
		protected override void Initialise()
		{
			this.tracked = true;
			this.climbable = false;
			this.kinematic = false;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000968B5 File Offset: 0x00094AB5
		protected virtual void SetTrackPointOrientation(ref Transform trackPoint, Transform currentGrabbedObject, Transform controllerPoint)
		{
			trackPoint.position = currentGrabbedObject.position;
			trackPoint.rotation = currentGrabbedObject.rotation;
		}

		// Token: 0x0400173E RID: 5950
		[Header("Track Options", order = 2)]
		[Tooltip("The maximum distance the grabbing controller is away from the object before it is automatically dropped.")]
		public float detachDistance = 1f;

		// Token: 0x0400173F RID: 5951
		[Tooltip("The maximum amount of velocity magnitude that can be applied to the object. Lowering this can prevent physics glitches if objects are moving too fast.")]
		public float velocityLimit = float.PositiveInfinity;

		// Token: 0x04001740 RID: 5952
		[Tooltip("The maximum amount of angular velocity magnitude that can be applied to the object. Lowering this can prevent physics glitches if objects are moving too fast.")]
		public float angularVelocityLimit = float.PositiveInfinity;

		// Token: 0x04001741 RID: 5953
		protected bool isReleasable = true;
	}
}
