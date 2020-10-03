using System;

namespace Valve.VR
{
	// Token: 0x02000400 RID: 1024
	public struct Compositor_FrameTiming
	{
		// Token: 0x04001C9B RID: 7323
		public uint m_nSize;

		// Token: 0x04001C9C RID: 7324
		public uint m_nFrameIndex;

		// Token: 0x04001C9D RID: 7325
		public uint m_nNumFramePresents;

		// Token: 0x04001C9E RID: 7326
		public uint m_nNumMisPresented;

		// Token: 0x04001C9F RID: 7327
		public uint m_nNumDroppedFrames;

		// Token: 0x04001CA0 RID: 7328
		public uint m_nReprojectionFlags;

		// Token: 0x04001CA1 RID: 7329
		public double m_flSystemTimeInSeconds;

		// Token: 0x04001CA2 RID: 7330
		public float m_flPreSubmitGpuMs;

		// Token: 0x04001CA3 RID: 7331
		public float m_flPostSubmitGpuMs;

		// Token: 0x04001CA4 RID: 7332
		public float m_flTotalRenderGpuMs;

		// Token: 0x04001CA5 RID: 7333
		public float m_flCompositorRenderGpuMs;

		// Token: 0x04001CA6 RID: 7334
		public float m_flCompositorRenderCpuMs;

		// Token: 0x04001CA7 RID: 7335
		public float m_flCompositorIdleCpuMs;

		// Token: 0x04001CA8 RID: 7336
		public float m_flClientFrameIntervalMs;

		// Token: 0x04001CA9 RID: 7337
		public float m_flPresentCallCpuMs;

		// Token: 0x04001CAA RID: 7338
		public float m_flWaitForPresentCpuMs;

		// Token: 0x04001CAB RID: 7339
		public float m_flSubmitFrameMs;

		// Token: 0x04001CAC RID: 7340
		public float m_flWaitGetPosesCalledMs;

		// Token: 0x04001CAD RID: 7341
		public float m_flNewPosesReadyMs;

		// Token: 0x04001CAE RID: 7342
		public float m_flNewFrameReadyMs;

		// Token: 0x04001CAF RID: 7343
		public float m_flCompositorUpdateStartMs;

		// Token: 0x04001CB0 RID: 7344
		public float m_flCompositorUpdateEndMs;

		// Token: 0x04001CB1 RID: 7345
		public float m_flCompositorRenderStartMs;

		// Token: 0x04001CB2 RID: 7346
		public TrackedDevicePose_t m_HmdPose;
	}
}
