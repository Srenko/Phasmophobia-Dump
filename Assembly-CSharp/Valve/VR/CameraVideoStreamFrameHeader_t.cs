using System;

namespace Valve.VR
{
	// Token: 0x020003FE RID: 1022
	public struct CameraVideoStreamFrameHeader_t
	{
		// Token: 0x04001C93 RID: 7315
		public EVRTrackedCameraFrameType eFrameType;

		// Token: 0x04001C94 RID: 7316
		public uint nWidth;

		// Token: 0x04001C95 RID: 7317
		public uint nHeight;

		// Token: 0x04001C96 RID: 7318
		public uint nBytesPerPixel;

		// Token: 0x04001C97 RID: 7319
		public uint nFrameSequence;

		// Token: 0x04001C98 RID: 7320
		public TrackedDevicePose_t standingTrackedDevicePose;
	}
}
