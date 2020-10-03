using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x02000223 RID: 547
	internal class Api
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0005EBFB File Offset: 0x0005CDFB
		static Api()
		{
			Api.LoadLibraryManually("viveport_api");
		}

		// Token: 0x06000F27 RID: 3879
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_GetLicense")]
		internal static extern void GetLicense(GetLicenseCallback callback, string appId, string appKey);

		// Token: 0x06000F28 RID: 3880
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_GetLicense")]
		internal static extern void GetLicense_64(GetLicenseCallback callback, string appId, string appKey);

		// Token: 0x06000F29 RID: 3881
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Init")]
		internal static extern int Init(StatusCallback initCallback, string appId);

		// Token: 0x06000F2A RID: 3882
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Init")]
		internal static extern int Init_64(StatusCallback initCallback, string appId);

		// Token: 0x06000F2B RID: 3883
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Shutdown")]
		internal static extern int Shutdown(StatusCallback initCallback);

		// Token: 0x06000F2C RID: 3884
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Shutdown")]
		internal static extern int Shutdown_64(StatusCallback initCallback);

		// Token: 0x06000F2D RID: 3885
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Version")]
		internal static extern IntPtr Version();

		// Token: 0x06000F2E RID: 3886
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_Version")]
		internal static extern IntPtr Version_64();

		// Token: 0x06000F2F RID: 3887
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_QueryRuntimeMode")]
		internal static extern void QueryRuntimeMode(QueryRuntimeModeCallback queryRunTimeCallback);

		// Token: 0x06000F30 RID: 3888
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "IViveportAPI_QueryRuntimeMode")]
		internal static extern void QueryRuntimeMode_64(QueryRuntimeModeCallback queryRunTimeCallback);

		// Token: 0x06000F31 RID: 3889
		[DllImport("kernel32.dll")]
		internal static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x06000F32 RID: 3890 RVA: 0x0005EC08 File Offset: 0x0005CE08
		internal static void LoadLibraryManually(string dllName)
		{
			if (string.IsNullOrEmpty(dllName))
			{
				return;
			}
			if (IntPtr.Size == 8)
			{
				Api.LoadLibrary("x64/" + dllName + "64.dll");
				return;
			}
			Api.LoadLibrary("x86/" + dllName + ".dll");
		}
	}
}
