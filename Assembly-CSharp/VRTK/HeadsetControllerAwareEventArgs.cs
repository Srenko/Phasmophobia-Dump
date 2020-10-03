using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F4 RID: 756
	public struct HeadsetControllerAwareEventArgs
	{
		// Token: 0x04001548 RID: 5448
		public RaycastHit raycastHit;

		// Token: 0x04001549 RID: 5449
		[Obsolete("`HeadsetControllerAwareEventArgs.controllerIndex` has been replaced with `HeadsetControllerAwareEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x0400154A RID: 5450
		public VRTK_ControllerReference controllerReference;
	}
}
