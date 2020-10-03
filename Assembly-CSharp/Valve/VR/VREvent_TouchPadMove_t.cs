using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003E6 RID: 998
	public struct VREvent_TouchPadMove_t
	{
		// Token: 0x04001C42 RID: 7234
		[MarshalAs(UnmanagedType.I1)]
		public bool bFingerDown;

		// Token: 0x04001C43 RID: 7235
		public float flSecondsFingerDown;

		// Token: 0x04001C44 RID: 7236
		public float fValueXFirst;

		// Token: 0x04001C45 RID: 7237
		public float fValueYFirst;

		// Token: 0x04001C46 RID: 7238
		public float fValueXRaw;

		// Token: 0x04001C47 RID: 7239
		public float fValueYRaw;
	}
}
