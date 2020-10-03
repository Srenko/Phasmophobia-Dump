using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK
{
	// Token: 0x0200028A RID: 650
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_Button")]
	public class VRTK_Button : VRTK_Control
	{
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060013CC RID: 5068 RVA: 0x0006C7B0 File Offset: 0x0006A9B0
		// (remove) Token: 0x060013CD RID: 5069 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
		public event Button3DEventHandler Pushed;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060013CE RID: 5070 RVA: 0x0006C820 File Offset: 0x0006AA20
		// (remove) Token: 0x060013CF RID: 5071 RVA: 0x0006C858 File Offset: 0x0006AA58
		public event Button3DEventHandler Released;

		// Token: 0x060013D0 RID: 5072 RVA: 0x0006C88D File Offset: 0x0006AA8D
		public virtual void OnPushed(Control3DEventArgs e)
		{
			if (this.Pushed != null)
			{
				this.Pushed(this, e);
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0006C8A4 File Offset: 0x0006AAA4
		public virtual void OnReleased(Control3DEventArgs e)
		{
			if (this.Released != null)
			{
				this.Released(this, e);
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0006C8BB File Offset: 0x0006AABB
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (!base.enabled || !this.setupSuccessful)
			{
				return;
			}
			Gizmos.DrawLine(this.bounds.center, this.bounds.center + this.activationDir);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0006C8FC File Offset: 0x0006AAFC
		protected override void InitRequiredComponents()
		{
			this.restingPosition = base.transform.position;
			if (!base.GetComponent<Collider>())
			{
				base.gameObject.AddComponent<BoxCollider>();
			}
			this.buttonRigidbody = base.GetComponent<Rigidbody>();
			if (this.buttonRigidbody == null)
			{
				this.buttonRigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			this.buttonRigidbody.isKinematic = false;
			this.buttonRigidbody.useGravity = false;
			this.buttonForce = base.GetComponent<ConstantForce>();
			if (this.buttonForce == null)
			{
				this.buttonForce = base.gameObject.AddComponent<ConstantForce>();
			}
			if (this.connectedTo)
			{
				Rigidbody rigidbody = this.connectedTo.GetComponent<Rigidbody>();
				if (rigidbody == null)
				{
					rigidbody = this.connectedTo.AddComponent<Rigidbody>();
				}
				rigidbody.useGravity = false;
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		protected override bool DetectSetup()
		{
			this.finalDirection = ((this.direction == VRTK_Button.ButtonDirection.autodetect) ? this.DetectDirection() : this.direction);
			if (this.finalDirection == VRTK_Button.ButtonDirection.autodetect)
			{
				this.activationDir = Vector3.zero;
				return false;
			}
			if (this.direction != VRTK_Button.ButtonDirection.autodetect)
			{
				this.activationDir = this.CalculateActivationDir();
			}
			if (this.buttonForce)
			{
				this.buttonForce.force = this.GetForceVector();
			}
			if (Application.isPlaying)
			{
				this.buttonJoint = base.GetComponent<ConfigurableJoint>();
				bool flag = false;
				Rigidbody connectedBody = null;
				Vector3 anchor = Vector3.zero;
				Vector3 axis = Vector3.zero;
				if (this.buttonJoint)
				{
					connectedBody = this.buttonJoint.connectedBody;
					anchor = this.buttonJoint.anchor;
					axis = this.buttonJoint.axis;
					Object.DestroyImmediate(this.buttonJoint);
					flag = true;
				}
				base.transform.position = base.transform.position + this.activationDir.normalized * this.activationDistance * 0.5f;
				this.buttonJoint = base.gameObject.AddComponent<ConfigurableJoint>();
				if (flag)
				{
					this.buttonJoint.connectedBody = connectedBody;
					this.buttonJoint.anchor = anchor;
					this.buttonJoint.axis = axis;
				}
				if (this.connectedTo)
				{
					this.buttonJoint.connectedBody = this.connectedTo.GetComponent<Rigidbody>();
				}
				SoftJointLimit linearLimit = default(SoftJointLimit);
				linearLimit.limit = this.activationDistance * 0.501f;
				this.buttonJoint.linearLimit = linearLimit;
				this.buttonJoint.angularXMotion = ConfigurableJointMotion.Locked;
				this.buttonJoint.angularYMotion = ConfigurableJointMotion.Locked;
				this.buttonJoint.angularZMotion = ConfigurableJointMotion.Locked;
				this.buttonJoint.xMotion = ConfigurableJointMotion.Locked;
				this.buttonJoint.yMotion = ConfigurableJointMotion.Locked;
				this.buttonJoint.zMotion = ConfigurableJointMotion.Locked;
				switch (this.finalDirection)
				{
				case VRTK_Button.ButtonDirection.x:
				case VRTK_Button.ButtonDirection.negX:
					if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.x)) == 1)
					{
						this.buttonJoint.xMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.x)) == 1)
					{
						this.buttonJoint.yMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.x)) == 1)
					{
						this.buttonJoint.zMotion = ConfigurableJointMotion.Limited;
					}
					break;
				case VRTK_Button.ButtonDirection.y:
				case VRTK_Button.ButtonDirection.negY:
					if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.y)) == 1)
					{
						this.buttonJoint.xMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.y)) == 1)
					{
						this.buttonJoint.yMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.y)) == 1)
					{
						this.buttonJoint.zMotion = ConfigurableJointMotion.Limited;
					}
					break;
				case VRTK_Button.ButtonDirection.z:
				case VRTK_Button.ButtonDirection.negZ:
					if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.z)) == 1)
					{
						this.buttonJoint.xMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.z)) == 1)
					{
						this.buttonJoint.yMotion = ConfigurableJointMotion.Limited;
					}
					else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.z)) == 1)
					{
						this.buttonJoint.zMotion = ConfigurableJointMotion.Limited;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0006CD78 File Offset: 0x0006AF78
		protected override VRTK_Control.ControlValueRange RegisterValueRange()
		{
			return new VRTK_Control.ControlValueRange
			{
				controlMin = 0f,
				controlMax = 1f
			};
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0006CDA8 File Offset: 0x0006AFA8
		protected override void HandleUpdate()
		{
			float value = this.value;
			if (this.ReachedActivationDistance())
			{
				if (value == 0f)
				{
					this.value = 1f;
					this.events.OnPush.Invoke();
					this.OnPushed(this.SetControlEvent());
					return;
				}
			}
			else if (value == 1f)
			{
				this.value = 0f;
				this.OnReleased(this.SetControlEvent());
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0006CE13 File Offset: 0x0006B013
		protected virtual void FixedUpdate()
		{
			if (this.forceCount == 0 && this.buttonJoint.connectedBody)
			{
				this.restingPosition = base.transform.position;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0006CE40 File Offset: 0x0006B040
		protected virtual void OnCollisionExit(Collision collision)
		{
			this.forceCount--;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0006CE50 File Offset: 0x0006B050
		protected virtual void OnCollisionEnter(Collision collision)
		{
			this.forceCount++;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0006CE60 File Offset: 0x0006B060
		protected virtual VRTK_Button.ButtonDirection DetectDirection()
		{
			VRTK_Button.ButtonDirection buttonDirection = VRTK_Button.ButtonDirection.autodetect;
			Bounds bounds = VRTK_SharedMethods.GetBounds(base.transform, null, null);
			RaycastHit raycastHit;
			Physics.Raycast(bounds.center, Vector3.forward, out raycastHit, bounds.extents.z * 4f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit2;
			Physics.Raycast(bounds.center, Vector3.back, out raycastHit2, bounds.extents.z * 4f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit3;
			Physics.Raycast(bounds.center, Vector3.left, out raycastHit3, bounds.extents.x * 4f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit4;
			Physics.Raycast(bounds.center, Vector3.right, out raycastHit4, bounds.extents.x * 4f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit5;
			Physics.Raycast(bounds.center, Vector3.up, out raycastHit5, bounds.extents.y * 4f, -5, QueryTriggerInteraction.UseGlobal);
			RaycastHit raycastHit6;
			Physics.Raycast(bounds.center, Vector3.down, out raycastHit6, bounds.extents.y * 4f, -5, QueryTriggerInteraction.UseGlobal);
			float num = (raycastHit4.collider != null) ? raycastHit4.distance : float.MaxValue;
			float num2 = (raycastHit6.collider != null) ? raycastHit6.distance : float.MaxValue;
			float num3 = (raycastHit2.collider != null) ? raycastHit2.distance : float.MaxValue;
			float num4 = (raycastHit3.collider != null) ? raycastHit3.distance : float.MaxValue;
			float num5 = (raycastHit5.collider != null) ? raycastHit5.distance : float.MaxValue;
			float num6 = (raycastHit.collider != null) ? raycastHit.distance : float.MaxValue;
			float num7 = 0f;
			Vector3 a = Vector3.zero;
			if (VRTK_SharedMethods.IsLowest(num, new float[]
			{
				num2,
				num3,
				num4,
				num5,
				num6
			}))
			{
				buttonDirection = VRTK_Button.ButtonDirection.negX;
				a = raycastHit4.point;
				num7 = bounds.extents.x;
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
				buttonDirection = VRTK_Button.ButtonDirection.y;
				a = raycastHit6.point;
				num7 = bounds.extents.y;
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
				buttonDirection = VRTK_Button.ButtonDirection.z;
				a = raycastHit2.point;
				num7 = bounds.extents.z;
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
				buttonDirection = VRTK_Button.ButtonDirection.x;
				a = raycastHit3.point;
				num7 = bounds.extents.x;
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
				buttonDirection = VRTK_Button.ButtonDirection.negY;
				a = raycastHit5.point;
				num7 = bounds.extents.y;
			}
			else if (VRTK_SharedMethods.IsLowest(num6, new float[]
			{
				num,
				num2,
				num3,
				num4,
				num5
			}))
			{
				buttonDirection = VRTK_Button.ButtonDirection.negZ;
				a = raycastHit.point;
				num7 = bounds.extents.z;
			}
			this.activationDistance = (Vector3.Distance(a, bounds.center) - num7) * 0.95f;
			if (buttonDirection == VRTK_Button.ButtonDirection.autodetect || this.activationDistance < 0.001f)
			{
				buttonDirection = VRTK_Button.ButtonDirection.autodetect;
				this.activationDistance = 0f;
			}
			else
			{
				this.activationDir = a - bounds.center;
			}
			return buttonDirection;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0006D228 File Offset: 0x0006B428
		protected virtual Vector3 CalculateActivationDir()
		{
			Bounds bounds = VRTK_SharedMethods.GetBounds(base.transform, base.transform, null);
			Vector3 a = Vector3.zero;
			float num = 0f;
			switch (this.direction)
			{
			case VRTK_Button.ButtonDirection.x:
			case VRTK_Button.ButtonDirection.negX:
				if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.x)) == 1)
				{
					a = base.transform.right;
					num = bounds.extents.x;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.x)) == 1)
				{
					a = base.transform.up;
					num = bounds.extents.y;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.x)) == 1)
				{
					a = base.transform.forward;
					num = bounds.extents.z;
				}
				a *= (float)((this.direction == VRTK_Button.ButtonDirection.x) ? -1 : 1);
				break;
			case VRTK_Button.ButtonDirection.y:
			case VRTK_Button.ButtonDirection.negY:
				if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.y)) == 1)
				{
					a = base.transform.right;
					num = bounds.extents.x;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.y)) == 1)
				{
					a = base.transform.up;
					num = bounds.extents.y;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.y)) == 1)
				{
					a = base.transform.forward;
					num = bounds.extents.z;
				}
				a *= (float)((this.direction == VRTK_Button.ButtonDirection.y) ? -1 : 1);
				break;
			case VRTK_Button.ButtonDirection.z:
			case VRTK_Button.ButtonDirection.negZ:
				if (Mathf.RoundToInt(Mathf.Abs(base.transform.right.z)) == 1)
				{
					a = base.transform.right;
					num = bounds.extents.x;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.up.z)) == 1)
				{
					a = base.transform.up;
					num = bounds.extents.y;
				}
				else if (Mathf.RoundToInt(Mathf.Abs(base.transform.forward.z)) == 1)
				{
					a = base.transform.forward;
					num = bounds.extents.z;
				}
				a *= (float)((this.direction == VRTK_Button.ButtonDirection.z) ? -1 : 1);
				break;
			}
			return a * (num + this.activationDistance);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0006D4C9 File Offset: 0x0006B6C9
		protected virtual bool ReachedActivationDistance()
		{
			return Vector3.Distance(base.transform.position, this.restingPosition) >= this.activationDistance;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0006D4EC File Offset: 0x0006B6EC
		protected virtual Vector3 GetForceVector()
		{
			return -this.activationDir.normalized * this.buttonStrength;
		}

		// Token: 0x04001103 RID: 4355
		[Tooltip("An optional game object to which the button will be connected. If the game object moves the button will follow along.")]
		public GameObject connectedTo;

		// Token: 0x04001104 RID: 4356
		[Tooltip("The axis on which the button should move. All other axis will be frozen.")]
		public VRTK_Button.ButtonDirection direction;

		// Token: 0x04001105 RID: 4357
		[Tooltip("The local distance the button needs to be pushed until a push event is triggered.")]
		public float activationDistance = 1f;

		// Token: 0x04001106 RID: 4358
		[Tooltip("The amount of force needed to push the button down as well as the speed with which it will go back into its original position.")]
		public float buttonStrength = 5f;

		// Token: 0x04001107 RID: 4359
		[Tooltip("The events specific to the button control. This parameter is deprecated and will be removed in a future version of VRTK.")]
		[Obsolete("`VRTK_Control.events` has been replaced with delegate events. `VRTK_Button_UnityEvents` is now required to access Unity events. This method will be removed in a future version of VRTK.")]
		public VRTK_Button.ButtonEvents events;

		// Token: 0x0400110A RID: 4362
		protected const float MAX_AUTODETECT_ACTIVATION_LENGTH = 4f;

		// Token: 0x0400110B RID: 4363
		protected VRTK_Button.ButtonDirection finalDirection;

		// Token: 0x0400110C RID: 4364
		protected Vector3 restingPosition;

		// Token: 0x0400110D RID: 4365
		protected Vector3 activationDir;

		// Token: 0x0400110E RID: 4366
		protected Rigidbody buttonRigidbody;

		// Token: 0x0400110F RID: 4367
		protected ConfigurableJoint buttonJoint;

		// Token: 0x04001110 RID: 4368
		protected ConstantForce buttonForce;

		// Token: 0x04001111 RID: 4369
		protected int forceCount;

		// Token: 0x020005C6 RID: 1478
		[Obsolete("`VRTK_Control.ButtonEvents` has been replaced with delegate events. `VRTK_Button_UnityEvents` is now required to access Unity events. This method will be removed in a future version of VRTK.")]
		[Serializable]
		public class ButtonEvents
		{
			// Token: 0x04002741 RID: 10049
			public UnityEvent OnPush;
		}

		// Token: 0x020005C7 RID: 1479
		public enum ButtonDirection
		{
			// Token: 0x04002743 RID: 10051
			autodetect,
			// Token: 0x04002744 RID: 10052
			x,
			// Token: 0x04002745 RID: 10053
			y,
			// Token: 0x04002746 RID: 10054
			z,
			// Token: 0x04002747 RID: 10055
			negX,
			// Token: 0x04002748 RID: 10056
			negY,
			// Token: 0x04002749 RID: 10057
			negZ
		}
	}
}
