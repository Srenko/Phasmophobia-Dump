using System;

namespace Valve.VR
{
	// Token: 0x020003BE RID: 958
	public enum EVRApplicationTransitionState
	{
		// Token: 0x04001B52 RID: 6994
		VRApplicationTransition_None,
		// Token: 0x04001B53 RID: 6995
		VRApplicationTransition_OldAppQuitSent = 10,
		// Token: 0x04001B54 RID: 6996
		VRApplicationTransition_WaitingForExternalLaunch,
		// Token: 0x04001B55 RID: 6997
		VRApplicationTransition_NewAppLaunched = 20
	}
}
