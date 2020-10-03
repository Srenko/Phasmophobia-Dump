using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003F8 RID: 1016
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct VREvent_t_Packed
	{
		// Token: 0x06002007 RID: 8199 RVA: 0x0009E516 File Offset: 0x0009C716
		public VREvent_t_Packed(VREvent_t unpacked)
		{
			this.eventType = unpacked.eventType;
			this.trackedDeviceIndex = unpacked.trackedDeviceIndex;
			this.eventAgeSeconds = unpacked.eventAgeSeconds;
			this.data = unpacked.data;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0009E548 File Offset: 0x0009C748
		public void Unpack(ref VREvent_t unpacked)
		{
			unpacked.eventType = this.eventType;
			unpacked.trackedDeviceIndex = this.trackedDeviceIndex;
			unpacked.eventAgeSeconds = this.eventAgeSeconds;
			unpacked.data = this.data;
		}

		// Token: 0x04001C6D RID: 7277
		public uint eventType;

		// Token: 0x04001C6E RID: 7278
		public uint trackedDeviceIndex;

		// Token: 0x04001C6F RID: 7279
		public float eventAgeSeconds;

		// Token: 0x04001C70 RID: 7280
		public VREvent_Data_t data;
	}
}
