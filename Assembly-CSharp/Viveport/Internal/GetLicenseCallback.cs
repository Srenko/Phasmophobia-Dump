using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000213 RID: 531
	// (Invoke) Token: 0x06000F03 RID: 3843
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void GetLicenseCallback([MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string signature);
}
