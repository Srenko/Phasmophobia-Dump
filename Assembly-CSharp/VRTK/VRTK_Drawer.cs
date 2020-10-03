using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x02000290 RID: 656
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Drawer")]
	public class VRTK_Drawer : VRTK_Control
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x0006F2AC File Offset: 0x0006D4AC
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (!base.enabled || !this.setupSuccessful)
			{
				return;
			}
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.GetHandle().transform, this.GetHandle().transform, null);
			float num = bounds.extents.y * (this.handle ? 5f : 1f);
			Vector3 vector = bounds.center;
			switch (this.finalDirection)
			{
			case VRTK_Control.Direction.x:
				vector -= base.transform.right.normalized * (num * this.subDirection);
				break;
			case VRTK_Control.Direction.y:
				vector -= base.transform.up.normalized * (num * this.subDirection);
				break;
			case VRTK_Control.Direction.z:
				vector -= base.transform.forward.normalized * (num * this.subDirection);
				break;
			}
			Gizmos.DrawLine(bounds.center, vector);
			Gizmos.DrawSphere(vector, num / 4f);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006F3CF File Offset: 0x0006D5CF
		protected override void InitRequiredComponents()
		{
			this.initialPosition = base.transform.position;
			this.InitBody();
			this.InitHandle();
			this.SetContent(this.content, this.hideContent);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0006F400 File Offset: 0x0006D600
		protected override bool DetectSetup()
		{
			this.finalDirection = ((this.direction == VRTK_Control.Direction.autodetect) ? this.DetectDirection() : this.direction);
			if (this.finalDirection == VRTK_Control.Direction.autodetect)
			{
				return false;
			}
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.GetHandle().transform, base.transform, null);
			Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.GetBody().transform, base.transform, null);
			switch (this.finalDirection)
			{
			case VRTK_Control.Direction.x:
				this.subDirection = (float)((bounds.center.x > bounds2.center.x) ? -1 : 1);
				this.pullDistance = bounds2.extents.x;
				break;
			case VRTK_Control.Direction.y:
				this.subDirection = (float)((bounds.center.y > bounds2.center.y) ? -1 : 1);
				this.pullDistance = bounds2.extents.y;
				break;
			case VRTK_Control.Direction.z:
				this.subDirection = (float)((bounds.center.z > bounds2.center.z) ? -1 : 1);
				this.pullDistance = bounds2.extents.z;
				break;
			}
			if ((this.body & this.handle) && this.handle.transform.IsChildOf(this.body.transform))
			{
				return false;
			}
			if (this.drawerJointCreated)
			{
				this.drawerJoint.xMotion = ConfigurableJointMotion.Locked;
				this.drawerJoint.yMotion = ConfigurableJointMotion.Locked;
				this.drawerJoint.zMotion = ConfigurableJointMotion.Locked;
				switch (this.finalDirection)
				{
				case VRTK_Control.Direction.x:
					this.drawerJoint.axis = Vector3.right;
					this.drawerJoint.xMotion = ConfigurableJointMotion.Limited;
					break;
				case VRTK_Control.Direction.y:
					this.drawerJoint.axis = Vector3.up;
					this.drawerJoint.yMotion = ConfigurableJointMotion.Limited;
					break;
				case VRTK_Control.Direction.z:
					this.drawerJoint.axis = Vector3.forward;
					this.drawerJoint.zMotion = ConfigurableJointMotion.Limited;
					break;
				}
				this.drawerJoint.anchor = this.drawerJoint.axis * (-this.subDirection * this.pullDistance);
			}
			if (this.drawerJoint)
			{
				this.drawerJoint.angularXMotion = ConfigurableJointMotion.Locked;
				this.drawerJoint.angularYMotion = ConfigurableJointMotion.Locked;
				this.drawerJoint.angularZMotion = ConfigurableJointMotion.Locked;
				this.pullDistance *= this.maxExtend * 1.8f;
				SoftJointLimit linearLimit = this.drawerJoint.linearLimit;
				linearLimit.limit = this.pullDistance;
				this.drawerJoint.linearLimit = linearLimit;
				if (this.connectedTo)
				{
					this.drawerJoint.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
				}
			}
			if (this.drawerSnapForceCreated)
			{
				this.drawerSnapForce.force = base.GetThirdDirection(this.drawerJoint.axis, this.drawerJoint.secondaryAxis) * (this.subDirection * -50f);
			}
			return true;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006F708 File Offset: 0x0006D908
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = 0f,
				controlMax = 100f
			};
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006F738 File Offset: 0x0006D938
		protected override void HandleUpdate()
		{
			this.value = this.CalculateValue();
			bool flag = Mathf.Abs(this.value) < this.minSnapClose * 100f;
			this.drawerSnapForce.enabled = flag;
			if (this.autoTriggerVolume)
			{
				this.autoTriggerVolume.isEnabled = !flag;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0006F794 File Offset: 0x0006D994
		protected virtual void InitBody()
		{
			this.drawerRigidbody = base.GetComponent<Rigidbody>();
			if (this.drawerRigidbody == null)
			{
				this.drawerRigidbody = base.gameObject.AddComponent<Rigidbody>();
				this.drawerRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
			this.drawerRigidbody.isKinematic = false;
			this.drawerInteractableObject = base.GetComponent<VRTK_InteractableObject>();
			if (this.drawerInteractableObject == null)
			{
				this.drawerInteractableObject = base.gameObject.AddComponent<VRTK_InteractableObject>();
			}
			this.drawerInteractableObject.isGrabbable = true;
			this.drawerInteractableObject.grabAttachMechanicScript = base.gameObject.AddComponent<VRTK_SpringJointGrabAttach>();
			this.drawerInteractableObject.grabAttachMechanicScript.precisionGrab = true;
			this.drawerInteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			this.drawerInteractableObject.stayGrabbedOnTeleport = false;
			if (this.connectedTo && this.connectedTo.GetComponent<Rigidbody>() == null)
			{
				Rigidbody rigidbody = this.connectedTo.AddComponent<Rigidbody>();
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
			}
			this.drawerJoint = base.GetComponent<ConfigurableJoint>();
			if (this.drawerJoint == null)
			{
				this.drawerJoint = base.gameObject.AddComponent<ConfigurableJoint>();
				this.drawerJointCreated = true;
			}
			this.drawerSnapForce = base.GetComponent<ConstantForce>();
			if (this.drawerSnapForce == null)
			{
				this.drawerSnapForce = base.gameObject.AddComponent<ConstantForce>();
				this.drawerSnapForce.enabled = false;
				this.drawerSnapForceCreated = true;
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0006F90C File Offset: 0x0006DB0C
		protected virtual void InitHandle()
		{
			this.handleRigidbody = this.GetHandle().GetComponent<Rigidbody>();
			if (this.handleRigidbody == null)
			{
				this.handleRigidbody = this.GetHandle().AddComponent<Rigidbody>();
			}
			this.handleRigidbody.isKinematic = false;
			this.handleRigidbody.useGravity = false;
			this.handleFixedJoint = this.GetHandle().GetComponent<FixedJoint>();
			if (this.handleFixedJoint == null)
			{
				this.handleFixedJoint = this.GetHandle().AddComponent<FixedJoint>();
				this.handleFixedJoint.connectedBody = this.drawerRigidbody;
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006F9A4 File Offset: 0x0006DBA4
		protected virtual VRTK_Control.Direction DetectDirection()
		{
			VRTK_Control.Direction result = VRTK_Control.Direction.autodetect;
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.GetHandle().transform, base.transform, null);
			Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.GetBody().transform, base.transform, null);
			float num = Mathf.Abs(bounds.center.x - (bounds2.center.x + bounds2.extents.x));
			float num2 = Mathf.Abs(bounds.center.y - (bounds2.center.y + bounds2.extents.y));
			float num3 = Mathf.Abs(bounds.center.z - (bounds2.center.z + bounds2.extents.z));
			float num4 = Mathf.Abs(bounds.center.x - (bounds2.center.x - bounds2.extents.x));
			float num5 = Mathf.Abs(bounds.center.y - (bounds2.center.y - bounds2.extents.y));
			float num6 = Mathf.Abs(bounds.center.z - (bounds2.center.z - bounds2.extents.z));
			if (VRTK_SharedMethods.IsLowest(num, new float[]
			{
				num2,
				num3,
				num4,
				num5,
				num6
			}))
			{
				result = VRTK_Control.Direction.x;
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
				result = VRTK_Control.Direction.x;
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
				result = VRTK_Control.Direction.y;
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
				result = VRTK_Control.Direction.y;
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
				result = VRTK_Control.Direction.z;
			}
			else if (VRTK_SharedMethods.IsLowest(num6, new float[]
			{
				num,
				num2,
				num3,
				num5,
				num4
			}))
			{
				result = VRTK_Control.Direction.z;
			}
			return result;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
		protected virtual float CalculateValue()
		{
			return Mathf.Round((base.transform.position - this.initialPosition).magnitude / this.pullDistance * 100f);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006FC35 File Offset: 0x0006DE35
		protected virtual GameObject GetBody()
		{
			if (!this.body)
			{
				return base.gameObject;
			}
			return this.body;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0006FC51 File Offset: 0x0006DE51
		protected virtual GameObject GetHandle()
		{
			if (!this.handle)
			{
				return base.gameObject;
			}
			return this.handle;
		}

		// Token: 0x04001149 RID: 4425
		[Tooltip("An optional game object to which the drawer will be connected. If the game object moves the drawer will follow along.")]
		public GameObject connectedTo;

		// Token: 0x0400114A RID: 4426
		[Tooltip("The axis on which the drawer should open. All other axis will be frozen.")]
		public VRTK_Control.Direction direction;

		// Token: 0x0400114B RID: 4427
		[Tooltip("The game object for the body.")]
		public GameObject body;

		// Token: 0x0400114C RID: 4428
		[Tooltip("The game object for the handle.")]
		public GameObject handle;

		// Token: 0x0400114D RID: 4429
		[Tooltip("The parent game object for the drawer content elements.")]
		public GameObject content;

		// Token: 0x0400114E RID: 4430
		[Tooltip("Makes the content invisible while the drawer is closed.")]
		public bool hideContent = true;

		// Token: 0x0400114F RID: 4431
		[Tooltip("If the extension of the drawer is below this percentage then the drawer will snap shut.")]
		[Range(0f, 1f)]
		public float minSnapClose = 1f;

		// Token: 0x04001150 RID: 4432
		[Tooltip("The maximum percentage of the drawer's total length that the drawer will open to.")]
		[Range(0f, 1f)]
		public float maxExtend = 1f;

		// Token: 0x04001151 RID: 4433
		protected Rigidbody drawerRigidbody;

		// Token: 0x04001152 RID: 4434
		protected Rigidbody handleRigidbody;

		// Token: 0x04001153 RID: 4435
		protected FixedJoint handleFixedJoint;

		// Token: 0x04001154 RID: 4436
		protected ConfigurableJoint drawerJoint;

		// Token: 0x04001155 RID: 4437
		protected VRTK_InteractableObject drawerInteractableObject;

		// Token: 0x04001156 RID: 4438
		protected ConstantForce drawerSnapForce;

		// Token: 0x04001157 RID: 4439
		protected VRTK_Control.Direction finalDirection;

		// Token: 0x04001158 RID: 4440
		protected float subDirection = 1f;

		// Token: 0x04001159 RID: 4441
		protected float pullDistance;

		// Token: 0x0400115A RID: 4442
		protected Vector3 initialPosition;

		// Token: 0x0400115B RID: 4443
		protected bool drawerJointCreated;

		// Token: 0x0400115C RID: 4444
		protected bool drawerSnapForceCreated;
	}
}
