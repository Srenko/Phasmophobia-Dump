using System;

namespace Valve.VR
{
	// Token: 0x02000401 RID: 1025
	public struct Compositor_CumulativeStats
	{
		// Token: 0x04001CB3 RID: 7347
		public uint m_nPid;

		// Token: 0x04001CB4 RID: 7348
		public uint m_nNumFramePresents;

		// Token: 0x04001CB5 RID: 7349
		public uint m_nNumDroppedFrames;

		// Token: 0x04001CB6 RID: 7350
		public uint m_nNumReprojectedFrames;

		// Token: 0x04001CB7 RID: 7351
		public uint m_nNumFramePresentsOnStartup;

		// Token: 0x04001CB8 RID: 7352
		public uint m_nNumDroppedFramesOnStartup;

		// Token: 0x04001CB9 RID: 7353
		public uint m_nNumReprojectedFramesOnStartup;

		// Token: 0x04001CBA RID: 7354
		public uint m_nNumLoading;

		// Token: 0x04001CBB RID: 7355
		public uint m_nNumFramePresentsLoading;

		// Token: 0x04001CBC RID: 7356
		public uint m_nNumDroppedFramesLoading;

		// Token: 0x04001CBD RID: 7357
		public uint m_nNumReprojectedFramesLoading;

		// Token: 0x04001CBE RID: 7358
		public uint m_nNumTimedOut;

		// Token: 0x04001CBF RID: 7359
		public uint m_nNumFramePresentsTimedOut;

		// Token: 0x04001CC0 RID: 7360
		public uint m_nNumDroppedFramesTimedOut;

		// Token: 0x04001CC1 RID: 7361
		public uint m_nNumReprojectedFramesTimedOut;
	}
}
