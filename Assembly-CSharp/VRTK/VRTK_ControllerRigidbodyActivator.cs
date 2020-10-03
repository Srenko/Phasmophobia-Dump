using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000248 RID: 584
	public class VRTK_ControllerRigidbodyActivator : MonoBehaviour
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060010E7 RID: 4327 RVA: 0x0006395C File Offset: 0x00061B5C
		// (remove) Token: 0x060010E8 RID: 4328 RVA: 0x00063994 File Offset: 0x00061B94
		public event ControllerRigidbodyActivatorEventHandler ControllerRigidbodyOn;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060010E9 RID: 4329 RVA: 0x000639CC File Offset: 0x00061BCC
		// (remove) Token: 0x060010EA RID: 4330 RVA: 0x00063A04 File Offset: 0x00061C04
		public event ControllerRigidbodyActivatorEventHandler ControllerRigidbodyOff;

		// Token: 0x060010EB RID: 4331 RVA: 0x00063A39 File Offset: 0x00061C39
		public virtual void OnControllerRigidbodyOn(ControllerRigidbodyActivatorEventArgs e)
		{
			if (this.ControllerRigidbodyOn != null)
			{
				this.ControllerRigidbodyOn(this, e);
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00063A50 File Offset: 0x00061C50
		public virtual void OnControllerRigidbodyOff(ControllerRigidbodyActivatorEventArgs e)
		{
			if (this.ControllerRigidbodyOff != null)
			{
				this.ControllerRigidbodyOff(this, e);
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00063A67 File Offset: 0x00061C67
		protected virtual void OnTriggerEnter(Collider collider)
		{
			this.ToggleRigidbody(collider, true);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00063A71 File Offset: 0x00061C71
		protected virtual void OnTriggerExit(Collider collider)
		{
			this.ToggleRigidbody(collider, false);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00063A7C File Offset: 0x00061C7C
		protected virtual void ToggleRigidbody(Collider collider, bool state)
		{
			VRTK_InteractTouch componentInParent = collider.GetComponentInParent<VRTK_InteractTouch>();
			if (componentInParent != null && (this.isEnabled || !state))
			{
				componentInParent.ToggleControllerRigidBody(state, state);
				this.EmitEvent(state, componentInParent);
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00063AB4 File Offset: 0x00061CB4
		protected virtual void EmitEvent(bool state, VRTK_InteractTouch touch)
		{
			ControllerRigidbodyActivatorEventArgs e;
			e.touchingObject = touch;
			if (state)
			{
				this.OnControllerRigidbodyOn(e);
				return;
			}
			this.OnControllerRigidbodyOff(e);
		}

		// Token: 0x04000FD4 RID: 4052
		[Tooltip("If this is checked then the collider will have it's rigidbody toggled on and off during a collision.")]
		public bool isEnabled = true;
	}
}
