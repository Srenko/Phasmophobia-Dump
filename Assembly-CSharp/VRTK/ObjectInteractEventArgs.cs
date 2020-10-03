using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002A5 RID: 677
	public struct ObjectInteractEventArgs
	{
		// Token: 0x0400126B RID: 4715
		[Obsolete("`ObjectInteractEventArgs.controllerIndex` has been replaced with `ObjectInteractEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x0400126C RID: 4716
		public VRTK_ControllerReference controllerReference;

		// Token: 0x0400126D RID: 4717
		public GameObject target;
	}
}
