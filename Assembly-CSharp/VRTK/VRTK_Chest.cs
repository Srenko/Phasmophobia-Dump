using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x0200028B RID: 651
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Chest")]
	public class VRTK_Chest : VRTK_Control
	{
		// Token: 0x060013DF RID: 5087 RVA: 0x0006D528 File Offset: 0x0006B728
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (!base.enabled || !this.setupSuccessful)
			{
				return;
			}
			Bounds bounds;
			if (this.handle)
			{
				bounds = VRTK_SharedMethods.GetBounds(this.handle.transform, this.handle.transform, null);
			}
			else
			{
				bounds = VRTK_SharedMethods.GetBounds(this.lid.transform, this.lid.transform, null);
			}
			float num = bounds.extents.y * 5f;
			Vector3 vector = bounds.center + new Vector3(0f, num, 0f);
			switch (this.finalDirection)
			{
			case VRTK_Control.Direction.x:
				vector += base.transform.right.normalized * (num / 2f) * this.subDirection;
				break;
			case VRTK_Control.Direction.y:
				vector += base.transform.up.normalized * (num / 2f) * this.subDirection;
				break;
			case VRTK_Control.Direction.z:
				vector += base.transform.forward.normalized * (num / 2f) * this.subDirection;
				break;
			}
			Gizmos.DrawLine(bounds.center + new Vector3(0f, bounds.extents.y, 0f), vector);
			Gizmos.DrawSphere(vector, num / 8f);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0006D6B9 File Offset: 0x0006B8B9
		protected override void InitRequiredComponents()
		{
			this.InitBody();
			this.InitLid();
			this.InitHandle();
			this.SetContent(this.content, this.hideContent);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0006D6E0 File Offset: 0x0006B8E0
		protected override bool DetectSetup()
		{
			if (this.lid == null || this.body == null)
			{
				return false;
			}
			this.finalDirection = ((this.direction == VRTK_Control.Direction.autodetect) ? this.DetectDirection() : this.direction);
			if (this.finalDirection == VRTK_Control.Direction.autodetect)
			{
				return false;
			}
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.lid.transform, base.transform, null);
			if (this.handle)
			{
				Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.handle.transform, base.transform, null);
				switch (this.finalDirection)
				{
				case VRTK_Control.Direction.x:
					this.subDirection = (float)((bounds2.center.x > bounds.center.x) ? -1 : 1);
					break;
				case VRTK_Control.Direction.y:
					this.subDirection = (float)((bounds2.center.y > bounds.center.y) ? -1 : 1);
					break;
				case VRTK_Control.Direction.z:
					this.subDirection = (float)((bounds2.center.z > bounds.center.z) ? -1 : 1);
					break;
				}
				if (this.handle.transform.IsChildOf(this.lid.transform))
				{
					return false;
				}
			}
			else
			{
				this.subDirection = -1f;
			}
			if (this.lidJointCreated)
			{
				this.lidJoint.useLimits = true;
				this.lidJoint.enableCollision = true;
				JointLimits limits = this.lidJoint.limits;
				switch (this.finalDirection)
				{
				case VRTK_Control.Direction.x:
					this.lidJoint.anchor = new Vector3(this.subDirection * bounds.extents.x, 0f, 0f);
					this.lidJoint.axis = new Vector3(0f, 0f, 1f);
					if (this.subDirection > 0f)
					{
						limits.min = -this.maxAngle;
						limits.max = this.minAngle;
					}
					else
					{
						limits.min = this.minAngle;
						limits.max = this.maxAngle;
					}
					break;
				case VRTK_Control.Direction.y:
					this.lidJoint.anchor = new Vector3(0f, this.subDirection * bounds.extents.y, 0f);
					this.lidJoint.axis = new Vector3(0f, 1f, 0f);
					if (this.subDirection > 0f)
					{
						limits.min = -this.maxAngle;
						limits.max = this.minAngle;
					}
					else
					{
						limits.min = this.minAngle;
						limits.max = this.maxAngle;
					}
					break;
				case VRTK_Control.Direction.z:
					this.lidJoint.anchor = new Vector3(0f, 0f, this.subDirection * bounds.extents.z);
					this.lidJoint.axis = new Vector3(1f, 0f, 0f);
					if (this.subDirection < 0f)
					{
						limits.min = -this.maxAngle;
						limits.max = this.minAngle;
					}
					else
					{
						limits.min = this.minAngle;
						limits.max = this.maxAngle;
					}
					break;
				}
				this.lidJoint.limits = limits;
			}
			return true;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0006DA48 File Offset: 0x0006BC48
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.lidJoint.limits.min,
				controlMax = this.lidJoint.limits.max
			};
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0006DA92 File Offset: 0x0006BC92
		protected override void HandleUpdate()
		{
			this.value = this.CalculateValue();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0006DAA0 File Offset: 0x0006BCA0
		protected virtual VRTK_Control.Direction DetectDirection()
		{
			VRTK_Control.Direction result = VRTK_Control.Direction.autodetect;
			if (!this.handle)
			{
				return result;
			}
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.handle.transform, base.transform, null);
			Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.lid.transform, base.transform, null);
			float num = Mathf.Abs(bounds.center.x - (bounds2.center.x + bounds2.extents.x));
			float num2 = Mathf.Abs(bounds.center.z - (bounds2.center.z + bounds2.extents.z));
			float num3 = Mathf.Abs(bounds.center.x - (bounds2.center.x - bounds2.extents.x));
			float num4 = Mathf.Abs(bounds.center.z - (bounds2.center.z - bounds2.extents.z));
			if (VRTK_SharedMethods.IsLowest(num, new float[]
			{
				num2,
				num3,
				num4
			}))
			{
				result = VRTK_Control.Direction.x;
			}
			else if (VRTK_SharedMethods.IsLowest(num3, new float[]
			{
				num,
				num2,
				num4
			}))
			{
				result = VRTK_Control.Direction.x;
			}
			else if (VRTK_SharedMethods.IsLowest(num2, new float[]
			{
				num,
				num3,
				num4
			}))
			{
				result = VRTK_Control.Direction.z;
			}
			else if (VRTK_SharedMethods.IsLowest(num4, new float[]
			{
				num,
				num2,
				num3
			}))
			{
				result = VRTK_Control.Direction.z;
			}
			return result;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0006DC24 File Offset: 0x0006BE24
		protected virtual void InitBody()
		{
			this.bodyRigidbody = this.body.GetComponent<Rigidbody>();
			if (this.bodyRigidbody == null)
			{
				this.bodyRigidbody = this.body.AddComponent<Rigidbody>();
				this.bodyRigidbody.isKinematic = true;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0006DC64 File Offset: 0x0006BE64
		protected virtual void InitLid()
		{
			this.lidRigidbody = this.lid.GetComponent<Rigidbody>();
			if (this.lidRigidbody == null)
			{
				this.lidRigidbody = this.lid.AddComponent<Rigidbody>();
			}
			this.lidJoint = this.lid.GetComponent<HingeJoint>();
			if (this.lidJoint == null)
			{
				this.lidJoint = this.lid.AddComponent<HingeJoint>();
				this.lidJointCreated = true;
			}
			this.lidJoint.connectedBody = this.bodyRigidbody;
			if (!this.handle)
			{
				this.CreateInteractableObject(this.lid);
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0006DD04 File Offset: 0x0006BF04
		protected virtual void InitHandle()
		{
			if (!this.handle)
			{
				return;
			}
			this.handleRigidbody = this.handle.GetComponent<Rigidbody>();
			if (this.handleRigidbody == null)
			{
				this.handleRigidbody = this.handle.AddComponent<Rigidbody>();
			}
			this.handleRigidbody.isKinematic = false;
			this.handleRigidbody.useGravity = false;
			this.handleJoint = this.handle.GetComponent<FixedJoint>();
			if (this.handleJoint == null)
			{
				this.handleJoint = this.handle.AddComponent<FixedJoint>();
				this.handleJoint.connectedBody = this.lidRigidbody;
			}
			this.CreateInteractableObject(this.handle);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0006DDB4 File Offset: 0x0006BFB4
		protected virtual void CreateInteractableObject(GameObject targetGameObject)
		{
			VRTK_InteractableObject vrtk_InteractableObject = targetGameObject.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = targetGameObject.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			vrtk_InteractableObject.grabAttachMechanicScript = base.gameObject.AddComponent<VRTK_TrackObjectGrabAttach>();
			vrtk_InteractableObject.secondaryGrabActionScript = base.gameObject.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.grabAttachMechanicScript.precisionGrab = true;
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0006DE14 File Offset: 0x0006C014
		protected virtual float CalculateValue()
		{
			return Mathf.Round((this.minAngle + Mathf.Clamp01(Mathf.Abs(this.lidJoint.angle / (this.lidJoint.limits.max - this.lidJoint.limits.min))) * (this.maxAngle - this.minAngle)) / this.stepSize) * this.stepSize;
		}

		// Token: 0x04001112 RID: 4370
		[Tooltip("The axis on which the chest should open. All other axis will be frozen.")]
		public VRTK_Control.Direction direction;

		// Token: 0x04001113 RID: 4371
		[Tooltip("The game object for the lid.")]
		public GameObject lid;

		// Token: 0x04001114 RID: 4372
		[Tooltip("The game object for the body.")]
		public GameObject body;

		// Token: 0x04001115 RID: 4373
		[Tooltip("The game object for the handle.")]
		public GameObject handle;

		// Token: 0x04001116 RID: 4374
		[Tooltip("The parent game object for the chest content elements.")]
		public GameObject content;

		// Token: 0x04001117 RID: 4375
		[Tooltip("Makes the content invisible while the chest is closed.")]
		public bool hideContent = true;

		// Token: 0x04001118 RID: 4376
		[Tooltip("The maximum opening angle of the chest.")]
		public float maxAngle = 160f;

		// Token: 0x04001119 RID: 4377
		protected float minAngle;

		// Token: 0x0400111A RID: 4378
		protected float stepSize = 1f;

		// Token: 0x0400111B RID: 4379
		protected Rigidbody bodyRigidbody;

		// Token: 0x0400111C RID: 4380
		protected Rigidbody handleRigidbody;

		// Token: 0x0400111D RID: 4381
		protected FixedJoint handleJoint;

		// Token: 0x0400111E RID: 4382
		protected Rigidbody lidRigidbody;

		// Token: 0x0400111F RID: 4383
		protected HingeJoint lidJoint;

		// Token: 0x04001120 RID: 4384
		protected bool lidJointCreated;

		// Token: 0x04001121 RID: 4385
		protected VRTK_Control.Direction finalDirection;

		// Token: 0x04001122 RID: 4386
		protected float subDirection = 1f;
	}
}
