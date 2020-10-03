using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D3 RID: 723
	public struct PlayerClimbEventArgs
	{
		// Token: 0x0400139E RID: 5022
		[Obsolete("`PlayerClimbEventArgs.controllerIndex` has been replaced with `PlayerClimbEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x0400139F RID: 5023
		public VRTK_ControllerReference controllerReference;

		// Token: 0x040013A0 RID: 5024
		public GameObject target;
	}
}
