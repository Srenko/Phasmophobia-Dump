using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x0200034A RID: 842
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_SpringJointGrabAttach")]
	public class VRTK_SpringJointGrabAttach : VRTK_BaseJointGrabAttach
	{
		// Token: 0x06001D70 RID: 7536 RVA: 0x000965DC File Offset: 0x000947DC
		protected override void CreateJoint(GameObject obj)
		{
			SpringJoint springJoint = obj.AddComponent<SpringJoint>();
			springJoint.breakForce = (this.grabbedObjectScript.IsDroppable() ? this.breakForce : float.PositiveInfinity);
			springJoint.spring = this.strength;
			springJoint.damper = this.damper;
			this.givenJoint = springJoint;
			base.CreateJoint(obj);
		}

		// Token: 0x0400173B RID: 5947
		[Tooltip("Maximum force the joint can withstand before breaking. Infinity means unbreakable.")]
		public float breakForce = 1500f;

		// Token: 0x0400173C RID: 5948
		[Tooltip("The strength of the spring.")]
		public float strength = 500f;

		// Token: 0x0400173D RID: 5949
		[Tooltip("The amount of dampening to apply to the spring.")]
		public float damper = 50f;
	}
}
