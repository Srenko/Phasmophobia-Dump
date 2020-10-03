using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003E8 RID: 1000
	public struct VREvent_Process_t
	{
		// Token: 0x04001C4A RID: 7242
		public uint pid;

		// Token: 0x04001C4B RID: 7243
		public uint oldPid;

		// Token: 0x04001C4C RID: 7244
		[MarshalAs(UnmanagedType.I1)]
		public bool bForced;
	}
}
