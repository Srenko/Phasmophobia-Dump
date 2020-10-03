using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000384 RID: 900
	public struct IVRExtendedDisplay
	{
		// Token: 0x0400183C RID: 6204
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRExtendedDisplay._GetWindowBounds GetWindowBounds;

		// Token: 0x0400183D RID: 6205
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRExtendedDisplay._GetEyeOutputViewport GetEyeOutputViewport;

		// Token: 0x0400183E RID: 6206
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRExtendedDisplay._GetDXGIOutputInfo GetDXGIOutputInfo;

		// Token: 0x02000673 RID: 1651
		// (Invoke) Token: 0x06002BD3 RID: 11219
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetWindowBounds(ref int pnX, ref int pnY, ref uint pnWidth, ref uint pnHeight);

		// Token: 0x02000674 RID: 1652
		// (Invoke) Token: 0x06002BD7 RID: 11223
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetEyeOutputViewport(EVREye eEye, ref uint pnX, ref uint pnY, ref uint pnWidth, ref uint pnHeight);

		// Token: 0x02000675 RID: 1653
		// (Invoke) Token: 0x06002BDB RID: 11227
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetDXGIOutputInfo(ref int pnAdapterIndex, ref int pnAdapterOutputIndex);
	}
}
