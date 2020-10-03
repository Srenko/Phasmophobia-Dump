using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002E3 RID: 739
	public struct DestinationMarkerEventArgs
	{
		// Token: 0x0400147F RID: 5247
		public float distance;

		// Token: 0x04001480 RID: 5248
		public Transform target;

		// Token: 0x04001481 RID: 5249
		public RaycastHit raycastHit;

		// Token: 0x04001482 RID: 5250
		public Vector3 destinationPosition;

		// Token: 0x04001483 RID: 5251
		public Quaternion? destinationRotation;

		// Token: 0x04001484 RID: 5252
		public bool forceDestinationPosition;

		// Token: 0x04001485 RID: 5253
		public bool enableTeleport;

		// Token: 0x04001486 RID: 5254
		[Obsolete("`DestinationMarkerEventArgs.controllerIndex` has been replaced with `DestinationMarkerEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x04001487 RID: 5255
		public VRTK_ControllerReference controllerReference;
	}
}
