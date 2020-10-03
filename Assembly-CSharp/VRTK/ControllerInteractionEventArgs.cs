using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000299 RID: 665
	public struct ControllerInteractionEventArgs
	{
		// Token: 0x040011A7 RID: 4519
		[Obsolete("`ControllerInteractionEventArgs.controllerIndex` has been replaced with `ControllerInteractionEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x040011A8 RID: 4520
		public VRTK_ControllerReference controllerReference;

		// Token: 0x040011A9 RID: 4521
		public float buttonPressure;

		// Token: 0x040011AA RID: 4522
		public Vector2 touchpadAxis;

		// Token: 0x040011AB RID: 4523
		public float touchpadAngle;
	}
}
