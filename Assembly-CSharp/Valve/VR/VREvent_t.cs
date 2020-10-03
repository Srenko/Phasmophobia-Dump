using System;

namespace Valve.VR
{
	// Token: 0x020003F7 RID: 1015
	public struct VREvent_t
	{
		// Token: 0x04001C69 RID: 7273
		public uint eventType;

		// Token: 0x04001C6A RID: 7274
		public uint trackedDeviceIndex;

		// Token: 0x04001C6B RID: 7275
		public float eventAgeSeconds;

		// Token: 0x04001C6C RID: 7276
		public VREvent_Data_t data;
	}
}
