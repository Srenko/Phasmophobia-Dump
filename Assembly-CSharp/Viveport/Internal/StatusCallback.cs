using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000214 RID: 532
	// (Invoke) Token: 0x06000F07 RID: 3847
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void StatusCallback(int nResult);
}
