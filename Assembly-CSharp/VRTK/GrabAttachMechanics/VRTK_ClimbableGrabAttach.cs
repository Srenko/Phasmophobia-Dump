using System;
using UnityEngine;

namespace VRTK.GrabAttachMechanics
{
	// Token: 0x02000346 RID: 838
	[AddComponentMenu("VRTK/Scripts/Interactions/Grab Attach Mechanics/VRTK_ClimbableGrabAttach")]
	public class VRTK_ClimbableGrabAttach : VRTK_BaseGrabAttach
	{
		// Token: 0x06001D63 RID: 7523 RVA: 0x000963AF File Offset: 0x000945AF
		protected override void Initialise()
		{
			this.tracked = false;
			this.climbable = true;
			this.kinematic = true;
		}

		// Token: 0x04001737 RID: 5943
		[Tooltip("Will respect the grabbed climbing object's rotation if it changes dynamically")]
		public bool useObjectRotation;
	}
}
