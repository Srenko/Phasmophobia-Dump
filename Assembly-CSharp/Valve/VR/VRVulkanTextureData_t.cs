using System;

namespace Valve.VR
{
	// Token: 0x020003E1 RID: 993
	public struct VRVulkanTextureData_t
	{
		// Token: 0x04001C2E RID: 7214
		public ulong m_nImage;

		// Token: 0x04001C2F RID: 7215
		public IntPtr m_pDevice;

		// Token: 0x04001C30 RID: 7216
		public IntPtr m_pPhysicalDevice;

		// Token: 0x04001C31 RID: 7217
		public IntPtr m_pInstance;

		// Token: 0x04001C32 RID: 7218
		public IntPtr m_pQueue;

		// Token: 0x04001C33 RID: 7219
		public uint m_nQueueFamilyIndex;

		// Token: 0x04001C34 RID: 7220
		public uint m_nWidth;

		// Token: 0x04001C35 RID: 7221
		public uint m_nHeight;

		// Token: 0x04001C36 RID: 7222
		public uint m_nFormat;

		// Token: 0x04001C37 RID: 7223
		public uint m_nSampleCount;
	}
}
