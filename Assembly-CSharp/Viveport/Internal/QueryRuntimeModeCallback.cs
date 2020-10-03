using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000216 RID: 534
	// (Invoke) Token: 0x06000F0F RID: 3855
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void QueryRuntimeModeCallback(int nResult, int nMode);
}
