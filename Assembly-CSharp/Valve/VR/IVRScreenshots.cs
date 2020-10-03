using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200038E RID: 910
	public struct IVRScreenshots
	{
		// Token: 0x0400191F RID: 6431
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._RequestScreenshot RequestScreenshot;

		// Token: 0x04001920 RID: 6432
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._HookScreenshot HookScreenshot;

		// Token: 0x04001921 RID: 6433
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._GetScreenshotPropertyType GetScreenshotPropertyType;

		// Token: 0x04001922 RID: 6434
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._GetScreenshotPropertyFilename GetScreenshotPropertyFilename;

		// Token: 0x04001923 RID: 6435
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._UpdateScreenshotProgress UpdateScreenshotProgress;

		// Token: 0x04001924 RID: 6436
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._TakeStereoScreenshot TakeStereoScreenshot;

		// Token: 0x04001925 RID: 6437
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRScreenshots._SubmitScreenshot SubmitScreenshot;

		// Token: 0x02000756 RID: 1878
		// (Invoke) Token: 0x06002F5F RID: 12127
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotError _RequestScreenshot(ref uint pOutScreenshotHandle, EVRScreenshotType type, string pchPreviewFilename, string pchVRFilename);

		// Token: 0x02000757 RID: 1879
		// (Invoke) Token: 0x06002F63 RID: 12131
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotError _HookScreenshot([In] [Out] EVRScreenshotType[] pSupportedTypes, int numTypes);

		// Token: 0x02000758 RID: 1880
		// (Invoke) Token: 0x06002F67 RID: 12135
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotType _GetScreenshotPropertyType(uint screenshotHandle, ref EVRScreenshotError pError);

		// Token: 0x02000759 RID: 1881
		// (Invoke) Token: 0x06002F6B RID: 12139
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetScreenshotPropertyFilename(uint screenshotHandle, EVRScreenshotPropertyFilenames filenameType, StringBuilder pchFilename, uint cchFilename, ref EVRScreenshotError pError);

		// Token: 0x0200075A RID: 1882
		// (Invoke) Token: 0x06002F6F RID: 12143
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotError _UpdateScreenshotProgress(uint screenshotHandle, float flProgress);

		// Token: 0x0200075B RID: 1883
		// (Invoke) Token: 0x06002F73 RID: 12147
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotError _TakeStereoScreenshot(ref uint pOutScreenshotHandle, string pchPreviewFilename, string pchVRFilename);

		// Token: 0x0200075C RID: 1884
		// (Invoke) Token: 0x06002F77 RID: 12151
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRScreenshotError _SubmitScreenshot(uint screenshotHandle, EVRScreenshotType type, string pchSourcePreviewFilename, string pchSourceVRFilename);
	}
}
