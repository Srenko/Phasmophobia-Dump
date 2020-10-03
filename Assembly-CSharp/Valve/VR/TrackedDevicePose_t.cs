using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003DF RID: 991
	public struct TrackedDevicePose_t
	{
		// Token: 0x04001C24 RID: 7204
		public HmdMatrix34_t mDeviceToAbsoluteTracking;

		// Token: 0x04001C25 RID: 7205
		public HmdVector3_t vVelocity;

		// Token: 0x04001C26 RID: 7206
		public HmdVector3_t vAngularVelocity;

		// Token: 0x04001C27 RID: 7207
		public ETrackingResult eTrackingResult;

		// Token: 0x04001C28 RID: 7208
		[MarshalAs(UnmanagedType.I1)]
		public bool bPoseIsValid;

		// Token: 0x04001C29 RID: 7209
		[MarshalAs(UnmanagedType.I1)]
		public bool bDeviceIsConnected;
	}
}
