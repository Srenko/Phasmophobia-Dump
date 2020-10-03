using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x0200028F RID: 655
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Door")]
	public class VRTK_Door : VRTK_Control
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (!base.enabled || !this.setupSuccessful)
			{
				return;
			}
			Bounds bounds = default(Bounds);
			Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.GetDoor().transform, this.GetDoor().transform, null);
			float num = 0.5f;
			if (this.handles)
			{
				bounds = VRTK_SharedMethods.GetBounds(this.handles.transform, this.handles.transform, null);
			}
			Vector3 a = Vector3.zero;
			Vector3 a2 = Vector3.zero;
			Vector3 thirdDirection = base.GetThirdDirection(this.Direction2Axis(this.finalDirection), this.secondaryDirection);
			bool flag = false;
			switch (this.finalDirection)
			{
			case VRTK_Control.Direction.x:
				if (thirdDirection == Vector3.up)
				{
					a = base.transform.up.normalized;
					a2 = base.transform.forward.normalized;
					num *= bounds2.extents.z;
				}
				else
				{
					a = base.transform.forward.normalized;
					a2 = base.transform.up.normalized;
					num *= bounds2.extents.y;
					flag = true;
				}
				break;
			case VRTK_Control.Direction.y:
				if (thirdDirection == Vector3.right)
				{
					a = base.transform.right.normalized;
					a2 = base.transform.forward.normalized;
					num *= bounds2.extents.z;
					flag = true;
				}
				else
				{
					a = base.transform.forward.normalized;
					a2 = base.transform.right.normalized;
					num *= bounds2.extents.x;
				}
				break;
			case VRTK_Control.Direction.z:
				if (thirdDirection == Vector3.up)
				{
					a = base.transform.up.normalized;
					a2 = base.transform.right.normalized;
					num *= bounds2.extents.x;
					flag = true;
				}
				else
				{
					a = base.transform.right.normalized;
					a2 = base.transform.up.normalized;
					num *= bounds2.extents.y;
				}
				break;
			}
			if ((!flag && this.openInward) || (flag && this.openOutward))
			{
				Vector3 vector = this.handles ? bounds.center : bounds2.center;
				Vector3 vector2 = vector + a2 * num * this.subDirection - a * (num / 2f) * this.subDirection;
				Gizmos.DrawLine(vector, vector2);
				Gizmos.DrawSphere(vector2, num / 8f);
			}
			if ((!flag && this.openOutward) || (flag && this.openInward))
			{
				Vector3 vector3 = this.handles ? bounds.center : bounds2.center;
				Vector3 vector4 = vector3 + a2 * num * this.subDirection + a * (num / 2f) * this.subDirection;
				Gizmos.DrawLine(vector3, vector4);
				Gizmos.DrawSphere(vector4, num / 8f);
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0006E642 File Offset: 0x0006C842
		protected override void InitRequiredComponents()
		{
			this.InitFrame();
			this.InitDoor();
			this.InitHandle();
			this.SetContent(this.content, this.hideContent);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0006E668 File Offset: 0x0006C868
		protected override bool DetectSetup()
		{
			this.doorHinge = this.GetDoor().GetComponent<HingeJoint>();
			if (this.doorHinge && !this.doorHingeCreated)
			{
				this.direction = VRTK_Control.Direction.autodetect;
			}
			this.finalDirection = ((this.direction == VRTK_Control.Direction.autodetect) ? this.DetectDirection() : this.direction);
			if (this.finalDirection == VRTK_Control.Direction.autodetect)
			{
				return false;
			}
			if (this.doorHinge && !this.doorHingeCreated)
			{
				this.direction = this.finalDirection;
			}
			Bounds bounds = VRTK_SharedMethods.GetBounds(this.GetDoor().transform, base.transform, null);
			if (this.doorHinge == null || this.doorHingeCreated)
			{
				if (this.handles)
				{
					Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.handles.transform, base.transform, null);
					switch (this.finalDirection)
					{
					case VRTK_Control.Direction.x:
						if (bounds2.center.z + bounds2.extents.z > bounds.center.z + bounds.extents.z || bounds2.center.z - bounds2.extents.z < bounds.center.z - bounds.extents.z)
						{
							this.subDirection = (float)((bounds2.center.y > bounds.center.y) ? -1 : 1);
							this.secondaryDirection = Vector3.up;
						}
						else
						{
							this.subDirection = (float)((bounds2.center.z > bounds.center.z) ? -1 : 1);
							this.secondaryDirection = Vector3.forward;
						}
						break;
					case VRTK_Control.Direction.y:
						if (bounds2.center.z + bounds2.extents.z > bounds.center.z + bounds.extents.z || bounds2.center.z - bounds2.extents.z < bounds.center.z - bounds.extents.z)
						{
							this.subDirection = (float)((bounds2.center.x > bounds.center.x) ? -1 : 1);
							this.secondaryDirection = Vector3.right;
						}
						else
						{
							this.subDirection = (float)((bounds2.center.z > bounds.center.z) ? -1 : 1);
							this.secondaryDirection = Vector3.forward;
						}
						break;
					case VRTK_Control.Direction.z:
						if (bounds2.center.x + bounds2.extents.x > bounds.center.x + bounds.extents.x || bounds2.center.x - bounds2.extents.x < bounds.center.x - bounds.extents.x)
						{
							this.subDirection = (float)((bounds2.center.y > bounds.center.y) ? -1 : 1);
							this.secondaryDirection = Vector3.up;
						}
						else
						{
							this.subDirection = (float)((bounds2.center.x > bounds.center.x) ? -1 : 1);
							this.secondaryDirection = Vector3.right;
						}
						break;
					}
				}
				else
				{
					switch (this.finalDirection)
					{
					case VRTK_Control.Direction.x:
						this.secondaryDirection = ((bounds.extents.y > bounds.extents.z) ? Vector3.up : Vector3.forward);
						break;
					case VRTK_Control.Direction.y:
						this.secondaryDirection = ((bounds.extents.x > bounds.extents.z) ? Vector3.right : Vector3.forward);
						break;
					case VRTK_Control.Direction.z:
						this.secondaryDirection = ((bounds.extents.y > bounds.extents.x) ? Vector3.up : Vector3.right);
						break;
					}
					this.subDirection = 1f;
				}
			}
			else
			{
				Vector3 vector = bounds.center - this.doorHinge.connectedAnchor;
				if (vector.x != 0f)
				{
					this.secondaryDirection = Vector3.right;
					this.subDirection = (float)((vector.x <= 0f) ? 1 : -1);
				}
				else if (vector.y != 0f)
				{
					this.secondaryDirection = Vector3.up;
					this.subDirection = (float)((vector.y <= 0f) ? 1 : -1);
				}
				else if (vector.z != 0f)
				{
					this.secondaryDirection = Vector3.forward;
					this.subDirection = (float)((vector.z <= 0f) ? 1 : -1);
				}
			}
			if (this.doorHingeCreated)
			{
				float d;
				if (this.secondaryDirection == Vector3.right)
				{
					d = bounds.extents.x / this.GetDoor().transform.lossyScale.x;
				}
				else if (this.secondaryDirection == Vector3.up)
				{
					d = bounds.extents.y / this.GetDoor().transform.lossyScale.y;
				}
				else
				{
					d = bounds.extents.z / this.GetDoor().transform.lossyScale.z;
				}
				this.doorHinge.anchor = this.secondaryDirection * this.subDirection * d;
				this.doorHinge.axis = this.Direction2Axis(this.finalDirection);
			}
			if (this.doorHinge)
			{
				this.doorHinge.useLimits = true;
				this.doorHinge.enableCollision = true;
				JointLimits limits = this.doorHinge.limits;
				limits.min = (this.openInward ? (-this.maxAngle) : 0f);
				limits.max = (this.openOutward ? this.maxAngle : 0f);
				limits.bounciness = 0f;
				this.doorHinge.limits = limits;
			}
			if (this.doorSnapForceCreated)
			{
				float num = -5f * this.GetDirectionFromJoint();
				this.doorSnapForce.relativeForce = base.GetThirdDirection(this.doorHinge.axis, this.secondaryDirection) * (this.subDirection * num);
			}
			return true;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = this.doorHinge.limits.min,
				controlMax = this.doorHinge.limits.max
			};
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0006ED34 File Offset: 0x0006CF34
		protected override void HandleUpdate()
		{
			this.value = this.CalculateValue();
			this.doorSnapForce.enabled = ((this.openOutward ^ this.openInward) && Mathf.Abs(this.value) < this.minSnapClose * 100f);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006ED84 File Offset: 0x0006CF84
		protected virtual float GetDirectionFromJoint()
		{
			if (this.doorHinge.limits.min >= 0f)
			{
				return 1f;
			}
			return -1f;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006EDB8 File Offset: 0x0006CFB8
		protected virtual Vector3 Direction2Axis(VRTK_Control.Direction givenDirection)
		{
			Vector3 zero = Vector3.zero;
			switch (givenDirection)
			{
			case VRTK_Control.Direction.x:
				zero = new Vector3(1f, 0f, 0f);
				break;
			case VRTK_Control.Direction.y:
				zero = new Vector3(0f, 1f, 0f);
				break;
			case VRTK_Control.Direction.z:
				zero = new Vector3(0f, 0f, 1f);
				break;
			}
			return zero;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006EE28 File Offset: 0x0006D028
		protected virtual VRTK_Control.Direction DetectDirection()
		{
			VRTK_Control.Direction result = VRTK_Control.Direction.autodetect;
			if (this.doorHinge && !this.doorHingeCreated)
			{
				if (this.doorHinge.axis == Vector3.right)
				{
					result = VRTK_Control.Direction.x;
				}
				else if (this.doorHinge.axis == Vector3.up)
				{
					result = VRTK_Control.Direction.y;
				}
				else if (this.doorHinge.axis == Vector3.forward)
				{
					result = VRTK_Control.Direction.z;
				}
			}
			else if (this.handles)
			{
				Bounds bounds = VRTK_SharedMethods.GetBounds(this.handles.transform, base.transform, null);
				Bounds bounds2 = VRTK_SharedMethods.GetBounds(this.GetDoor().transform, base.transform, this.handles.transform);
				if (bounds.center.y + bounds.extents.y > bounds2.center.y + bounds2.extents.y || bounds.center.y - bounds.extents.y < bounds2.center.y - bounds2.extents.y)
				{
					result = VRTK_Control.Direction.x;
				}
				else
				{
					result = VRTK_Control.Direction.y;
				}
			}
			return result;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006EF64 File Offset: 0x0006D164
		protected virtual void InitFrame()
		{
			if (this.frame == null)
			{
				return;
			}
			this.frameRigidbody = this.frame.GetComponent<Rigidbody>();
			if (this.frameRigidbody == null)
			{
				this.frameRigidbody = this.frame.AddComponent<Rigidbody>();
				this.frameRigidbody.isKinematic = true;
				this.frameRigidbody.angularDrag = this.releasedFriction;
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0006EFD0 File Offset: 0x0006D1D0
		protected virtual void InitDoor()
		{
			GameObject gameObject = this.GetDoor();
			VRTK_SharedMethods.CreateColliders(gameObject);
			this.doorRigidbody = gameObject.GetComponent<Rigidbody>();
			if (this.doorRigidbody == null)
			{
				this.doorRigidbody = gameObject.AddComponent<Rigidbody>();
				this.doorRigidbody.angularDrag = this.releasedFriction;
			}
			this.doorRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			this.doorRigidbody.isKinematic = false;
			this.doorHinge = gameObject.GetComponent<HingeJoint>();
			if (this.doorHinge == null)
			{
				this.doorHinge = gameObject.AddComponent<HingeJoint>();
				this.doorHingeCreated = true;
			}
			this.doorHinge.connectedBody = this.frameRigidbody;
			this.doorSnapForce = gameObject.GetComponent<ConstantForce>();
			if (this.doorSnapForce == null)
			{
				this.doorSnapForce = gameObject.AddComponent<ConstantForce>();
				this.doorSnapForce.enabled = false;
				this.doorSnapForceCreated = true;
			}
			if (!this.handleInteractableOnly)
			{
				this.CreateInteractableObject(gameObject);
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0006F0C0 File Offset: 0x0006D2C0
		protected virtual void InitHandle()
		{
			if (this.handles == null)
			{
				return;
			}
			if (this.handles.GetComponentInChildren<Collider>() == null)
			{
				VRTK_SharedMethods.CreateColliders(this.handles);
			}
			Rigidbody rigidbody = this.handles.GetComponent<Rigidbody>();
			if (rigidbody == null)
			{
				rigidbody = this.handles.AddComponent<Rigidbody>();
			}
			rigidbody.isKinematic = false;
			rigidbody.useGravity = false;
			if (this.handles.GetComponent<FixedJoint>() == null)
			{
				this.handles.AddComponent<FixedJoint>().connectedBody = this.doorRigidbody;
			}
			if (this.handleInteractableOnly)
			{
				this.CreateInteractableObject(this.handles);
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006F168 File Offset: 0x0006D368
		protected virtual void CreateInteractableObject(GameObject target)
		{
			VRTK_InteractableObject vrtk_InteractableObject = target.GetComponent<VRTK_InteractableObject>();
			if (vrtk_InteractableObject == null)
			{
				vrtk_InteractableObject = target.AddComponent<VRTK_InteractableObject>();
			}
			vrtk_InteractableObject.isGrabbable = true;
			vrtk_InteractableObject.grabAttachMechanicScript = target.AddComponent<VRTK_RotatorTrackGrabAttach>();
			vrtk_InteractableObject.grabAttachMechanicScript.precisionGrab = true;
			vrtk_InteractableObject.secondaryGrabActionScript = target.AddComponent<VRTK_SwapControllerGrabAction>();
			vrtk_InteractableObject.stayGrabbedOnTeleport = false;
			vrtk_InteractableObject.InteractableObjectGrabbed += this.InteractableObjectGrabbed;
			vrtk_InteractableObject.InteractableObjectUngrabbed += this.InteractableObjectUngrabbed;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006F1E4 File Offset: 0x0006D3E4
		protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.doorRigidbody.angularDrag = this.grabbedFriction;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0006F1F7 File Offset: 0x0006D3F7
		protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.doorRigidbody.angularDrag = this.releasedFriction;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006F20A File Offset: 0x0006D40A
		protected virtual float CalculateValue()
		{
			return Mathf.Round(this.doorHinge.angle / this.stepSize) * this.stepSize;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006F22A File Offset: 0x0006D42A
		protected virtual GameObject GetDoor()
		{
			if (!this.door)
			{
				return base.gameObject;
			}
			return this.door;
		}

		// Token: 0x04001132 RID: 4402
		[Tooltip("The axis on which the door should open.")]
		public VRTK_Control.Direction direction;

		// Token: 0x04001133 RID: 4403
		[Tooltip("The game object for the door. Can also be an empty parent or left empty if the script is put onto the actual door mesh. If no colliders exist yet a collider will tried to be automatically attached to all children that expose renderers.")]
		public GameObject door;

		// Token: 0x04001134 RID: 4404
		[Tooltip("The game object for the handles. Can also be an empty parent or left empty. If empty the door can only be moved using the rigidbody mode of the controller. If no collider exists yet a compound collider made up of all children will try to be calculated but this will fail if the door is rotated. In that case a manual collider will need to be assigned.")]
		public GameObject handles;

		// Token: 0x04001135 RID: 4405
		[Tooltip("The game object for the frame to which the door is attached. Should only be set if the frame will move as well to ensure that the door moves along with the frame.")]
		public GameObject frame;

		// Token: 0x04001136 RID: 4406
		[Tooltip("The parent game object for the door content elements.")]
		public GameObject content;

		// Token: 0x04001137 RID: 4407
		[Tooltip("Makes the content invisible while the door is closed.")]
		public bool hideContent = true;

		// Token: 0x04001138 RID: 4408
		[Tooltip("The maximum opening angle of the door.")]
		public float maxAngle = 120f;

		// Token: 0x04001139 RID: 4409
		[Tooltip("Can the door be pulled to open.")]
		public bool openInward;

		// Token: 0x0400113A RID: 4410
		[Tooltip("Can the door be pushed to open.")]
		public bool openOutward = true;

		// Token: 0x0400113B RID: 4411
		[Tooltip("The range at which the door must be to being closed before it snaps shut. Only works if either inward or outward is selected, not both.")]
		[Range(0f, 1f)]
		public float minSnapClose = 1f;

		// Token: 0x0400113C RID: 4412
		[Tooltip("The amount of friction the door will have whilst swinging when it is not grabbed.")]
		public float releasedFriction = 10f;

		// Token: 0x0400113D RID: 4413
		[Tooltip("The amount of friction the door will have whilst swinging when it is grabbed.")]
		public float grabbedFriction = 100f;

		// Token: 0x0400113E RID: 4414
		[Tooltip("If this is checked then only the door handle is grabbale to operate the door.")]
		public bool handleInteractableOnly;

		// Token: 0x0400113F RID: 4415
		protected float stepSize = 1f;

		// Token: 0x04001140 RID: 4416
		protected Rigidbody doorRigidbody;

		// Token: 0x04001141 RID: 4417
		protected HingeJoint doorHinge;

		// Token: 0x04001142 RID: 4418
		protected ConstantForce doorSnapForce;

		// Token: 0x04001143 RID: 4419
		protected Rigidbody frameRigidbody;

		// Token: 0x04001144 RID: 4420
		protected VRTK_Control.Direction finalDirection;

		// Token: 0x04001145 RID: 4421
		protected float subDirection = 1f;

		// Token: 0x04001146 RID: 4422
		protected Vector3 secondaryDirection;

		// Token: 0x04001147 RID: 4423
		protected bool doorHingeCreated;

		// Token: 0x04001148 RID: 4424
		protected bool doorSnapForceCreated;
	}
}
