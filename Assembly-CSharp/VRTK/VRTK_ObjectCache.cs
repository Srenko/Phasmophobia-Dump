using System;
using System.Collections.Generic;

namespace VRTK
{
	// Token: 0x020002B9 RID: 697
	public static class VRTK_ObjectCache
	{
		// Token: 0x040012FA RID: 4858
		public static List<VRTK_BasicTeleport> registeredTeleporters = new List<VRTK_BasicTeleport>();

		// Token: 0x040012FB RID: 4859
		public static List<VRTK_DestinationMarker> registeredDestinationMarkers = new List<VRTK_DestinationMarker>();

		// Token: 0x040012FC RID: 4860
		public static VRTK_HeadsetCollision registeredHeadsetCollider = null;

		// Token: 0x040012FD RID: 4861
		public static VRTK_HeadsetControllerAware registeredHeadsetControllerAwareness = null;
	}
}
