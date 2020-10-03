using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000395 RID: 917
	public class CVRChaperone
	{
		// Token: 0x06001F34 RID: 7988 RVA: 0x0009D1F8 File Offset: 0x0009B3F8
		internal CVRChaperone(IntPtr pInterface)
		{
			this.FnTable = (IVRChaperone)Marshal.PtrToStructure(pInterface, typeof(IVRChaperone));
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0009D21B File Offset: 0x0009B41B
		public ChaperoneCalibrationState GetCalibrationState()
		{
			return this.FnTable.GetCalibrationState();
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0009D22D File Offset: 0x0009B42D
		public bool GetPlayAreaSize(ref float pSizeX, ref float pSizeZ)
		{
			pSizeX = 0f;
			pSizeZ = 0f;
			return this.FnTable.GetPlayAreaSize(ref pSizeX, ref pSizeZ);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0009D24F File Offset: 0x0009B44F
		public bool GetPlayAreaRect(ref HmdQuad_t rect)
		{
			return this.FnTable.GetPlayAreaRect(ref rect);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0009D262 File Offset: 0x0009B462
		public void ReloadInfo()
		{
			this.FnTable.ReloadInfo();
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0009D274 File Offset: 0x0009B474
		public void SetSceneColor(HmdColor_t color)
		{
			this.FnTable.SetSceneColor(color);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0009D287 File Offset: 0x0009B487
		public void GetBoundsColor(ref HmdColor_t pOutputColorArray, int nNumOutputColors, float flCollisionBoundsFadeDistance, ref HmdColor_t pOutputCameraColor)
		{
			this.FnTable.GetBoundsColor(ref pOutputColorArray, nNumOutputColors, flCollisionBoundsFadeDistance, ref pOutputCameraColor);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0009D29E File Offset: 0x0009B49E
		public bool AreBoundsVisible()
		{
			return this.FnTable.AreBoundsVisible();
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0009D2B0 File Offset: 0x0009B4B0
		public void ForceBoundsVisible(bool bForce)
		{
			this.FnTable.ForceBoundsVisible(bForce);
		}

		// Token: 0x0400192E RID: 6446
		private IVRChaperone FnTable;
	}
}
