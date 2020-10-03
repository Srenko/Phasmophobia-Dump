using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000294 RID: 660
	[AddComponentMenu("VRTK/Scripts/Controls/3D/VRTK_SpringLever")]
	public class VRTK_SpringLever : VRTK_Lever
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x00071368 File Offset: 0x0006F568
		protected override void InitRequiredComponents()
		{
			base.InitRequiredComponents();
			if (!this.leverHingeJoint.useSpring)
			{
				this.leverHingeJoint.useSpring = true;
				JointSpring spring = this.leverHingeJoint.spring;
				spring.spring = this.springStrength;
				spring.damper = this.springDamper;
				spring.targetPosition = this.minAngle;
				this.leverHingeJoint.spring = spring;
				return;
			}
			this.springStrength = this.leverHingeJoint.spring.spring;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000713EA File Offset: 0x0006F5EA
		protected override void HandleUpdate()
		{
			base.HandleUpdate();
			this.ApplySpringForce();
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000713F8 File Offset: 0x0006F5F8
		protected override void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
		{
			base.InteractableObjectGrabbed(sender, e);
			this.isGrabbed = true;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00071409 File Offset: 0x0006F609
		protected override void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			base.InteractableObjectUngrabbed(sender, e);
			this.isGrabbed = false;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0007141A File Offset: 0x0006F61A
		protected virtual float GetSpringTarget(bool towardZero)
		{
			if (!towardZero)
			{
				return this.maxAngle;
			}
			return this.minAngle;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0007142C File Offset: 0x0006F62C
		protected virtual void ApplySpringForce()
		{
			this.leverHingeJoint.useSpring = (this.alwaysActive || !this.isGrabbed);
			if (this.leverHingeJoint.useSpring)
			{
				bool flag = !this.snapToNearestLimit || this.GetNormalizedValue() <= 50f;
				if (flag != this.wasTowardZero)
				{
					JointSpring spring = this.leverHingeJoint.spring;
					spring.targetPosition = this.GetSpringTarget(flag);
					this.leverHingeJoint.spring = spring;
					this.wasTowardZero = flag;
				}
			}
		}

		// Token: 0x04001184 RID: 4484
		[Tooltip("The strength of the spring force that will be applied upon the lever.")]
		public float springStrength = 10f;

		// Token: 0x04001185 RID: 4485
		[Tooltip("The damper of the spring force that will be applied upon the lever.")]
		public float springDamper = 10f;

		// Token: 0x04001186 RID: 4486
		[Tooltip("If this is checked then the spring will snap the lever to the nearest end point (either min or max angle). If it is unchecked, the lever will always snap to the min angle position.")]
		public bool snapToNearestLimit;

		// Token: 0x04001187 RID: 4487
		[Tooltip("If this is checked then the spring will always be active even when grabbing the lever.")]
		public bool alwaysActive;

		// Token: 0x04001188 RID: 4488
		protected bool wasTowardZero = true;

		// Token: 0x04001189 RID: 4489
		protected bool isGrabbed;
	}
}
