using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002EE RID: 750
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_BodyPhysics")]
	public class VRTK_BodyPhysics : VRTK_DestinationMarker
	{
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600199A RID: 6554 RVA: 0x0008873C File Offset: 0x0008693C
		// (remove) Token: 0x0600199B RID: 6555 RVA: 0x00088774 File Offset: 0x00086974
		public event BodyPhysicsEventHandler StartFalling;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600199C RID: 6556 RVA: 0x000887AC File Offset: 0x000869AC
		// (remove) Token: 0x0600199D RID: 6557 RVA: 0x000887E4 File Offset: 0x000869E4
		public event BodyPhysicsEventHandler StopFalling;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x0600199E RID: 6558 RVA: 0x0008881C File Offset: 0x00086A1C
		// (remove) Token: 0x0600199F RID: 6559 RVA: 0x00088854 File Offset: 0x00086A54
		public event BodyPhysicsEventHandler StartMoving;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060019A0 RID: 6560 RVA: 0x0008888C File Offset: 0x00086A8C
		// (remove) Token: 0x060019A1 RID: 6561 RVA: 0x000888C4 File Offset: 0x00086AC4
		public event BodyPhysicsEventHandler StopMoving;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060019A2 RID: 6562 RVA: 0x000888FC File Offset: 0x00086AFC
		// (remove) Token: 0x060019A3 RID: 6563 RVA: 0x00088934 File Offset: 0x00086B34
		public event BodyPhysicsEventHandler StartColliding;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060019A4 RID: 6564 RVA: 0x0008896C File Offset: 0x00086B6C
		// (remove) Token: 0x060019A5 RID: 6565 RVA: 0x000889A4 File Offset: 0x00086BA4
		public event BodyPhysicsEventHandler StopColliding;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060019A6 RID: 6566 RVA: 0x000889DC File Offset: 0x00086BDC
		// (remove) Token: 0x060019A7 RID: 6567 RVA: 0x00088A14 File Offset: 0x00086C14
		public event BodyPhysicsEventHandler StartLeaning;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x060019A8 RID: 6568 RVA: 0x00088A4C File Offset: 0x00086C4C
		// (remove) Token: 0x060019A9 RID: 6569 RVA: 0x00088A84 File Offset: 0x00086C84
		public event BodyPhysicsEventHandler StopLeaning;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060019AA RID: 6570 RVA: 0x00088ABC File Offset: 0x00086CBC
		// (remove) Token: 0x060019AB RID: 6571 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public event BodyPhysicsEventHandler StartTouchingGround;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060019AC RID: 6572 RVA: 0x00088B2C File Offset: 0x00086D2C
		// (remove) Token: 0x060019AD RID: 6573 RVA: 0x00088B64 File Offset: 0x00086D64
		public event BodyPhysicsEventHandler StopTouchingGround;

		// Token: 0x060019AE RID: 6574 RVA: 0x00088B99 File Offset: 0x00086D99
		public virtual bool ArePhysicsEnabled()
		{
			return this.bodyRigidbody != null && !this.bodyRigidbody.isKinematic;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00088BBC File Offset: 0x00086DBC
		public virtual void ApplyBodyVelocity(Vector3 velocity, bool forcePhysicsOn = false, bool applyMomentum = false)
		{
			if (this.enableBodyCollisions && forcePhysicsOn)
			{
				this.TogglePhysics(true);
			}
			if (this.ArePhysicsEnabled())
			{
				Vector3 b = new Vector3(0f, this.gravityPush, 0f);
				this.bodyRigidbody.velocity = velocity + b;
				this.ApplyBodyMomentum(applyMomentum);
				this.StartFall(this.currentValidFloorObject);
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00088C1E File Offset: 0x00086E1E
		public virtual void ToggleOnGround(bool state)
		{
			this.onGround = state;
			if (this.onGround)
			{
				this.OnStartTouchingGround(this.SetBodyPhysicsEvent(this.currentValidFloorObject, null));
				return;
			}
			this.OnStopTouchingGround(this.SetBodyPhysicsEvent(null, null));
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00088C51 File Offset: 0x00086E51
		public virtual void TogglePreventSnapToFloor(bool state)
		{
			this.preventSnapToFloor = state;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00088C5A File Offset: 0x00086E5A
		public virtual void ForceSnapToFloor()
		{
			this.TogglePreventSnapToFloor(false);
			this.SnapToNearestFloor();
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00088C69 File Offset: 0x00086E69
		public virtual bool IsFalling()
		{
			return this.isFalling;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00088C71 File Offset: 0x00086E71
		public virtual bool IsMoving()
		{
			return this.isMoving;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00088C79 File Offset: 0x00086E79
		public virtual bool IsLeaning()
		{
			return this.isLeaning;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00088C81 File Offset: 0x00086E81
		public virtual bool OnGround()
		{
			return this.onGround;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00088C89 File Offset: 0x00086E89
		public virtual Vector3 GetVelocity()
		{
			if (!(this.bodyRigidbody != null))
			{
				return Vector3.zero;
			}
			return this.bodyRigidbody.velocity;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00088CAA File Offset: 0x00086EAA
		public virtual Vector3 GetAngularVelocity()
		{
			if (!(this.bodyRigidbody != null))
			{
				return Vector3.zero;
			}
			return this.bodyRigidbody.angularVelocity;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00088CCB File Offset: 0x00086ECB
		public virtual void ResetVelocities()
		{
			this.bodyRigidbody.velocity = Vector3.zero;
			this.bodyRigidbody.angularVelocity = Vector3.zero;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00088CED File Offset: 0x00086EED
		public virtual void ResetFalling()
		{
			this.StopFall();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00088CF5 File Offset: 0x00086EF5
		public virtual GameObject GetBodyColliderContainer()
		{
			return this.bodyColliderContainer;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00088CFD File Offset: 0x00086EFD
		public virtual GameObject GetFootColliderContainer()
		{
			return this.footColliderContainer;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00088D05 File Offset: 0x00086F05
		public virtual GameObject GetCurrentCollidingObject()
		{
			return this.currentCollidingObject;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00088D10 File Offset: 0x00086F10
		public virtual void ResetIgnoredCollisions()
		{
			for (int i = 0; i < this.ignoreCollisionsOnGameObjects.Count; i++)
			{
				if (this.ignoreCollisionsOnGameObjects[i] != null)
				{
					Collider[] componentsInChildren = this.ignoreCollisionsOnGameObjects[i].GetComponentsInChildren<Collider>();
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						this.ManagePhysicsCollider(componentsInChildren[j], false);
					}
				}
			}
			this.ignoreCollisionsOnGameObjects.Clear();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00088D7C File Offset: 0x00086F7C
		public virtual bool SweepCollision(Vector3 direction, float maxDistance)
		{
			Vector3 point = this.bodyCollider.transform.position + this.bodyCollider.center + Vector3.up * (this.bodyCollider.height * 0.5f - this.bodyCollider.radius);
			Vector3 point2 = this.bodyCollider.transform.position + this.bodyCollider.center - Vector3.up * (this.bodyCollider.height * 0.5f - this.bodyCollider.radius);
			RaycastHit raycastHit;
			return VRTK_CustomRaycast.CapsuleCast(this.customRaycast, point, point2, this.bodyCollider.radius, direction, maxDistance, out raycastHit, this.layersToIgnore, QueryTriggerInteraction.Ignore);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00088E48 File Offset: 0x00087048
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetupPlayArea();
			this.SetupHeadset();
			this.footColliderContainerNameCheck = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				"FootColliderContainer"
			});
			this.enableBodyCollisionsStartingValue = this.enableBodyCollisions;
			this.EnableDropToFloor();
			this.EnableBodyPhysics();
			this.SetupIgnoredCollisions();
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00088E9F File Offset: 0x0008709F
		protected override void OnDisable()
		{
			base.OnDisable();
			this.DisableDropToFloor();
			this.DisableBodyPhysics();
			this.ManageCollisionListeners(false);
			this.ResetIgnoredCollisions();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00088EC0 File Offset: 0x000870C0
		protected virtual void FixedUpdate()
		{
			this.CheckBodyCollisionsSetting();
			this.ManageFalling();
			this.CalculateVelocity();
			this.UpdateCollider();
			this.lastPlayAreaPosition = ((this.playArea != null) ? this.playArea.position : Vector3.zero);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00088F00 File Offset: 0x00087100
		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (this.CheckValidCollision(collision.gameObject))
			{
				this.CheckStepUpCollision(collision);
				this.currentCollidingObject = collision.gameObject;
				this.OnStartColliding(this.SetBodyPhysicsEvent(this.currentCollidingObject, collision.collider));
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00088F3B File Offset: 0x0008713B
		protected virtual void OnTriggerEnter(Collider collider)
		{
			if (this.CheckValidCollision(collider.gameObject))
			{
				this.currentCollidingObject = collider.gameObject;
				this.OnStartColliding(this.SetBodyPhysicsEvent(this.currentCollidingObject, collider));
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00088F6A File Offset: 0x0008716A
		protected virtual void OnCollisionExit(Collision collision)
		{
			if (this.CheckExistingCollision(collision.gameObject))
			{
				this.OnStopColliding(this.SetBodyPhysicsEvent(this.currentCollidingObject, collision.collider));
				this.currentCollidingObject = null;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00088F99 File Offset: 0x00087199
		protected virtual void OnTriggerExit(Collider collider)
		{
			if (this.CheckExistingCollision(collider.gameObject))
			{
				this.OnStopColliding(this.SetBodyPhysicsEvent(this.currentCollidingObject, collider));
				this.currentCollidingObject = null;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00088FC4 File Offset: 0x000871C4
		protected virtual void OnDrawGizmos()
		{
			if (this.drawDebugGizmo && this.headset != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(new Vector3(this.headset.position.x, this.headset.position.y - 0.3f, this.headset.position.z), 0.075f);
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(new Vector3(this.currentStandingPosition.x, this.headset.position.y - 0.3f, this.currentStandingPosition.y), 0.05f);
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00089081 File Offset: 0x00087281
		protected virtual bool CheckValidCollision(GameObject checkObject)
		{
			return !VRTK_PlayerObject.IsPlayerObject(checkObject, VRTK_PlayerObject.ObjectTypes.Null) && (!this.onGround || (this.currentValidFloorObject != null && !this.currentValidFloorObject.Equals(checkObject)));
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000890B7 File Offset: 0x000872B7
		protected virtual bool CheckExistingCollision(GameObject checkObject)
		{
			return this.currentCollidingObject != null && this.currentCollidingObject.Equals(checkObject);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000890D8 File Offset: 0x000872D8
		protected virtual void SetupPlayArea()
		{
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			if (this.playArea != null)
			{
				this.lastPlayAreaPosition = this.playArea.position;
				this.collisionTracker = this.playArea.GetComponent<VRTK_CollisionTracker>();
				if (this.collisionTracker == null)
				{
					this.collisionTracker = this.playArea.gameObject.AddComponent<VRTK_CollisionTracker>();
				}
				this.ManageCollisionListeners(true);
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0008914C File Offset: 0x0008734C
		protected virtual void SetupHeadset()
		{
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			if (this.headset != null)
			{
				this.currentStandingPosition = new Vector2(this.headset.position.x, this.headset.position.z);
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000891A0 File Offset: 0x000873A0
		protected virtual void ManageCollisionListeners(bool state)
		{
			if (this.collisionTracker != null)
			{
				if (state)
				{
					this.collisionTracker.CollisionEnter += this.CollisionTracker_CollisionEnter;
					this.collisionTracker.CollisionExit += this.CollisionTracker_CollisionExit;
					this.collisionTracker.TriggerEnter += this.CollisionTracker_TriggerEnter;
					this.collisionTracker.TriggerExit += this.CollisionTracker_TriggerExit;
					return;
				}
				this.collisionTracker.CollisionEnter -= this.CollisionTracker_CollisionEnter;
				this.collisionTracker.CollisionExit -= this.CollisionTracker_CollisionExit;
				this.collisionTracker.TriggerEnter -= this.CollisionTracker_TriggerEnter;
				this.collisionTracker.TriggerExit -= this.CollisionTracker_TriggerExit;
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00089282 File Offset: 0x00087482
		protected virtual void CollisionTracker_TriggerExit(object sender, CollisionTrackerEventArgs e)
		{
			this.OnTriggerExit(e.collider);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00089290 File Offset: 0x00087490
		protected virtual void CollisionTracker_TriggerEnter(object sender, CollisionTrackerEventArgs e)
		{
			this.OnTriggerEnter(e.collider);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0008929E File Offset: 0x0008749E
		protected virtual void CollisionTracker_CollisionExit(object sender, CollisionTrackerEventArgs e)
		{
			this.OnCollisionExit(e.collision);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000892AC File Offset: 0x000874AC
		protected virtual void CollisionTracker_CollisionEnter(object sender, CollisionTrackerEventArgs e)
		{
			this.OnCollisionEnter(e.collision);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000892BA File Offset: 0x000874BA
		protected virtual void OnStartFalling(BodyPhysicsEventArgs e)
		{
			if (this.StartFalling != null)
			{
				this.StartFalling(this, e);
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000892D1 File Offset: 0x000874D1
		protected virtual void OnStopFalling(BodyPhysicsEventArgs e)
		{
			if (this.StopFalling != null)
			{
				this.StopFalling(this, e);
			}
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000892E8 File Offset: 0x000874E8
		protected virtual void OnStartMoving(BodyPhysicsEventArgs e)
		{
			if (this.StartMoving != null)
			{
				this.StartMoving(this, e);
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000892FF File Offset: 0x000874FF
		protected virtual void OnStopMoving(BodyPhysicsEventArgs e)
		{
			if (this.StopMoving != null)
			{
				this.StopMoving(this, e);
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00089316 File Offset: 0x00087516
		protected virtual void OnStartColliding(BodyPhysicsEventArgs e)
		{
			if (this.StartColliding != null)
			{
				this.StartColliding(this, e);
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0008932D File Offset: 0x0008752D
		protected virtual void OnStopColliding(BodyPhysicsEventArgs e)
		{
			if (this.StopColliding != null)
			{
				this.StopColliding(this, e);
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00089344 File Offset: 0x00087544
		protected virtual void OnStartLeaning(BodyPhysicsEventArgs e)
		{
			if (this.StartLeaning != null)
			{
				this.StartLeaning(this, e);
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0008935B File Offset: 0x0008755B
		protected virtual void OnStopLeaning(BodyPhysicsEventArgs e)
		{
			if (this.StopLeaning != null)
			{
				this.StopLeaning(this, e);
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00089372 File Offset: 0x00087572
		protected virtual void OnStartTouchingGround(BodyPhysicsEventArgs e)
		{
			if (this.StartTouchingGround != null)
			{
				this.StartTouchingGround(this, e);
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00089389 File Offset: 0x00087589
		protected virtual void OnStopTouchingGround(BodyPhysicsEventArgs e)
		{
			if (this.StopTouchingGround != null)
			{
				this.StopTouchingGround(this, e);
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000893A0 File Offset: 0x000875A0
		protected virtual BodyPhysicsEventArgs SetBodyPhysicsEvent(GameObject target, Collider collider)
		{
			BodyPhysicsEventArgs result;
			result.target = target;
			result.collider = collider;
			return result;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000893BE File Offset: 0x000875BE
		protected virtual void CalculateVelocity()
		{
			this.playAreaVelocity = ((this.playArea != null) ? ((this.playArea.position - this.lastPlayAreaPosition) / Time.fixedDeltaTime) : Vector3.zero);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000893FC File Offset: 0x000875FC
		protected virtual void TogglePhysics(bool state)
		{
			if (this.bodyRigidbody != null)
			{
				this.bodyRigidbody.isKinematic = !state;
			}
			if (this.bodyCollider != null)
			{
				this.bodyCollider.isTrigger = !state;
			}
			if (this.footCollider != null)
			{
				this.footCollider.isTrigger = !state;
			}
			this.currentBodyCollisionsSetting = state;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00089467 File Offset: 0x00087667
		protected virtual void ManageFalling()
		{
			if (!this.isFalling)
			{
				this.CheckHeadsetMovement();
				this.SnapToNearestFloor();
				return;
			}
			this.CheckFalling();
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00089484 File Offset: 0x00087684
		protected virtual void CheckBodyCollisionsSetting()
		{
			if (this.enableBodyCollisions != this.currentBodyCollisionsSetting)
			{
				this.TogglePhysics(this.enableBodyCollisions);
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000894A0 File Offset: 0x000876A0
		protected virtual void CheckFalling()
		{
			if (this.isFalling && this.fallMinTime < Time.time && VRTK_SharedMethods.RoundFloat(this.lastPlayAreaPosition.y, this.fallCheckPrecision, false) == VRTK_SharedMethods.RoundFloat(this.playArea.position.y, this.fallCheckPrecision, false))
			{
				this.StopFall();
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00089500 File Offset: 0x00087700
		protected virtual void SetCurrentStandingPosition()
		{
			if (this.playArea != null && !this.playArea.transform.position.Equals(this.lastPlayAreaPosition))
			{
				Vector3 vector = this.playArea.transform.position - this.lastPlayAreaPosition;
				this.currentStandingPosition = new Vector2(this.currentStandingPosition.x + vector.x, this.currentStandingPosition.y + vector.z);
			}
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00089588 File Offset: 0x00087788
		protected virtual void SetIsMoving(Vector2 currentHeadsetPosition)
		{
			float num = Vector2.Distance(currentHeadsetPosition, this.currentStandingPosition);
			float num2 = (this.playArea != null) ? Vector3.Distance(this.playArea.transform.position, this.lastPlayAreaPosition) : 0f;
			this.isMoving = (num > this.movementThreshold);
			if (this.playArea != null && (num2 > this.playAreaMovementThreshold || !this.onGround))
			{
				this.isMoving = false;
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0008960C File Offset: 0x0008780C
		protected virtual void CheckLean()
		{
			if (this.bodyCollider != null)
			{
				Vector3 vector = (this.headset != null) ? new Vector3(this.currentStandingPosition.x, this.headset.position.y, this.currentStandingPosition.y) : Vector3.zero;
				Vector3 direction = (this.playArea != null) ? (-this.playArea.up) : Vector3.zero;
				Ray ray = new Ray(vector, direction);
				RaycastHit raycastHit;
				this.currentValidFloorObject = (VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.Ignore) ? raycastHit.collider.gameObject : null);
				if (this.headset == null || this.playArea == null || !this.enableBodyCollisions)
				{
					return;
				}
				Quaternion rotation = this.headset.rotation;
				this.headset.rotation = new Quaternion(0f, this.headset.rotation.y, this.headset.rotation.z, this.headset.rotation.w);
				Ray ray2 = new Ray(this.headset.position, this.headset.forward);
				float num = this.bodyCollider.radius + this.leanForwardLengthAddition;
				RaycastHit raycastHit2;
				if (!VRTK_CustomRaycast.Raycast(this.customRaycast, ray2, out raycastHit2, this.layersToIgnore, num, QueryTriggerInteraction.Ignore) && this.currentValidFloorObject != null)
				{
					this.CalculateLean(vector, num, raycastHit.distance);
				}
				this.headset.rotation = rotation;
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000897B8 File Offset: 0x000879B8
		protected virtual void CalculateLean(Vector3 startPosition, float forwardLength, float originalRayDistance)
		{
			Vector3 vector = startPosition + this.headset.forward * forwardLength;
			vector = new Vector3(vector.x, startPosition.y, vector.z);
			Ray ray = new Ray(vector, -this.playArea.up);
			RaycastHit raycastHit;
			if (VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.Ignore))
			{
				float num = VRTK_SharedMethods.RoundFloat(originalRayDistance - raycastHit.distance, this.decimalPrecision, false);
				float num2 = VRTK_SharedMethods.RoundFloat(Vector3.Distance(this.playArea.transform.position, this.lastPlayAreaPosition), this.decimalPrecision, false);
				this.isMoving = ((this.onGround && num2 <= this.playAreaPositionThreshold && num > 0f) || this.isMoving);
				this.isLeaning = (this.onGround && num > this.leanYThreshold);
				if (this.isLeaning)
				{
					this.OnStartLeaning(this.SetBodyPhysicsEvent(raycastHit.collider.gameObject, raycastHit.collider));
					return;
				}
				this.OnStopLeaning(this.SetBodyPhysicsEvent(null, null));
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000898E8 File Offset: 0x00087AE8
		protected virtual void UpdateStandingPosition(Vector2 currentHeadsetPosition)
		{
			this.standingPositionHistory.Add(currentHeadsetPosition);
			if (this.standingPositionHistory.Count > this.standingHistorySamples)
			{
				if (!this.isLeaning && this.currentCollidingObject == null)
				{
					bool flag = true;
					for (int i = 0; i < this.standingHistorySamples; i++)
					{
						flag = (Vector2.Distance(this.standingPositionHistory[i], this.standingPositionHistory[this.standingHistorySamples]) <= this.movementThreshold && flag);
					}
					this.currentStandingPosition = (flag ? currentHeadsetPosition : this.currentStandingPosition);
				}
				this.standingPositionHistory.Clear();
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0008998C File Offset: 0x00087B8C
		protected virtual void CheckHeadsetMovement()
		{
			bool flag = this.isMoving;
			Vector2 currentHeadsetPosition = (this.headset != null) ? new Vector2(this.headset.position.x, this.headset.position.z) : Vector2.zero;
			this.SetCurrentStandingPosition();
			this.SetIsMoving(currentHeadsetPosition);
			this.CheckLean();
			this.UpdateStandingPosition(currentHeadsetPosition);
			if (this.enableBodyCollisions)
			{
				this.TogglePhysics(!this.isMoving);
			}
			if (flag != this.isMoving)
			{
				this.MovementChanged(this.isMoving);
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00089A1F File Offset: 0x00087C1F
		protected virtual void MovementChanged(bool movementState)
		{
			if (movementState)
			{
				this.OnStartMoving(this.SetBodyPhysicsEvent(null, null));
				return;
			}
			this.OnStopMoving(this.SetBodyPhysicsEvent(null, null));
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00089A44 File Offset: 0x00087C44
		protected virtual void EnableDropToFloor()
		{
			this.initialFloorDrop = false;
			this.retogglePhysicsOnCanFall = false;
			this.teleporter = ((this.teleporter != null) ? this.teleporter : base.GetComponentInChildren<VRTK_BasicTeleport>());
			if (this.teleporter != null)
			{
				this.teleporter.Teleported += this.Teleported;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00089AA7 File Offset: 0x00087CA7
		protected virtual void DisableDropToFloor()
		{
			if (this.teleporter != null)
			{
				this.teleporter.Teleported -= this.Teleported;
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00089ACF File Offset: 0x00087CCF
		protected virtual void Teleported(object sender, DestinationMarkerEventArgs e)
		{
			this.initialFloorDrop = false;
			this.StopFall();
			if (this.resetPhysicsAfterTeleport)
			{
				this.TogglePhysics(this.storedCurrentPhysics);
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00089AF2 File Offset: 0x00087CF2
		protected virtual void EnableBodyPhysics()
		{
			this.currentBodyCollisionsSetting = this.enableBodyCollisions;
			this.CreateCollider();
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerLeftHand(false), true);
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerRightHand(false), true);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00089B20 File Offset: 0x00087D20
		protected virtual void DisableBodyPhysics()
		{
			this.DestroyCollider();
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerLeftHand(false), false);
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerRightHand(false), false);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00089B44 File Offset: 0x00087D44
		protected virtual void SetupIgnoredCollisions()
		{
			this.ResetIgnoredCollisions();
			if (this.ignoreCollisionsWith == null)
			{
				return;
			}
			for (int i = 0; i < this.ignoreCollisionsWith.Length; i++)
			{
				Collider[] componentsInChildren = this.ignoreCollisionsWith[i].GetComponentsInChildren<Collider>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					this.ManagePhysicsCollider(componentsInChildren[j], true);
				}
				if (componentsInChildren.Length != 0)
				{
					this.ignoreCollisionsOnGameObjects.Add(this.ignoreCollisionsWith[i]);
				}
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00089BB0 File Offset: 0x00087DB0
		protected virtual void ManagePhysicsCollider(Collider collider, bool state)
		{
			Physics.IgnoreCollision(this.bodyCollider, collider, state);
			Physics.IgnoreCollision(this.footCollider, collider, state);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00089BCC File Offset: 0x00087DCC
		protected virtual void CheckStepUpCollision(Collision collision)
		{
			if (this.bodyCollider != null && this.footCollider != null && collision.contacts.Length != 0 && collision.contacts[0].thisCollider.transform.name == this.footColliderContainerNameCheck)
			{
				float num = 0.55f;
				float y = 0.01f;
				Vector3 vector = this.playArea.TransformPoint(this.footCollider.center);
				Vector3 vector2 = new Vector3(vector.x, vector.y + this.CalculateStepUpYOffset() * num, vector.z);
				Vector3 halfExtents = new Vector3(this.bodyCollider.radius, y, this.bodyCollider.radius);
				float maxDistance = vector2.y - this.playArea.position.y;
				RaycastHit rayCollidedWith;
				if (Physics.BoxCast(vector2, halfExtents, Vector3.down, out rayCollidedWith, Quaternion.identity, maxDistance) && rayCollidedWith.point.y - this.playArea.position.y > this.stepDropThreshold)
				{
					if (this.teleporter != null && this.enableTeleport)
					{
						this.hitFloorYDelta = this.playArea.position.y - rayCollidedWith.point.y;
						this.TeleportFall(rayCollidedWith.point.y, rayCollidedWith);
						this.lastFrameFloorY = rayCollidedWith.point.y;
						return;
					}
					this.playArea.position = new Vector3(rayCollidedWith.point.x - (this.headset.position.x - this.playArea.position.x), rayCollidedWith.point.y, rayCollidedWith.point.z - (this.headset.position.z - this.playArea.position.z));
				}
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00089DC8 File Offset: 0x00087FC8
		protected virtual GameObject CreateColliderContainer(string name, Transform parent)
		{
			GameObject gameObject = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				name
			}));
			gameObject.transform.SetParent(parent);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			VRTK_PlayerObject.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.Collider);
			return gameObject;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00089E40 File Offset: 0x00088040
		protected virtual GameObject InstantiateColliderContainer(GameObject objectToClone, string name, Transform parent)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(objectToClone, parent);
			gameObject.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				name
			});
			VRTK_PlayerObject.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.Collider);
			return gameObject;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00089E74 File Offset: 0x00088074
		protected virtual void GenerateRigidbody()
		{
			this.bodyRigidbody = this.playArea.GetComponent<Rigidbody>();
			if (this.bodyRigidbody == null)
			{
				this.generateRigidbody = true;
				this.bodyRigidbody = this.playArea.gameObject.AddComponent<Rigidbody>();
				this.bodyRigidbody.mass = this.bodyMass;
				this.bodyRigidbody.freezeRotation = true;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00089EDC File Offset: 0x000880DC
		protected virtual CapsuleCollider GenerateCapsuleCollider(GameObject parent, float setRadius)
		{
			CapsuleCollider capsuleCollider = parent.GetComponent<CapsuleCollider>();
			if (capsuleCollider == null)
			{
				capsuleCollider = parent.AddComponent<CapsuleCollider>();
				capsuleCollider.radius = setRadius;
			}
			return capsuleCollider;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00089F08 File Offset: 0x00088108
		protected virtual void GenerateBodyCollider()
		{
			if (this.bodyColliderContainer == null)
			{
				if (this.customBodyColliderContainer != null)
				{
					this.bodyColliderContainer = this.InstantiateColliderContainer(this.customBodyColliderContainer, "BodyColliderContainer", this.playArea);
					this.bodyCollider = this.bodyColliderContainer.GetComponent<CapsuleCollider>();
				}
				else
				{
					this.bodyColliderContainer = this.CreateColliderContainer("BodyColliderContainer", this.playArea);
					this.bodyColliderContainer.gameObject.layer = LayerMask.NameToLayer("AlivePlayer");
				}
				this.bodyCollider = this.GenerateCapsuleCollider(this.bodyColliderContainer, this.bodyRadius);
				this.bodyCollider.GetComponent<Collider>().material = this.VRPhysicsMaterial;
				this.GenerateFootCollider();
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00089FCC File Offset: 0x000881CC
		protected virtual void GenerateFootCollider()
		{
			if (this.CalculateStepUpYOffset() > 0f)
			{
				if (this.customFootColliderContainer != null)
				{
					this.footColliderContainer = this.InstantiateColliderContainer(this.customFootColliderContainer, "FootColliderContainer", this.bodyColliderContainer.transform);
				}
				else
				{
					this.footColliderContainer = this.CreateColliderContainer("FootColliderContainer", this.bodyColliderContainer.transform);
					this.footColliderContainer.gameObject.layer = LayerMask.NameToLayer("AlivePlayer");
				}
				this.footCollider = this.GenerateCapsuleCollider(this.footColliderContainer, 0f);
				this.footCollider.GetComponent<Collider>().material = this.VRPhysicsMaterial;
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0008A080 File Offset: 0x00088280
		protected virtual void CreateCollider()
		{
			this.generateRigidbody = false;
			if (this.playArea == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
				return;
			}
			VRTK_PlayerObject.SetPlayerObject(this.playArea.gameObject, VRTK_PlayerObject.ObjectTypes.CameraRig);
			this.GenerateRigidbody();
			this.GenerateBodyCollider();
			if (this.playArea.gameObject.layer == 0)
			{
				this.playArea.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
			this.TogglePhysics(this.enableBodyCollisions);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0008A119 File Offset: 0x00088319
		protected virtual void DestroyCollider()
		{
			if (this.generateRigidbody && this.bodyRigidbody != null)
			{
				Object.Destroy(this.bodyRigidbody);
			}
			if (this.bodyColliderContainer != null)
			{
				Object.Destroy(this.bodyColliderContainer);
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0008A158 File Offset: 0x00088358
		protected virtual void UpdateCollider()
		{
			if (this.bodyCollider != null && this.headset != null)
			{
				float num = this.headset.position.y - this.playArea.position.y - (this.headsetYOffset + this.CalculateStepUpYOffset());
				float y = Mathf.Max(num * 0.5f + this.CalculateStepUpYOffset() + this.playAreaHeightAdjustment, this.bodyCollider.radius + this.playAreaHeightAdjustment);
				this.bodyCollider.height = Mathf.Max(num, this.bodyCollider.radius);
				this.bodyCollider.center = new Vector3(this.headset.localPosition.x, y, this.headset.localPosition.z);
				if (this.footCollider != null)
				{
					float radius = this.bodyCollider.radius * this.stepThicknessMultiplier;
					this.footCollider.radius = radius;
					this.footCollider.height = this.CalculateStepUpYOffset();
					this.footCollider.center = new Vector3(this.headset.localPosition.x, this.CalculateStepUpYOffset() * 0.5f, this.headset.localPosition.z);
				}
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0008A2AB File Offset: 0x000884AB
		protected virtual float CalculateStepUpYOffset()
		{
			return this.stepUpYOffset * 2f;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0008A2BC File Offset: 0x000884BC
		protected virtual void InitControllerListeners(GameObject mappedController, bool state)
		{
			if (mappedController != null)
			{
				this.IgnoreCollisions(mappedController.GetComponentsInChildren<Collider>(), true);
				VRTK_InteractGrab component = mappedController.GetComponent<VRTK_InteractGrab>();
				if (component != null && this.ignoreGrabbedCollisions)
				{
					if (state)
					{
						component.ControllerGrabInteractableObject += this.OnGrabObject;
						component.ControllerUngrabInteractableObject += this.OnUngrabObject;
						return;
					}
					component.ControllerGrabInteractableObject -= this.OnGrabObject;
					component.ControllerUngrabInteractableObject -= this.OnUngrabObject;
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0008A347 File Offset: 0x00088547
		protected virtual IEnumerator RestoreCollisions(GameObject obj)
		{
			yield return new WaitForEndOfFrame();
			if (obj != null)
			{
				VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
				if (component != null && !component.IsGrabbed(null))
				{
					this.IgnoreCollisions(obj.GetComponentsInChildren<Collider>(), false);
				}
			}
			yield break;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0008A360 File Offset: 0x00088560
		protected virtual void IgnoreCollisions(Collider[] colliders, bool state)
		{
			if (this.bodyColliderContainer != null)
			{
				foreach (Collider collider in this.bodyColliderContainer.GetComponentsInChildren<Collider>())
				{
					if (collider.gameObject.activeInHierarchy)
					{
						foreach (Collider collider2 in colliders)
						{
							if (collider2.gameObject.activeInHierarchy)
							{
								Physics.IgnoreCollision(collider, collider2, state);
							}
						}
					}
				}
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0008A3D0 File Offset: 0x000885D0
		protected virtual void OnGrabObject(object sender, ObjectInteractEventArgs e)
		{
			if (e.target != null)
			{
				base.StopCoroutine("RestoreCollisions");
				this.IgnoreCollisions(e.target.GetComponentsInChildren<Collider>(), true);
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0008A3FD File Offset: 0x000885FD
		protected virtual void OnUngrabObject(object sender, ObjectInteractEventArgs e)
		{
			if (base.gameObject.activeInHierarchy && this.playArea.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.RestoreCollisions(e.target));
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0008A434 File Offset: 0x00088634
		protected virtual bool FloorIsGrabbedObject(RaycastHit collidedObj)
		{
			if (this.cachedGrabbedObjectTransform != collidedObj.transform)
			{
				this.cachedGrabbedObjectTransform = collidedObj.transform;
				this.cachedGrabbedObject = collidedObj.transform.GetComponent<VRTK_InteractableObject>();
			}
			return this.cachedGrabbedObject != null && this.cachedGrabbedObject.IsGrabbed(null);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0008A490 File Offset: 0x00088690
		protected virtual bool FloorHeightChanged(float currentY)
		{
			return Mathf.Abs(currentY - this.lastFrameFloorY) > this.floorHeightTolerance;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0008A4A7 File Offset: 0x000886A7
		protected virtual bool ValidDrop(bool rayHit, RaycastHit rayCollidedWith, float floorY)
		{
			return rayHit && this.teleporter != null && this.teleporter.ValidLocation(rayCollidedWith.transform, rayCollidedWith.point) && !this.FloorIsGrabbedObject(rayCollidedWith) && this.FloorHeightChanged(floorY);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0008A4E8 File Offset: 0x000886E8
		protected virtual float ControllerHeightCheck(GameObject controllerObj)
		{
			Ray ray = new Ray(controllerObj.transform.position, -this.playArea.up);
			RaycastHit raycastHit;
			VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.Ignore);
			return controllerObj.transform.position.y - raycastHit.distance;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0008A54C File Offset: 0x0008874C
		protected virtual bool ControllersStillOverPreviousFloor()
		{
			if (this.fallRestriction == VRTK_BodyPhysics.FallingRestrictors.NoRestriction)
			{
				return false;
			}
			float num = 0.05f;
			GameObject controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(false);
			GameObject controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(false);
			float y = this.playArea.position.y;
			bool flag = controllerRightHand.activeInHierarchy && Mathf.Abs(this.ControllerHeightCheck(controllerRightHand) - y) < num;
			bool flag2 = controllerLeftHand.activeInHierarchy && Mathf.Abs(this.ControllerHeightCheck(controllerLeftHand) - y) < num;
			if (this.fallRestriction == VRTK_BodyPhysics.FallingRestrictors.LeftController)
			{
				flag = false;
			}
			if (this.fallRestriction == VRTK_BodyPhysics.FallingRestrictors.RightController)
			{
				flag2 = false;
			}
			if (this.fallRestriction == VRTK_BodyPhysics.FallingRestrictors.BothControllers)
			{
				return flag && flag2;
			}
			return flag || flag2;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0008A5F0 File Offset: 0x000887F0
		protected virtual void SnapToNearestFloor()
		{
			if (!this.preventSnapToFloor && (this.enableBodyCollisions || this.enableTeleport) && this.headset != null && this.headset.transform.position.y > this.playArea.position.y)
			{
				Ray ray = new Ray(this.headset.transform.position, -this.playArea.up);
				RaycastHit rayCollidedWith;
				bool rayHit = VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out rayCollidedWith, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.Ignore);
				this.hitFloorYDelta = this.playArea.position.y - rayCollidedWith.point.y;
				if (this.initialFloorDrop && (this.ValidDrop(rayHit, rayCollidedWith, rayCollidedWith.point.y) || this.retogglePhysicsOnCanFall))
				{
					this.storedCurrentPhysics = this.ArePhysicsEnabled();
					this.resetPhysicsAfterTeleport = false;
					this.TogglePhysics(false);
					this.HandleFall(rayCollidedWith.point.y, rayCollidedWith);
				}
				this.initialFloorDrop = true;
				this.lastFrameFloorY = rayCollidedWith.point.y;
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0008A727 File Offset: 0x00088927
		protected virtual bool PreventFall(float hitFloorY)
		{
			return hitFloorY < this.playArea.position.y && this.ControllersStillOverPreviousFloor();
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0008A744 File Offset: 0x00088944
		protected virtual void HandleFall(float hitFloorY, RaycastHit rayCollidedWith)
		{
			if (this.PreventFall(hitFloorY))
			{
				if (!this.retogglePhysicsOnCanFall)
				{
					this.retogglePhysicsOnCanFall = true;
					this.storedRetogglePhysics = this.storedCurrentPhysics;
					return;
				}
			}
			else
			{
				if (this.retogglePhysicsOnCanFall)
				{
					this.storedCurrentPhysics = this.storedRetogglePhysics;
					this.retogglePhysicsOnCanFall = false;
				}
				if (this.enableBodyCollisions && (this.teleporter == null || !this.enableTeleport || this.hitFloorYDelta > this.gravityFallYThreshold))
				{
					this.GravityFall(rayCollidedWith);
					return;
				}
				if (this.teleporter != null && this.enableTeleport)
				{
					this.TeleportFall(hitFloorY, rayCollidedWith);
				}
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0008A7E8 File Offset: 0x000889E8
		protected virtual void StartFall(GameObject targetFloor)
		{
			if (this.IsLeaning())
			{
				this.OnStopLeaning(this.SetBodyPhysicsEvent(null, null));
			}
			if (this.OnGround())
			{
				this.OnStopTouchingGround(this.SetBodyPhysicsEvent(null, null));
			}
			this.isFalling = true;
			this.isMoving = false;
			this.isLeaning = false;
			this.onGround = false;
			this.fallMinTime = Time.time + Time.fixedDeltaTime * 3f;
			this.OnStartFalling(this.SetBodyPhysicsEvent(targetFloor, null));
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0008A864 File Offset: 0x00088A64
		protected virtual void StopFall()
		{
			bool flag = this.isFalling;
			if (!this.OnGround())
			{
				this.OnStartTouchingGround(this.SetBodyPhysicsEvent(this.currentValidFloorObject, null));
			}
			this.isFalling = false;
			this.onGround = true;
			this.enableBodyCollisions = this.enableBodyCollisionsStartingValue;
			if (flag)
			{
				this.OnStopFalling(this.SetBodyPhysicsEvent(null, null));
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0008A8BC File Offset: 0x00088ABC
		protected virtual void GravityFall(RaycastHit rayCollidedWith)
		{
			this.StartFall(rayCollidedWith.collider.gameObject);
			this.TogglePhysics(true);
			this.ApplyBodyVelocity(Vector3.zero, false, false);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0008A8E4 File Offset: 0x00088AE4
		protected virtual void TeleportFall(float floorY, RaycastHit rayCollidedWith)
		{
			this.StartFall(rayCollidedWith.collider.gameObject);
			GameObject gameObject = rayCollidedWith.transform.gameObject;
			Vector3 position = new Vector3(this.playArea.position.x, floorY, this.playArea.position.z);
			float blinkTransitionSpeed = this.teleporter.blinkTransitionSpeed;
			this.teleporter.blinkTransitionSpeed = ((Mathf.Abs(this.hitFloorYDelta) > this.blinkYThreshold) ? blinkTransitionSpeed : 0f);
			this.OnDestinationMarkerSet(this.SetDestinationMarkerEvent(rayCollidedWith.distance, gameObject.transform, rayCollidedWith, position, null, true, null));
			this.teleporter.blinkTransitionSpeed = blinkTransitionSpeed;
			this.resetPhysicsAfterTeleport = true;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0008A9A4 File Offset: 0x00088BA4
		protected virtual void ApplyBodyMomentum(bool applyMomentum = false)
		{
			if (applyMomentum)
			{
				float magnitude = this.bodyRigidbody.velocity.magnitude;
				Vector3 force = this.playAreaVelocity / ((magnitude < 1f) ? 1f : magnitude);
				this.bodyRigidbody.AddRelativeForce(force, ForceMode.VelocityChange);
			}
		}

		// Token: 0x040014E7 RID: 5351
		[Header("Body Collision Settings")]
		public PhysicMaterial VRPhysicsMaterial;

		// Token: 0x040014E8 RID: 5352
		[Tooltip("If checked then the body collider and rigidbody will be used to check for rigidbody collisions.")]
		public bool enableBodyCollisions = true;

		// Token: 0x040014E9 RID: 5353
		[Tooltip("If this is checked then any items that are grabbed with the controller will not collide with the body collider. This is very useful if the user is required to grab and wield objects because if the collider was active they would bounce off the collider.")]
		public bool ignoreGrabbedCollisions = true;

		// Token: 0x040014EA RID: 5354
		[Tooltip("An array of GameObjects that will not collide with the body collider.")]
		public GameObject[] ignoreCollisionsWith;

		// Token: 0x040014EB RID: 5355
		[Tooltip("The collider which is created for the user is set at a height from the user's headset position. If the collider is required to be lower to allow for room between the play area collider and the headset then this offset value will shorten the height of the generated collider.")]
		public float headsetYOffset = 0.2f;

		// Token: 0x040014EC RID: 5356
		[Tooltip("The amount of movement of the headset between the headset's current position and the current standing position to determine if the user is walking in play space and to ignore the body physics collisions if the movement delta is above this threshold.")]
		public float movementThreshold = 0.0015f;

		// Token: 0x040014ED RID: 5357
		[Tooltip("The amount of movement of the play area between the play area's current position and the previous position to determine if the user is moving play space.")]
		public float playAreaMovementThreshold = 0.00075f;

		// Token: 0x040014EE RID: 5358
		[Tooltip("The maximum number of samples to collect of headset position before determining if the current standing position within the play space has changed.")]
		public int standingHistorySamples = 5;

		// Token: 0x040014EF RID: 5359
		[Tooltip("The `y` distance between the headset and the object being leaned over, if object being leaned over is taller than this threshold then the current standing position won't be updated.")]
		public float leanYThreshold = 0.5f;

		// Token: 0x040014F0 RID: 5360
		[Header("Step Settings")]
		[Tooltip("The maximum height to consider when checking if an object can be stepped upon to.")]
		public float stepUpYOffset = 0.15f;

		// Token: 0x040014F1 RID: 5361
		[Tooltip("The width/depth of the foot collider in relation to the radius of the body collider.")]
		[Range(0.1f, 0.9f)]
		public float stepThicknessMultiplier = 0.5f;

		// Token: 0x040014F2 RID: 5362
		[Tooltip("The distance between the current play area Y position and the new stepped up Y position to consider a valid step up. A higher number can help with juddering on slopes or small increases in collider heights.")]
		public float stepDropThreshold = 0.08f;

		// Token: 0x040014F3 RID: 5363
		[Header("Snap To Floor Settings")]
		[Tooltip("A custom raycaster to use when raycasting to find floors.")]
		public VRTK_CustomRaycast customRaycast;

		// Token: 0x040014F4 RID: 5364
		[Tooltip("**OBSOLETE [Use customRaycast]** The layers to ignore when raycasting to find floors.")]
		[Obsolete("`VRTK_BodyPhysics.layersToIgnore` is no longer used in the `VRTK_BodyPhysics` class, use the `customRaycast` parameter instead. This parameter will be removed in a future version of VRTK.")]
		public LayerMask layersToIgnore = 4;

		// Token: 0x040014F5 RID: 5365
		[Tooltip("A check to see if the drop to nearest floor should take place. If the selected restrictor is still over the current floor then the drop to nearest floor will not occur. Works well for being able to lean over ledges and look down. Only works for falling down not teleporting up.")]
		public VRTK_BodyPhysics.FallingRestrictors fallRestriction;

		// Token: 0x040014F6 RID: 5366
		[Tooltip("When the `y` distance between the floor and the headset exceeds this distance and `Enable Body Collisions` is true then the rigidbody gravity will be used instead of teleport to drop to nearest floor.")]
		public float gravityFallYThreshold = 1f;

		// Token: 0x040014F7 RID: 5367
		[Tooltip("The `y` distance between the floor and the headset that must change before a fade transition is initiated. If the new user location is at a higher distance than the threshold then the headset blink transition will activate on teleport. If the new user location is within the threshold then no blink transition will happen, which is useful for walking up slopes, meshes and terrains to prevent constant blinking.")]
		public float blinkYThreshold = 0.15f;

		// Token: 0x040014F8 RID: 5368
		[Tooltip("The amount the `y` position needs to change by between the current floor `y` position and the previous floor `y` position before a change in floor height is considered to have occurred. A higher value here will mean that a `Drop To Floor` will be less likely to happen if the `y` of the floor beneath the user hasn't changed as much as the given threshold.")]
		public float floorHeightTolerance = 0.001f;

		// Token: 0x040014F9 RID: 5369
		[Range(1f, 10f)]
		[Tooltip("The amount of rounding on the play area Y position to be applied when checking if falling is occuring.")]
		public int fallCheckPrecision = 5;

		// Token: 0x040014FA RID: 5370
		[Header("Custom Settings")]
		[Tooltip("The VRTK Teleport script to use when snapping to floor. If this is left blank then a Teleport script will need to be applied to the same GameObject.")]
		public VRTK_BasicTeleport teleporter;

		// Token: 0x040014FB RID: 5371
		[Tooltip("A GameObject to represent a custom body collider container. It should contain a collider component that will be used for detecting body collisions. If one isn't provided then it will be auto generated.")]
		public GameObject customBodyColliderContainer;

		// Token: 0x040014FC RID: 5372
		[Tooltip("A GameObject to represent a custom foot collider container. It should contain a collider component that will be used for detecting step collisions. If one isn't provided then it will be auto generated.")]
		public GameObject customFootColliderContainer;

		// Token: 0x04001507 RID: 5383
		protected Transform playArea;

		// Token: 0x04001508 RID: 5384
		protected Transform headset;

		// Token: 0x04001509 RID: 5385
		protected Rigidbody bodyRigidbody;

		// Token: 0x0400150A RID: 5386
		protected GameObject bodyColliderContainer;

		// Token: 0x0400150B RID: 5387
		protected GameObject footColliderContainer;

		// Token: 0x0400150C RID: 5388
		protected CapsuleCollider bodyCollider;

		// Token: 0x0400150D RID: 5389
		protected CapsuleCollider footCollider;

		// Token: 0x0400150E RID: 5390
		protected VRTK_CollisionTracker collisionTracker;

		// Token: 0x0400150F RID: 5391
		protected bool currentBodyCollisionsSetting;

		// Token: 0x04001510 RID: 5392
		protected GameObject currentCollidingObject;

		// Token: 0x04001511 RID: 5393
		protected GameObject currentValidFloorObject;

		// Token: 0x04001512 RID: 5394
		protected float lastFrameFloorY;

		// Token: 0x04001513 RID: 5395
		protected float hitFloorYDelta;

		// Token: 0x04001514 RID: 5396
		protected bool initialFloorDrop;

		// Token: 0x04001515 RID: 5397
		protected bool resetPhysicsAfterTeleport;

		// Token: 0x04001516 RID: 5398
		protected bool storedCurrentPhysics;

		// Token: 0x04001517 RID: 5399
		protected bool retogglePhysicsOnCanFall;

		// Token: 0x04001518 RID: 5400
		protected bool storedRetogglePhysics;

		// Token: 0x04001519 RID: 5401
		protected Vector3 lastPlayAreaPosition = Vector3.zero;

		// Token: 0x0400151A RID: 5402
		protected Vector2 currentStandingPosition;

		// Token: 0x0400151B RID: 5403
		protected List<Vector2> standingPositionHistory = new List<Vector2>();

		// Token: 0x0400151C RID: 5404
		protected float playAreaHeightAdjustment = 0.009f;

		// Token: 0x0400151D RID: 5405
		protected float bodyMass = 100f;

		// Token: 0x0400151E RID: 5406
		protected float bodyRadius = 0.15f;

		// Token: 0x0400151F RID: 5407
		protected float leanForwardLengthAddition = 0.05f;

		// Token: 0x04001520 RID: 5408
		protected float playAreaPositionThreshold = 0.002f;

		// Token: 0x04001521 RID: 5409
		protected float gravityPush = -0.001f;

		// Token: 0x04001522 RID: 5410
		protected int decimalPrecision = 3;

		// Token: 0x04001523 RID: 5411
		protected bool isFalling;

		// Token: 0x04001524 RID: 5412
		protected bool isMoving;

		// Token: 0x04001525 RID: 5413
		protected bool isLeaning;

		// Token: 0x04001526 RID: 5414
		protected bool onGround = true;

		// Token: 0x04001527 RID: 5415
		protected bool preventSnapToFloor;

		// Token: 0x04001528 RID: 5416
		protected bool generateRigidbody;

		// Token: 0x04001529 RID: 5417
		protected Vector3 playAreaVelocity = Vector3.zero;

		// Token: 0x0400152A RID: 5418
		protected string footColliderContainerNameCheck;

		// Token: 0x0400152B RID: 5419
		protected const string BODY_COLLIDER_CONTAINER_NAME = "BodyColliderContainer";

		// Token: 0x0400152C RID: 5420
		protected const string FOOT_COLLIDER_CONTAINER_NAME = "FootColliderContainer";

		// Token: 0x0400152D RID: 5421
		protected bool enableBodyCollisionsStartingValue;

		// Token: 0x0400152E RID: 5422
		protected float fallMinTime;

		// Token: 0x0400152F RID: 5423
		protected List<GameObject> ignoreCollisionsOnGameObjects = new List<GameObject>();

		// Token: 0x04001530 RID: 5424
		protected Transform cachedGrabbedObjectTransform;

		// Token: 0x04001531 RID: 5425
		protected VRTK_InteractableObject cachedGrabbedObject;

		// Token: 0x04001532 RID: 5426
		protected bool drawDebugGizmo;

		// Token: 0x020005F5 RID: 1525
		public enum FallingRestrictors
		{
			// Token: 0x0400282C RID: 10284
			NoRestriction,
			// Token: 0x0400282D RID: 10285
			LeftController,
			// Token: 0x0400282E RID: 10286
			RightController,
			// Token: 0x0400282F RID: 10287
			EitherController,
			// Token: 0x04002830 RID: 10288
			BothControllers
		}
	}
}
