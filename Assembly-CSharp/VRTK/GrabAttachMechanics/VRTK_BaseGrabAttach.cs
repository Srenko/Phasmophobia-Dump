using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000343 RID: 835
	public abstract class VRTK_BaseGrabAttach : MonoBehaviour
	{
		// Token: 0x06001D3D RID: 7485 RVA: 0x00095D06 File Offset: 0x00093F06
		public virtual bool IsTracked()
		{
			return this.tracked;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00095D0E File Offset: 0x00093F0E
		public virtual bool IsClimbable()
		{
			return this.climbable;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00095D16 File Offset: 0x00093F16
		public virtual bool IsKinematic()
		{
			return this.kinematic;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000694B6 File Offset: 0x000676B6
		public virtual bool ValidGrab(Rigidbody checkAttachPoint)
		{
			return true;
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00095D1E File Offset: 0x00093F1E
		public virtual void SetTrackPoint(Transform givenTrackPoint)
		{
			this.trackPoint = givenTrackPoint;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00095D27 File Offset: 0x00093F27
		public virtual void SetInitialAttachPoint(Transform givenInitialAttachPoint)
		{
			this.initialAttachPoint = givenInitialAttachPoint;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00095D30 File Offset: 0x00093F30
		public virtual bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
		{
			this.grabbedObject = givenGrabbedObject;
			if (this.grabbedObject == null)
			{
				return false;
			}
			this.grabbedObjectScript = this.grabbedObject.GetComponent<VRTK_InteractableObject>();
			this.grabbedObjectRigidBody = this.grabbedObject.GetComponent<Rigidbody>();
			this.controllerAttachPoint = givenControllerAttachPoint;
			this.grabbedSnapHandle = this.GetSnapHandle(grabbingObject);
			this.grabbedObjectScript.PauseCollisions(this.onGrabCollisionDelay);
			return true;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00095D9C File Offset: 0x00093F9C
		public virtual void StopGrab(bool applyGrabbingObjectVelocity)
		{
			this.grabbedObject = null;
			this.grabbedObjectScript = null;
			this.trackPoint = null;
			this.grabbedSnapHandle = null;
			this.initialAttachPoint = null;
			this.controllerAttachPoint = null;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00095DC8 File Offset: 0x00093FC8
		public virtual Transform CreateTrackPoint(Transform controllerPoint, GameObject currentGrabbedObject, GameObject currentGrabbingObject, ref bool customTrackPoint)
		{
			customTrackPoint = false;
			return controllerPoint;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void ProcessUpdate()
		{
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void ProcessFixedUpdate()
		{
		}

		// Token: 0x06001D48 RID: 7496
		protected abstract void Initialise();

		// Token: 0x06001D49 RID: 7497 RVA: 0x00095DCF File Offset: 0x00093FCF
		protected virtual Rigidbody ReleaseFromController(bool applyGrabbingObjectVelocity)
		{
			return this.grabbedObjectRigidBody;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00095DD8 File Offset: 0x00093FD8
		protected virtual void ForceReleaseGrab()
		{
			if (this.grabbedObjectScript)
			{
				GameObject grabbingObject = this.grabbedObjectScript.GetGrabbingObject();
				if (grabbingObject != null)
				{
					VRTK_InteractGrab component = grabbingObject.GetComponent<VRTK_InteractGrab>();
					if (component != null)
					{
						component.ForceRelease(false);
					}
				}
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00095E20 File Offset: 0x00094020
		protected virtual void ReleaseObject(bool applyGrabbingObjectVelocity)
		{
			Rigidbody rigidbody = this.ReleaseFromController(applyGrabbingObjectVelocity);
			if (rigidbody != null && applyGrabbingObjectVelocity)
			{
				this.ThrowReleasedObject(rigidbody);
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00095E47 File Offset: 0x00094047
		protected virtual void FlipSnapHandles()
		{
			this.FlipSnapHandle(this.rightSnapHandle);
			this.FlipSnapHandle(this.leftSnapHandle);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00095E61 File Offset: 0x00094061
		protected virtual void Awake()
		{
			this.Initialise();
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00095E6C File Offset: 0x0009406C
		protected virtual void ThrowReleasedObject(Rigidbody objectRigidbody)
		{
			if (this.grabbedObjectScript != null)
			{
				VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(this.grabbedObjectScript.GetGrabbingObject());
				if (VRTK_ControllerReference.IsValid(controllerReference) && controllerReference.scriptAlias != null)
				{
					VRTK_InteractGrab component = controllerReference.scriptAlias.GetComponent<VRTK_InteractGrab>();
					if (component != null)
					{
						Transform controllerOrigin = VRTK_DeviceFinder.GetControllerOrigin(controllerReference);
						Vector3 controllerVelocity = VRTK_DeviceFinder.GetControllerVelocity(controllerReference);
						Vector3 controllerAngularVelocity = VRTK_DeviceFinder.GetControllerAngularVelocity(controllerReference);
						float num = component.throwMultiplier;
						if (controllerOrigin != null)
						{
							objectRigidbody.velocity = controllerOrigin.TransformVector(controllerVelocity) * (num * this.throwMultiplier);
							objectRigidbody.angularVelocity = controllerOrigin.TransformDirection(controllerAngularVelocity);
						}
						else
						{
							objectRigidbody.velocity = controllerVelocity * (num * this.throwMultiplier);
							objectRigidbody.angularVelocity = controllerAngularVelocity;
						}
						if (this.throwVelocityWithAttachDistance)
						{
							Collider componentInChildren = objectRigidbody.GetComponentInChildren<Collider>();
							if (componentInChildren != null)
							{
								Vector3 center = componentInChildren.bounds.center;
								objectRigidbody.velocity = objectRigidbody.GetPointVelocity(center + (center - base.transform.position));
								return;
							}
							objectRigidbody.velocity = objectRigidbody.GetPointVelocity(objectRigidbody.position + (objectRigidbody.position - base.transform.position));
						}
					}
				}
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00095FC0 File Offset: 0x000941C0
		protected virtual Transform GetSnapHandle(GameObject grabbingObject)
		{
			if (this.rightSnapHandle == null && this.leftSnapHandle != null)
			{
				this.rightSnapHandle = this.leftSnapHandle;
			}
			if (this.leftSnapHandle == null && this.rightSnapHandle != null)
			{
				this.leftSnapHandle = this.rightSnapHandle;
			}
			if (VRTK_DeviceFinder.IsControllerRightHand(grabbingObject))
			{
				return this.rightSnapHandle;
			}
			if (VRTK_DeviceFinder.IsControllerLeftHand(grabbingObject))
			{
				return this.leftSnapHandle;
			}
			return null;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0009603C File Offset: 0x0009423C
		protected virtual void FlipSnapHandle(Transform snapHandle)
		{
			if (snapHandle != null)
			{
				snapHandle.localRotation = Quaternion.Inverse(snapHandle.localRotation);
			}
		}

		// Token: 0x04001724 RID: 5924
		[Header("Base Options", order = 1)]
		[Tooltip("If this is checked then when the controller grabs the object, it will grab it with precision and pick it up at the particular point on the object the controller is touching.")]
		public bool precisionGrab;

		// Token: 0x04001725 RID: 5925
		[Tooltip("A Transform provided as an empty game object which must be the child of the item being grabbed and serves as an orientation point to rotate and position the grabbed item in relation to the right handed controller. If no Right Snap Handle is provided but a Left Snap Handle is provided, then the Left Snap Handle will be used in place. If no Snap Handle is provided then the object will be grabbed at its central point. Not required for `Precision Snap`.")]
		public Transform rightSnapHandle;

		// Token: 0x04001726 RID: 5926
		[Tooltip("A Transform provided as an empty game object which must be the child of the item being grabbed and serves as an orientation point to rotate and position the grabbed item in relation to the left handed controller. If no Left Snap Handle is provided but a Right Snap Handle is provided, then the Right Snap Handle will be used in place. If no Snap Handle is provided then the object will be grabbed at its central point. Not required for `Precision Snap`.")]
		public Transform leftSnapHandle;

		// Token: 0x04001727 RID: 5927
		[Tooltip("If checked then when the object is thrown, the distance between the object's attach point and the controller's attach point will be used to calculate a faster throwing velocity.")]
		public bool throwVelocityWithAttachDistance;

		// Token: 0x04001728 RID: 5928
		[Tooltip("An amount to multiply the velocity of the given object when it is thrown. This can also be used in conjunction with the Interact Grab Throw Multiplier to have certain objects be thrown even further than normal (or thrown a shorter distance if a number below 1 is entered).")]
		public float throwMultiplier = 1f;

		// Token: 0x04001729 RID: 5929
		[Tooltip("The amount of time to delay collisions affecting the object when it is first grabbed. This is useful if a game object may get stuck inside another object when it is being grabbed.")]
		public float onGrabCollisionDelay;

		// Token: 0x0400172A RID: 5930
		protected bool tracked;

		// Token: 0x0400172B RID: 5931
		protected bool climbable;

		// Token: 0x0400172C RID: 5932
		protected bool kinematic;

		// Token: 0x0400172D RID: 5933
		protected GameObject grabbedObject;

		// Token: 0x0400172E RID: 5934
		protected Rigidbody grabbedObjectRigidBody;

		// Token: 0x0400172F RID: 5935
		protected VRTK_InteractableObject grabbedObjectScript;

		// Token: 0x04001730 RID: 5936
		protected Transform trackPoint;

		// Token: 0x04001731 RID: 5937
		protected Transform grabbedSnapHandle;

		// Token: 0x04001732 RID: 5938
		protected Transform initialAttachPoint;

		// Token: 0x04001733 RID: 5939
		protected Rigidbody controllerAttachPoint;
	}
}
