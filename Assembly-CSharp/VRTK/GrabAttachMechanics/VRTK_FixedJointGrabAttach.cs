using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000348 RID: 840
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_FixedJointGrabAttach")]
	public class VRTK_FixedJointGrabAttach : VRTK_BaseJointGrabAttach
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x00096529 File Offset: 0x00094729
		protected override void CreateJoint(GameObject obj)
		{
			this.givenJoint = obj.AddComponent<FixedJoint>();
			this.givenJoint.breakForce = float.PositiveInfinity;
			base.CreateJoint(obj);
		}

		// Token: 0x0400173A RID: 5946
		[Tooltip("Maximum force the joint can withstand before breaking. Infinity means unbreakable.")]
		[HideInInspector]
		public float breakForce = float.PositiveInfinity;
	}
}
