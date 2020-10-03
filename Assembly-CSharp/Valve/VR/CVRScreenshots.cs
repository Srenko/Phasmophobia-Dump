using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200039C RID: 924
	public class CVRScreenshots
	{
		// Token: 0x06001FF0 RID: 8176 RVA: 0x0009E3BA File Offset: 0x0009C5BA
		internal CVRScreenshots(IntPtr pInterface)
		{
			this.FnTable = (IVRScreenshots)Marshal.PtrToStructure(pInterface, typeof(IVRScreenshots));
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0009E3DD File Offset: 0x0009C5DD
		public EVRScreenshotError RequestScreenshot(ref uint pOutScreenshotHandle, EVRScreenshotType type, string pchPreviewFilename, string pchVRFilename)
		{
			pOutScreenshotHandle = 0U;
			return this.FnTable.RequestScreenshot(ref pOutScreenshotHandle, type, pchPreviewFilename, pchVRFilename);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0009E3F7 File Offset: 0x0009C5F7
		public EVRScreenshotError HookScreenshot(EVRScreenshotType[] pSupportedTypes)
		{
			return this.FnTable.HookScreenshot(pSupportedTypes, pSupportedTypes.Length);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0009E40D File Offset: 0x0009C60D
		public EVRScreenshotType GetScreenshotPropertyType(uint screenshotHandle, ref EVRScreenshotError pError)
		{
			return this.FnTable.GetScreenshotPropertyType(screenshotHandle, ref pError);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0009E421 File Offset: 0x0009C621
		public uint GetScreenshotPropertyFilename(uint screenshotHandle, EVRScreenshotPropertyFilenames filenameType, StringBuilder pchFilename, uint cchFilename, ref EVRScreenshotError pError)
		{
			return this.FnTable.GetScreenshotPropertyFilename(screenshotHandle, filenameType, pchFilename, cchFilename, ref pError);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0009E43A File Offset: 0x0009C63A
		public EVRScreenshotError UpdateScreenshotProgress(uint screenshotHandle, float flProgress)
		{
			return this.FnTable.UpdateScreenshotProgress(screenshotHandle, flProgress);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0009E44E File Offset: 0x0009C64E
		public EVRScreenshotError TakeStereoScreenshot(ref uint pOutScreenshotHandle, string pchPreviewFilename, string pchVRFilename)
		{
			pOutScreenshotHandle = 0U;
			return this.FnTable.TakeStereoScreenshot(ref pOutScreenshotHandle, pchPreviewFilename, pchVRFilename);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0009E466 File Offset: 0x0009C666
		public EVRScreenshotError SubmitScreenshot(uint screenshotHandle, EVRScreenshotType type, string pchSourcePreviewFilename, string pchSourceVRFilename)
		{
			return this.FnTable.SubmitScreenshot(screenshotHandle, type, pchSourcePreviewFilename, pchSourceVRFilename);
		}

		// Token: 0x04001935 RID: 6453
		private IVRScreenshots FnTable;
	}
}
