using System;

namespace Valve.VR
{
	// Token: 0x020003BC RID: 956
	public enum EVRApplicationError
	{
		// Token: 0x04001B2D RID: 6957
		None,
		// Token: 0x04001B2E RID: 6958
		AppKeyAlreadyExists = 100,
		// Token: 0x04001B2F RID: 6959
		NoManifest,
		// Token: 0x04001B30 RID: 6960
		NoApplication,
		// Token: 0x04001B31 RID: 6961
		InvalidIndex,
		// Token: 0x04001B32 RID: 6962
		UnknownApplication,
		// Token: 0x04001B33 RID: 6963
		IPCFailed,
		// Token: 0x04001B34 RID: 6964
		ApplicationAlreadyRunning,
		// Token: 0x04001B35 RID: 6965
		InvalidManifest,
		// Token: 0x04001B36 RID: 6966
		InvalidApplication,
		// Token: 0x04001B37 RID: 6967
		LaunchFailed,
		// Token: 0x04001B38 RID: 6968
		ApplicationAlreadyStarting,
		// Token: 0x04001B39 RID: 6969
		LaunchInProgress,
		// Token: 0x04001B3A RID: 6970
		OldApplicationQuitting,
		// Token: 0x04001B3B RID: 6971
		TransitionAborted,
		// Token: 0x04001B3C RID: 6972
		IsTemplate,
		// Token: 0x04001B3D RID: 6973
		BufferTooSmall = 200,
		// Token: 0x04001B3E RID: 6974
		PropertyNotSet,
		// Token: 0x04001B3F RID: 6975
		UnknownProperty,
		// Token: 0x04001B40 RID: 6976
		InvalidParameter
	}
}
