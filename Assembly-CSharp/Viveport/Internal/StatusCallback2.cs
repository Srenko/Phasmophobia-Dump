using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000215 RID: 533
	// (Invoke) Token: 0x06000F0B RID: 3851
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void StatusCallback2(int nResult, [MarshalAs(UnmanagedType.LPStr)] string message);
}
