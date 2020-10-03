using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000396 RID: 918
	public class CVRChaperoneSetup
	{
		// Token: 0x06001F3D RID: 7997 RVA: 0x0009D2C3 File Offset: 0x0009B4C3
		internal CVRChaperoneSetup(IntPtr pInterface)
		{
			this.FnTable = (IVRChaperoneSetup)Marshal.PtrToStructure(pInterface, typeof(IVRChaperoneSetup));
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0009D2E6 File Offset: 0x0009B4E6
		public bool CommitWorkingCopy(EChaperoneConfigFile configFile)
		{
			return this.FnTable.CommitWorkingCopy(configFile);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0009D2F9 File Offset: 0x0009B4F9
		public void RevertWorkingCopy()
		{
			this.FnTable.RevertWorkingCopy();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0009D30B File Offset: 0x0009B50B
		public bool GetWorkingPlayAreaSize(ref float pSizeX, ref float pSizeZ)
		{
			pSizeX = 0f;
			pSizeZ = 0f;
			return this.FnTable.GetWorkingPlayAreaSize(ref pSizeX, ref pSizeZ);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0009D32D File Offset: 0x0009B52D
		public bool GetWorkingPlayAreaRect(ref HmdQuad_t rect)
		{
			return this.FnTable.GetWorkingPlayAreaRect(ref rect);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0009D340 File Offset: 0x0009B540
		public bool GetWorkingCollisionBoundsInfo(out HmdQuad_t[] pQuadsBuffer)
		{
			uint num = 0U;
			this.FnTable.GetWorkingCollisionBoundsInfo(null, ref num);
			pQuadsBuffer = new HmdQuad_t[num];
			return this.FnTable.GetWorkingCollisionBoundsInfo(pQuadsBuffer, ref num);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0009D380 File Offset: 0x0009B580
		public bool GetLiveCollisionBoundsInfo(out HmdQuad_t[] pQuadsBuffer)
		{
			uint num = 0U;
			this.FnTable.GetLiveCollisionBoundsInfo(null, ref num);
			pQuadsBuffer = new HmdQuad_t[num];
			return this.FnTable.GetLiveCollisionBoundsInfo(pQuadsBuffer, ref num);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0009D3BF File Offset: 0x0009B5BF
		public bool GetWorkingSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatSeatedZeroPoseToRawTrackingPose)
		{
			return this.FnTable.GetWorkingSeatedZeroPoseToRawTrackingPose(ref pmatSeatedZeroPoseToRawTrackingPose);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0009D3D2 File Offset: 0x0009B5D2
		public bool GetWorkingStandingZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatStandingZeroPoseToRawTrackingPose)
		{
			return this.FnTable.GetWorkingStandingZeroPoseToRawTrackingPose(ref pmatStandingZeroPoseToRawTrackingPose);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0009D3E5 File Offset: 0x0009B5E5
		public void SetWorkingPlayAreaSize(float sizeX, float sizeZ)
		{
			this.FnTable.SetWorkingPlayAreaSize(sizeX, sizeZ);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0009D3F9 File Offset: 0x0009B5F9
		public void SetWorkingCollisionBoundsInfo(HmdQuad_t[] pQuadsBuffer)
		{
			this.FnTable.SetWorkingCollisionBoundsInfo(pQuadsBuffer, (uint)pQuadsBuffer.Length);
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0009D40F File Offset: 0x0009B60F
		public void SetWorkingSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pMatSeatedZeroPoseToRawTrackingPose)
		{
			this.FnTable.SetWorkingSeatedZeroPoseToRawTrackingPose(ref pMatSeatedZeroPoseToRawTrackingPose);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0009D422 File Offset: 0x0009B622
		public void SetWorkingStandingZeroPoseToRawTrackingPose(ref HmdMatrix34_t pMatStandingZeroPoseToRawTrackingPose)
		{
			this.FnTable.SetWorkingStandingZeroPoseToRawTrackingPose(ref pMatStandingZeroPoseToRawTrackingPose);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0009D435 File Offset: 0x0009B635
		public void ReloadFromDisk(EChaperoneConfigFile configFile)
		{
			this.FnTable.ReloadFromDisk(configFile);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0009D448 File Offset: 0x0009B648
		public bool GetLiveSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatSeatedZeroPoseToRawTrackingPose)
		{
			return this.FnTable.GetLiveSeatedZeroPoseToRawTrackingPose(ref pmatSeatedZeroPoseToRawTrackingPose);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0009D45B File Offset: 0x0009B65B
		public void SetWorkingCollisionBoundsTagsInfo(byte[] pTagsBuffer)
		{
			this.FnTable.SetWorkingCollisionBoundsTagsInfo(pTagsBuffer, (uint)pTagsBuffer.Length);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0009D474 File Offset: 0x0009B674
		public bool GetLiveCollisionBoundsTagsInfo(out byte[] pTagsBuffer)
		{
			uint num = 0U;
			this.FnTable.GetLiveCollisionBoundsTagsInfo(null, ref num);
			pTagsBuffer = new byte[num];
			return this.FnTable.GetLiveCollisionBoundsTagsInfo(pTagsBuffer, ref num);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0009D4B3 File Offset: 0x0009B6B3
		public bool SetWorkingPhysicalBoundsInfo(HmdQuad_t[] pQuadsBuffer)
		{
			return this.FnTable.SetWorkingPhysicalBoundsInfo(pQuadsBuffer, (uint)pQuadsBuffer.Length);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0009D4CC File Offset: 0x0009B6CC
		public bool GetLivePhysicalBoundsInfo(out HmdQuad_t[] pQuadsBuffer)
		{
			uint num = 0U;
			this.FnTable.GetLivePhysicalBoundsInfo(null, ref num);
			pQuadsBuffer = new HmdQuad_t[num];
			return this.FnTable.GetLivePhysicalBoundsInfo(pQuadsBuffer, ref num);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0009D50B File Offset: 0x0009B70B
		public bool ExportLiveToBuffer(StringBuilder pBuffer, ref uint pnBufferLength)
		{
			pnBufferLength = 0U;
			return this.FnTable.ExportLiveToBuffer(pBuffer, ref pnBufferLength);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0009D522 File Offset: 0x0009B722
		public bool ImportFromBufferToWorking(string pBuffer, uint nImportFlags)
		{
			return this.FnTable.ImportFromBufferToWorking(pBuffer, nImportFlags);
		}

		// Token: 0x0400192F RID: 6447
		private IVRChaperoneSetup FnTable;
	}
}
