using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000220 RID: 544
	// (Invoke) Token: 0x06000F13 RID: 3859
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void IAPurchaseCallback(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
}
