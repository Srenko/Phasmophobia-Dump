using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000390 RID: 912
	public struct IVRDriverManager
	{
		// Token: 0x04001928 RID: 6440
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRDriverManager._GetDriverCount GetDriverCount;

		// Token: 0x04001929 RID: 6441
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRDriverManager._GetDriverName GetDriverName;

		// Token: 0x0200075F RID: 1887
		// (Invoke) Token: 0x06002F83 RID: 12163
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetDriverCount();

		// Token: 0x02000760 RID: 1888
		// (Invoke) Token: 0x06002F87 RID: 12167
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetDriverName(uint nDriver, StringBuilder pchValue, uint unBufferSize);
	}
}
