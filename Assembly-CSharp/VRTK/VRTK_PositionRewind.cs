using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002FD RID: 765
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_PositionRewind")]
	public class VRTK_PositionRewind : MonoBehaviour
	{
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06001A76 RID: 6774 RVA: 0x0008BBD4 File Offset: 0x00089DD4
		// (remove) Token: 0x06001A77 RID: 6775 RVA: 0x0008BC0C File Offset: 0x00089E0C
		public event PositionRewindEventHandler PositionRewindToSafe;

		// Token: 0x06001A78 RID: 6776 RVA: 0x0008BC41 File Offset: 0x00089E41
		public virtual void OnPositionRewindToSafe(PositionRewindEventArgs e)
		{
			if (this.PositionRewindToSafe != null)
			{
				this.PositionRewindToSafe(this, e);
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0008BC58 File Offset: 0x00089E58
		public virtual void SetLastGoodPosition()
		{
			if (this.playArea != null && this.headset != null)
			{
				this.lastGoodPositionSet = true;
				this.lastGoodStandingPosition = this.playArea.position;
				this.lastGoodHeadsetPosition = this.headset.position;
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0008BCAC File Offset: 0x00089EAC
		public virtual void RewindPosition()
		{
			if (this.headset != null)
			{
				Vector3 position = this.playArea.position;
				Vector3 a = this.lastGoodHeadsetPosition - this.headset.position;
				Vector3 b = a.normalized * this.pushbackDistance;
				this.playArea.position += a + b;
				if (this.bodyPhysics != null)
				{
					this.bodyPhysics.ResetVelocities();
				}
				this.OnPositionRewindToSafe(this.SetEventPayload(position));
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0008BD40 File Offset: 0x00089F40
		protected virtual void OnEnable()
		{
			this.lastGoodPositionSet = false;
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			if (this.playArea == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
			}
			this.bodyPhysics = ((this.bodyPhysics != null) ? this.bodyPhysics : Object.FindObjectOfType<VRTK_BodyPhysics>());
			this.headsetCollision = ((this.headsetCollision != null) ? this.headsetCollision : base.GetComponentInChildren<VRTK_HeadsetCollision>());
			this.ManageListeners(true);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0008BDE3 File Offset: 0x00089FE3
		protected virtual void OnDisable()
		{
			this.ManageListeners(false);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0008BDEC File Offset: 0x00089FEC
		protected virtual void Update()
		{
			if (this.isColliding)
			{
				if (this.collideTimer > 0f)
				{
					this.collideTimer -= Time.deltaTime;
					return;
				}
				this.collideTimer = 0f;
				this.isColliding = false;
				this.DoPositionRewind();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0008BE3C File Offset: 0x0008A03C
		protected virtual PositionRewindEventArgs SetEventPayload(Vector3 previousPosition)
		{
			PositionRewindEventArgs result;
			result.collidedPosition = previousPosition;
			result.resetPosition = this.playArea.position;
			return result;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0008BE64 File Offset: 0x0008A064
		protected virtual bool CrouchThresholdReached()
		{
			float num = 0.005f;
			return this.playArea.position.y > this.lastPlayAreaY + num || this.playArea.position.y < this.lastPlayAreaY - num;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0008BEB0 File Offset: 0x0008A0B0
		protected virtual void SetHighestHeadsetY()
		{
			this.highestHeadsetY = (this.CrouchThresholdReached() ? this.crouchThreshold : ((this.headset.localPosition.y > this.highestHeadsetY) ? this.headset.localPosition.y : this.highestHeadsetY));
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0008BF04 File Offset: 0x0008A104
		protected virtual void UpdateLastGoodPosition()
		{
			float num = this.highestHeadsetY - this.crouchThreshold;
			if (this.headset.localPosition.y > num && num > this.crouchThreshold)
			{
				this.SetLastGoodPosition();
			}
			this.lastPlayAreaY = this.playArea.position.y;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0008BF57 File Offset: 0x0008A157
		protected virtual void FixedUpdate()
		{
			if (!this.isColliding && this.playArea != null)
			{
				this.SetHighestHeadsetY();
				this.UpdateLastGoodPosition();
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0008BF7C File Offset: 0x0008A17C
		protected virtual void StartCollision(GameObject target, Collider collider)
		{
			if (this.ignoreTriggerColliders && collider.isTrigger)
			{
				return;
			}
			if (!VRTK_PolicyList.Check(target, this.targetListPolicy))
			{
				this.isColliding = true;
				if (!this.hasCollided && this.collideTimer <= 0f)
				{
					this.hasCollided = true;
					this.collideTimer = this.rewindDelay;
				}
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0008BFD7 File Offset: 0x0008A1D7
		protected virtual void EndCollision(Collider collider)
		{
			if (this.ignoreTriggerColliders && collider != null && collider.isTrigger)
			{
				return;
			}
			this.isColliding = false;
			this.hasCollided = false;
			this.isRewinding = false;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0008C008 File Offset: 0x0008A208
		protected virtual bool BodyCollisionsEnabled()
		{
			return this.bodyPhysics == null || this.bodyPhysics.enableBodyCollisions;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0008C025 File Offset: 0x0008A225
		protected virtual bool CanRewind()
		{
			return !this.isRewinding && (this.playArea != null & this.lastGoodPositionSet) && this.headset.localPosition.y > this.crouchRewindThreshold && this.BodyCollisionsEnabled();
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0008C064 File Offset: 0x0008A264
		protected virtual void DoPositionRewind()
		{
			if (this.CanRewind())
			{
				this.isRewinding = true;
				this.RewindPosition();
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0008C07B File Offset: 0x0008A27B
		protected virtual bool HeadsetListen()
		{
			return this.collisionDetector == VRTK_PositionRewind.CollisionDetectors.HeadsetAndBody || this.collisionDetector == VRTK_PositionRewind.CollisionDetectors.HeadsetOnly;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0008C091 File Offset: 0x0008A291
		protected virtual bool BodyListen()
		{
			return this.collisionDetector == VRTK_PositionRewind.CollisionDetectors.HeadsetAndBody || this.collisionDetector == VRTK_PositionRewind.CollisionDetectors.BodyOnly;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0008C0A8 File Offset: 0x0008A2A8
		protected virtual void ManageListeners(bool state)
		{
			if (state)
			{
				if (this.headsetCollision != null && this.HeadsetListen())
				{
					this.headsetCollision.HeadsetCollisionDetect += this.HeadsetCollisionDetect;
					this.headsetCollision.HeadsetCollisionEnded += this.HeadsetCollisionEnded;
				}
				if (this.bodyPhysics != null && this.BodyListen())
				{
					this.bodyPhysics.StartColliding += this.StartColliding;
					this.bodyPhysics.StopColliding += this.StopColliding;
					return;
				}
			}
			else
			{
				if (this.headsetCollision != null && this.HeadsetListen())
				{
					this.headsetCollision.HeadsetCollisionDetect -= this.HeadsetCollisionDetect;
					this.headsetCollision.HeadsetCollisionEnded -= this.HeadsetCollisionEnded;
				}
				if (this.bodyPhysics != null && this.BodyListen())
				{
					this.bodyPhysics.StartColliding -= this.StartColliding;
					this.bodyPhysics.StopColliding -= this.StopColliding;
				}
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0008C1D6 File Offset: 0x0008A3D6
		private void StartColliding(object sender, BodyPhysicsEventArgs e)
		{
			this.StartCollision(e.target, e.collider);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0008C1EA File Offset: 0x0008A3EA
		private void StopColliding(object sender, BodyPhysicsEventArgs e)
		{
			this.EndCollision(e.collider);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0008C1F8 File Offset: 0x0008A3F8
		protected virtual void HeadsetCollisionDetect(object sender, HeadsetCollisionEventArgs e)
		{
			this.StartCollision(e.collider.gameObject, e.collider);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0008C211 File Offset: 0x0008A411
		protected virtual void HeadsetCollisionEnded(object sender, HeadsetCollisionEventArgs e)
		{
			this.EndCollision(e.collider);
		}

		// Token: 0x0400156F RID: 5487
		[Header("Rewind Settings")]
		[Tooltip("The colliders to determine if a collision has occured for the rewind to be actioned.")]
		public VRTK_PositionRewind.CollisionDetectors collisionDetector;

		// Token: 0x04001570 RID: 5488
		[Tooltip("If this is checked then the collision detector will ignore colliders set to `Is Trigger = true`.")]
		public bool ignoreTriggerColliders;

		// Token: 0x04001571 RID: 5489
		[Tooltip("The amount of time from original headset collision until the rewind to the last good known position takes place.")]
		public float rewindDelay = 0.5f;

		// Token: 0x04001572 RID: 5490
		[Tooltip("The additional distance to push the play area back upon rewind to prevent being right next to the wall again.")]
		public float pushbackDistance = 0.5f;

		// Token: 0x04001573 RID: 5491
		[Tooltip("The threshold to determine how low the headset has to be before it is considered the user is crouching. The last good position will only be recorded in a non-crouching position.")]
		public float crouchThreshold = 0.5f;

		// Token: 0x04001574 RID: 5492
		[Tooltip("The threshold to determind how low the headset can be to perform a position rewind. If the headset Y position is lower than this threshold then a rewind won't occur.")]
		public float crouchRewindThreshold = 0.1f;

		// Token: 0x04001575 RID: 5493
		[Tooltip("A specified VRTK_PolicyList to use to determine whether any objects will be acted upon by the Position Rewind.")]
		public VRTK_PolicyList targetListPolicy;

		// Token: 0x04001576 RID: 5494
		[Header("Custom Settings")]
		[Tooltip("The VRTK Body Physics script to use for the collisions and rigidbodies. If this is left blank then the first Body Physics script found in the scene will be used.")]
		public VRTK_BodyPhysics bodyPhysics;

		// Token: 0x04001577 RID: 5495
		[Tooltip("The VRTK Headset Collision script to use to determine if the headset is colliding. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_HeadsetCollision headsetCollision;

		// Token: 0x04001579 RID: 5497
		protected Transform headset;

		// Token: 0x0400157A RID: 5498
		protected Transform playArea;

		// Token: 0x0400157B RID: 5499
		protected Vector3 lastGoodStandingPosition;

		// Token: 0x0400157C RID: 5500
		protected Vector3 lastGoodHeadsetPosition;

		// Token: 0x0400157D RID: 5501
		protected float highestHeadsetY;

		// Token: 0x0400157E RID: 5502
		protected float lastPlayAreaY;

		// Token: 0x0400157F RID: 5503
		protected bool lastGoodPositionSet;

		// Token: 0x04001580 RID: 5504
		protected bool hasCollided;

		// Token: 0x04001581 RID: 5505
		protected bool isColliding;

		// Token: 0x04001582 RID: 5506
		protected bool isRewinding;

		// Token: 0x04001583 RID: 5507
		protected float collideTimer;

		// Token: 0x020005F7 RID: 1527
		public enum CollisionDetectors
		{
			// Token: 0x04002836 RID: 10294
			HeadsetOnly,
			// Token: 0x04002837 RID: 10295
			BodyOnly,
			// Token: 0x04002838 RID: 10296
			HeadsetAndBody
		}
	}
}
