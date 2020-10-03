using System;

namespace Valve.VR
{
	// Token: 0x020003BA RID: 954
	public enum EVRTrackedCameraError
	{
		// Token: 0x04001B16 RID: 6934
		None,
		// Token: 0x04001B17 RID: 6935
		OperationFailed = 100,
		// Token: 0x04001B18 RID: 6936
		InvalidHandle,
		// Token: 0x04001B19 RID: 6937
		InvalidFrameHeaderVersion,
		// Token: 0x04001B1A RID: 6938
		OutOfHandles,
		// Token: 0x04001B1B RID: 6939
		IPCFailure,
		// Token: 0x04001B1C RID: 6940
		NotSupportedForThisDevice,
		// Token: 0x04001B1D RID: 6941
		SharedMemoryFailure,
		// Token: 0x04001B1E RID: 6942
		FrameBufferingFailure,
		// Token: 0x04001B1F RID: 6943
		StreamSetupFailure,
		// Token: 0x04001B20 RID: 6944
		InvalidGLTextureId,
		// Token: 0x04001B21 RID: 6945
		InvalidSharedTextureHandle,
		// Token: 0x04001B22 RID: 6946
		FailedToGetGLTextureId,
		// Token: 0x04001B23 RID: 6947
		SharedTextureFailure,
		// Token: 0x04001B24 RID: 6948
		NoFrameAvailable,
		// Token: 0x04001B25 RID: 6949
		InvalidArgument,
		// Token: 0x04001B26 RID: 6950
		InvalidFrameBufferSize
	}
}
