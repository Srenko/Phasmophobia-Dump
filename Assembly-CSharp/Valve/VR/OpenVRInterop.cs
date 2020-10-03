using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200039F RID: 927
	public class OpenVRInterop
	{
		// Token: 0x06001FFE RID: 8190
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_InitInternal")]
		internal static extern uint InitInternal(ref EVRInitError peError, EVRApplicationType eApplicationType);

		// Token: 0x06001FFF RID: 8191
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_ShutdownInternal")]
		internal static extern void ShutdownInternal();

		// Token: 0x06002000 RID: 8192
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_IsHmdPresent")]
		internal static extern bool IsHmdPresent();

		// Token: 0x06002001 RID: 8193
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_IsRuntimeInstalled")]
		internal static extern bool IsRuntimeInstalled();

		// Token: 0x06002002 RID: 8194
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_GetStringForHmdError")]
		internal static extern IntPtr GetStringForHmdError(EVRInitError error);

		// Token: 0x06002003 RID: 8195
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_GetGenericInterface")]
		internal static extern IntPtr GetGenericInterface([MarshalAs(UnmanagedType.LPStr)] [In] string pchInterfaceVersion, ref EVRInitError peError);

		// Token: 0x06002004 RID: 8196
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_IsInterfaceVersionValid")]
		internal static extern bool IsInterfaceVersionValid([MarshalAs(UnmanagedType.LPStr)] [In] string pchInterfaceVersion);

		// Token: 0x06002005 RID: 8197
		[DllImport("openvr_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VR_GetInitToken")]
		internal static extern uint GetInitToken();
	}
}
